using System;

namespace Mapr.Exceptions;

/// <summary>
/// Represents an exception that occurs when the <see cref="MapLocator"/> is unable to
/// find or instantiate an <see cref="IMap{TSource,TTarget}"/>
/// </summary>
public class MapLocatorException : Exception
{
    /// <inheritdoc />
    public MapLocatorException(Type mapType, Exception innerException)
        : base($"The type map: {mapType} failed to initialize.", innerException)
    {
    }
}