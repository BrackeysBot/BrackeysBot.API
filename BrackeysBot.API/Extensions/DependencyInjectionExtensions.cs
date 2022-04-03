using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BrackeysBot.API.Extensions;

/// <summary>
///     Extension methods for dependency-injection related entities.
/// </summary>
public static class DependencyInjectionExtensions
{
    /// <summary>
    ///     Adds an <see cref="IHostedService" /> registration for the given type, while simultaneously adding it as a singleton.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to add the service to.</param>
    /// <typeparam name="TService">The type of the service to add.</typeparam>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static IServiceCollection AddHostedSingleton<TService>(this IServiceCollection services)
        where TService : class, IHostedService
    {
        services.AddSingleton<TService>();
        return services.AddSingleton<IHostedService, TService>(provider => provider.GetRequiredService<TService>());
    }

    /// <summary>
    ///     Adds an <see cref="IHostedService" /> registration for the given type, while simultaneously adding it as a singleton.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to add the service to.</param>
    /// <param name="type">The type of the service to register and the implementation to use.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static IServiceCollection AddHostedSingleton(this IServiceCollection services, Type type)
    {
        services.AddSingleton(type);
        return services.AddSingleton(provider => (IHostedService) provider.GetRequiredService(type));
    }
}
