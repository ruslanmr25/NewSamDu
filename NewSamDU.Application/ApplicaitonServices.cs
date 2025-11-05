using System;
using Microsoft.Extensions.DependencyInjection;
using NewSamDU.Application.Abstractions.IServices;
using NewSamDU.Application.Services;

namespace NewSamDU.Application;

public static class ApplicaitonServices
{
    public static IServiceCollection RegisterApplicationSerices(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        services.AddScoped<TokenService>();
        return services;
    }
}
