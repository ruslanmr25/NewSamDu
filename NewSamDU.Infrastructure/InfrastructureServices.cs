using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NewSamDU.Infrastructure.Repositories;

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
        return services;
    }
}
