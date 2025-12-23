using System.Text;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
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
                .AllowAnyOrigin() // Har qanday domen (frontend URL) uchun
                .AllowAnyMethod() // Har qanday HTTP metod (GET, POST, PUT, DELETE va h.k.)
                .AllowAnyHeader() // Har qanday header
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

builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
    {
        var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unkown";

        return RateLimitPartition.GetFixedWindowLimiter(
            ip,
            partition => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 500,

                Window = TimeSpan.FromSeconds(10),
            }
        );
    });
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

var app = builder.Build();

app.UseCors("AllowFrontend");

app.UseStaticFiles();

app.UseRateLimiter();

var supportedCultures = new[] { "uz", "en", "ru", "kr" };

var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture("uz")
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

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
