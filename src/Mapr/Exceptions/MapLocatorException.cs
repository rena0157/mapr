using System;

namespace Mapr.Exceptions
{
    public class MapLocatorException : Exception
    {
        /// <inheritdoc />
        public MapLocatorException(Type typeMapType, Exception innerException)
            : base($"The type map: {typeMapType} failed to initialize.", innerException)
        {
        }
    }
}