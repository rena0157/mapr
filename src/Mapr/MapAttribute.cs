using System;

namespace Mapr;

/// <summary>
/// Represents an attribute that can be placed on map classes to control their behavior
/// and registration.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class MapAttribute : Attribute
{
    /// <summary>
    /// Gets or sets the lifetime for the map. Determining how it will be registered in the
    /// DI container. The default is <see cref="MapLifetime.Transient"/>.
    /// </summary>
    public MapLifetime Lifetime { get; set; } = MapLifetime.Transient;
}

/// <summary>
/// Represents the different lifetimes that can be used to register a map.
/// </summary>
public enum MapLifetime
{
    /// <summary>
    /// Registers the map as a singleton.
    /// </summary>
    Singleton,
    
    /// <summary>
    /// Registers the map as a transient.
    /// </summary>
    Transient
}