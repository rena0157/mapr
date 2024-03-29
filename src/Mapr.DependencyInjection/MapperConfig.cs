﻿using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Mapr.DependencyInjection;

/// <summary>
/// Represents an implementation of <see cref="IMapperConfig"/>
/// </summary>
public class MapperConfig : IMapperConfig
{
    private readonly IServiceCollection _services;

    /// <summary>
    /// Initializes a new instance of <see cref="MapperConfig"/>
    /// </summary>
    /// <param name="services">The service collection that the maps will be added to.</param>
    public MapperConfig(IServiceCollection services)
    {
        _services = services;
        RegisterDefaults();
    }

    /// <inheritdoc />
    public IMapperConfig AddMap<TSource, TDestination, TMap>() where TMap : class, IMap<TSource, TDestination>
    {
        var type = typeof(TMap);
        var mapAttribute = type.GetCustomAttribute<MapAttribute>() ?? new MapAttribute();

        if (mapAttribute.Lifetime == MapLifetime.Singleton)
        {
            _services.AddSingleton<IMap<TSource, TDestination>, TMap>();
        }
        else
        {
            _services.AddTransient<IMap<TSource, TDestination>, TMap>();
        }
        
        return this;
    }

    /// <inheritdoc />
    public IMapperConfig Scan(params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
            Scan(assembly);

        return this;
    }

    /// <inheritdoc />
    public IMapperConfig Scan(Assembly assembly)
    {
        foreach (var type in assembly.GetExportedTypes())
            RegisterType(type);

        return this;
    }

    /// <inheritdoc />
    public IMapperConfig Scan<T>()
    {
        return Scan(typeof(T).Assembly);
    }

    private void RegisterDefaults()
    {
        _services.AddSingleton<IMapper, Mapper>();
        _services.AddSingleton<IMapLocator, MapLocator>();
        _services.AddSingleton<MapFactory>(provider => provider.GetService);
    }

    private void RegisterType(Type type)
    {
        if (!type.IsClass)
            return;

        var interfaces = type
            .GetInterfaces()
            .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMap<,>));

        foreach (var i in interfaces)
        {
            var mapAttribute = type.GetCustomAttribute<MapAttribute>() ?? new MapAttribute();

            if (mapAttribute.Lifetime == MapLifetime.Singleton)
            {
                _services.AddSingleton(i, type);
            }
            else
            {
                _services.AddTransient(i, type);
            }
        }
    }
}