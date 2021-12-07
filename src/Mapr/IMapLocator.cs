using System;

namespace Mapr;

/// <summary>
/// Represents a delegate that can be used to wire up the <see cref="IMapLocator"/> with a dependency
/// injection container. This is done so that the container does not have to be directly injected into the
/// <see cref="IMapLocator"/> and can be indirectly referenced. 
/// </summary>
public delegate object? MapFactory(Type mapTyp);
    
/// <summary>
/// Represents an interface for locating <see cref="IMap{TSource,TTarget}"/> instances.
/// </summary>
public interface IMapLocator
{
    /// <summary>
    /// Locates an instance of <see cref="IMap{TSource,TTarget}"/> with the provided <typeparamref name="TSource"/>
    /// and <typeparamref name="TDestination"/>
    /// </summary>
    /// <typeparam name="TSource">The source object type.</typeparam>
    /// <typeparam name="TDestination">The destination object type.</typeparam>
    /// <returns>An instance of <see cref="IMap{TSource,TTarget}"/></returns>
    public IMap<TSource, TDestination> LocateMapFor<TSource, TDestination>();
}