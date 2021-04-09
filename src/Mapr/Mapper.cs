namespace Mapr
{
    /// <summary>
    /// Represents an implementation of <see cref="IMapper"/>
    /// </summary>
    /// <remarks>
    /// The <see cref="IMapper"/> and <see cref="Mapper"/> act as a kind of Mediator between
    /// <see cref="IMap{TSource,TTarget}"/> implementations. Using the <see cref="IMapLocator"/> as a
    /// service to locate maps.
    /// </remarks>
    public class Mapper : IMapper
    {
        private readonly IMapLocator _locator;

        /// <summary>
        /// Initializes a new instance of <see cref="Mapper"/>
        /// </summary>
        /// <param name="locator">An instance of <see cref="IMapLocator"/> to find <see cref="IMap{TSource,TTarget}"/>s</param>
        public Mapper(IMapLocator locator)
        {
            _locator = locator;
        }

        /// <summary>
        /// Maps the provided <paramref name="source"/> object to the provided type <typeparamref name="TSource"/>.
        /// </summary>
        /// <remarks>
        /// This method uses an <see cref="IMapLocator"/> to locate the appropriate <see cref="IMap{TSource,TTarget}"/>
        /// for the provided type parameters and then maps them using it.
        /// </remarks>
        /// <param name="source">The source object that will be mapped.</param>
        /// <typeparam name="TSource">The type of the source object.</typeparam>
        /// <typeparam name="TDestination">The destination of the map.</typeparam>
        /// <returns>A new instance of type <typeparamref name="TDestination"/> mapped from the source object.</returns>
        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return _locator.LocateMapFor<TSource, TDestination>().Map(source);
        }
    }
}