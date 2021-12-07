using System;
using Mapr.Exceptions;

namespace Mapr;

/// <inheritdoc />
public class MapLocator : IMapLocator
{
    private readonly MapFactory _mapFactory;

    /// <summary>
    /// Initializes a new instance of <see cref="MapLocator"/>
    /// </summary>
    /// <param name="mapFactory">A delegate for locating maps.</param>
    public MapLocator(MapFactory mapFactory)
    {
        _mapFactory = mapFactory;
    }

    /// <inheritdoc />
    public IMap<TSource, TDestination> LocateMapFor<TSource, TDestination>()
    {
        var mapType = typeof(IMap<TSource, TDestination>);

        try
        {
            if (_mapFactory(mapType) is not IMap<TSource, TDestination> typeMap)
                throw new MapNotFoundException(mapType);

            return typeMap;
        }
        catch (Exception ex)
        {
            throw new MapLocatorException(mapType, ex);
        }
    }
}