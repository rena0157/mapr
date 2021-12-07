using System.Reflection;

namespace Mapr.DependencyInjection;

/// <summary>
/// Represents an interface for a mapping configuration.
/// </summary>
public interface IMapperConfig
{
    /// <summary>
    /// Adds an <see cref="IMap{TSource,TTarget}"/> with the provided <typeparamref name="TMap"/>
    /// </summary>
    /// <typeparam name="TSource">The source of the type map.</typeparam>
    /// <typeparam name="TDestination">The destination of the type map.</typeparam>
    /// <typeparam name="TMap">The actual type map type being registered.</typeparam>
    /// <returns>This instance of <see cref="IMapperConfig"/></returns>
    IMapperConfig AddMap<TSource, TDestination, TMap>() where TMap : class, IMap<TSource, TDestination>;

    /// <summary>
    /// Scans a collection of assemblies for classes that implement the <see cref="IMap{TSource,TTarget}"/>
    /// interface. 
    /// </summary>
    /// <param name="assemblies">The assemblies that will be scanned.</param>
    /// <returns>This instance of <see cref="IMapperConfig"/></returns>
    IMapperConfig Scan(params Assembly[] assemblies);

    /// <summary>
    /// Scans the specified assembly
    /// </summary>
    /// <param name="assembly">The assembly that will be scanned.</param>
    /// <returns>This instance of <see cref="IMapperConfig"/></returns>
    IMapperConfig Scan(Assembly assembly);

    /// <summary>
    /// Scans the assembly with the containing type provided.
    /// </summary>
    /// <typeparam name="T">A type that is contained within the assembly that will be scanned.</typeparam>
    /// <returns>This instance of <see cref="IMapperConfig"/></returns>
    IMapperConfig Scan<T>();
}