namespace Mapr
{
    /// <summary>
    /// Represents the interface for a Mapper.
    /// </summary>
    /// <seealso cref="Mapper"/>
    public interface IMapper
    {
        /// <summary>
        /// Map between the <typeparamref name="TSource"/> and the <typeparamref name="TDestination"/>
        /// </summary>
        /// <param name="source">The source object.</param>
        /// <typeparam name="TSource">The source object type.</typeparam>
        /// <typeparam name="TDestination">The destination object type.</typeparam>
        /// <returns>A instance of <typeparamref name="TDestination"/> that represents a mapped type.</returns>
        public TDestination Map<TSource, TDestination>(TSource source);
    }
}