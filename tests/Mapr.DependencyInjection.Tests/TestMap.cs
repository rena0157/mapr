namespace Mapr.DependencyInjection.Tests
{
    public class TestMap : IMap<string, int>, IMap<int, string>
    {
        /// <inheritdoc />
        public int Map(string source)
        {
            return int.Parse(source);
        }

        /// <inheritdoc />
        public string Map(int source)
        {
            return source.ToString();
        }
    }
}