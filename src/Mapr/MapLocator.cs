using System;
using Mapr.Exceptions;

namespace Mapr
{
    public class MapLocator : IMapLocator
    {
        private readonly MapFactory _typeMapFactory;

        /// <summary>
        /// Initializes a new instance of <see cref="MapLocator"/>
        /// </summary>
        /// <param name="typeMapFactory"></param>
        public MapLocator(MapFactory typeMapFactory)
        {
            _typeMapFactory = typeMapFactory;
        }

        /// <inheritdoc />
        public IMap<TSource, TDestination> LocateMapFor<TSource, TDestination>()
        {
            var mapType = typeof(IMap<TSource, TDestination>);

            try
            {
                if (_typeMapFactory(mapType) is not IMap<TSource, TDestination> typeMap)
                    throw new MapNotFoundException(mapType);

                return typeMap;
            }
            catch (Exception ex)
            {
                throw new MapLocatorException(mapType, ex);
            }
        }
    }
}