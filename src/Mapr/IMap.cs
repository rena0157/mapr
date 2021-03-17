namespace Mapr
{
    /// <summary>
    /// Represents a type map from <typeparamref name="TSource"/> <typeparamref name="TTarget"/> 
    /// </summary>
    /// <typeparam name="TSource">The source type.</typeparam>
    /// <typeparam name="TTarget">The target type.</typeparam>
    public interface IMap<in TSource, out TTarget>
    {
        /// <summary>
        /// Maps the <paramref name="source"/> from its current type <typeparamref name="TSource"/>
        /// to the target type <typeparamref name="TTarget"/>
        /// </summary>
        /// <param name="source">The source object that will be mapped.</param>
        /// <returns>An instance of the source mapped to the <typeparamref name="TTarget"/></returns>
        TTarget Map(TSource source);
    }
}