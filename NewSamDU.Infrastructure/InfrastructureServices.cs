using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NewSamDU.Application.Abstractions.IServices;
using NewSamDU.Infrastructure.Repositories;
using NewSamDU.Infrastructure.Services;

namespace NewSamDU.Infrastructure;

public static class InfrastructureServices
{
    public static IServiceCollection RegisterInfrastructureServices(
        this IServiceCollection services,
        string connectionString
    )
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<AuthRepository>();

        services.AddScoped<NewsRepository>();

        services.AddScoped<SlideRepository>();
        services.AddScoped<AnnouncementRepositoy>();

        services.AddScoped<PageRepository>();

        services.AddScoped<MenuRepository>();

        services.AddScoped<IValidationService, ValidationService>();

        services.AddScoped<ExternalProjectRepository>();
        return services;
    }
}
