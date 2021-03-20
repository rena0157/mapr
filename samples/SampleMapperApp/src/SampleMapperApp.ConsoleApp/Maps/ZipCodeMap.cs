using Mapr;
using SampleMapperApp.ConsoleApp.Domain;

namespace SampleMapperApp.ConsoleApp.Maps
{
    public class ZipCodeMap : IMap<ZipCode, string>, IMap<string, ZipCode>
    {
        public string Map(ZipCode source)
        {
            return source.Value;
        }

        public ZipCode Map(string source)
        {
            return new(source);
        }
    }
}