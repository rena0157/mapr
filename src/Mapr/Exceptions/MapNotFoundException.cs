using System;

namespace Mapr.Exceptions;

/// <summary>
/// Represents an exception that occurs when a <see cref="MapLocator"/> is unable to find a
/// map.
/// </summary>
public class MapNotFoundException : Exception
{
    /// <inheritdoc />
    public MapNotFoundException(Type mapType) 
        : base($"The type map: {mapType} could not be found. Check to make sure that " +
               "it is registered.")
    {
    }
}