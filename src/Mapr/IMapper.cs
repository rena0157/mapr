using System.Collections.Generic;

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
        
        /// <summary>
        /// Map from a collection of <typeparamref name="TSource"/> to a collection of <typeparamref name="TDestination"/>
        /// </summary>
        /// <param name="source">The source collection.</param>
        /// <typeparam name="TSource">The source type</typeparam>
        /// <typeparam name="TDestination">The destination type</typeparam>
        /// <returns>An <see cref="IEnumerable{T}"/> of the mapped values.</returns>
        public IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> source);
    }
}