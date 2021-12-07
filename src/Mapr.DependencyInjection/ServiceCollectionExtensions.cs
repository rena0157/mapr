using System;
using Microsoft.Extensions.DependencyInjection;

namespace Mapr.DependencyInjection;

/// <summary>
/// Extensions for <see cref="IServiceCollection"/> for setting up Mapr.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the Mapr library to this <see cref="IServiceCollection"/>
    /// </summary>
    /// <param name="services">The service collection that mapping will be added to.</param>
    /// <param name="config">A configuration builder action.</param>
    public static void AddMapr(this IServiceCollection services, Action<IMapperConfig> config)
    {
        var configuration = new MapperConfig(services);
        config(configuration);
    }
}