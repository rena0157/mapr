namespace Mapr.DependencyInjection.Tests
{
    public class TestMap : IMap<string, int>, IMap<int, string>
    {
        /// <inheritdoc />
        public int Map(string source)
        {
            return 0;
        }

        /// <inheritdoc />
        public string Map(int source)
        {
            return "";
        }
    }
}