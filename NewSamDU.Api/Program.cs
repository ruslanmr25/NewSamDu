using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NewSamDU.Api.Helpers;
using NewSamDU.Application;
using NewSamDU.Application.Responses;
using NewSamDU.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder
    .Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context
                .ModelState.Where(e => e.Value?.Errors.Count > 0)
                .Select(e => new
                {
                    Field = e.Key,
                    Errors = e.Value?.Errors.Select(er => er.ErrorMessage),
                });

            var response = new BadRequest(errors);

            return new BadRequestObjectResult(response);
        };
    });

builder.Services.AddSingleton(sp =>
{
    var env = sp.GetRequiredService<IWebHostEnvironment>();
    return new FolderHelper(env, "uploads");
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowFrontend",
        policy =>
            policy
                .WithOrigins(builder.Configuration["FrontendUrl"] ?? "localhost:5040") // frontend manzilingiz
                .AllowAnyHeader()
                .AllowAnyMethod()
    );
});
builder
    .Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(Options =>
    {
        Options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateLifetime = true,

            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    builder.Configuration["Jwt:Key"]
                        ?? throw new Exception("you must add jwt to appsettings")
                )
            ),
        };
    });

builder.Services.AddSingleton(sp =>
{
    var env = sp.GetRequiredService<IWebHostEnvironment>();
    var folderHelper = sp.GetRequiredService<FolderHelper>();

    return new FileHelper(folderHelper, env);
});

builder.Services.RegisterApplicationSerices();
builder.Services.RegisterInfrastructureServices(
    builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new ArgumentNullException("Default connection string not found")
);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();
app.MapScalarApiReference(options =>
{
    options.Title = "New SamDU API";
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
