namespace Mapr.DependencyInjection.Tests;

[Map(Lifetime = MapLifetime.Singleton)]
public class TestSingletonMap: IMap<string, string>
{
    /// <inheritdoc />
    public string Map(string source)
    {
        return source;
    }
}