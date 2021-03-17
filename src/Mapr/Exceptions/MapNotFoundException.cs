using System;

namespace Mapr.Exceptions
{
    public class MapNotFoundException : Exception
    {
        /// <inheritdoc />
        public MapNotFoundException(Type typeMapType) : base($"The type map: {typeMapType} could not be found.")
        {
        }
    }
}