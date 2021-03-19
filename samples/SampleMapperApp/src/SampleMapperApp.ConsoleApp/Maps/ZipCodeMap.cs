using Mapr;
using SampleMapperApp.ConsoleApp.Models;

namespace SampleMapperApp.ConsoleApp.Maps
{
    public class ZipCodeMap : IMap<ZipCode, string>, IMap<string, ZipCode>
    {
        /// <inheritdoc />
        public string Map(ZipCode source)
        {
            return source.Value;
        }

        /// <inheritdoc />
        public ZipCode Map(string source)
        {
            return new(source);
        }
    }
}