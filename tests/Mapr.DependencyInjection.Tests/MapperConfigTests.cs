using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Mapr.DependencyInjection.Tests;

public class MapperConfigTests
{
    [Fact]
    public void ShouldAdd_Defaults()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddMapr(_ => { });

        serviceCollection.Should()
            .Contain(
                s => s.ServiceType == typeof(IMapper) && 
                     s.ImplementationType == typeof(Mapper) &&
                     s.Lifetime == ServiceLifetime.Singleton
            );

        serviceCollection.Should()
            .Contain(
                s => s.ServiceType == typeof(IMapLocator) && 
                     s.ImplementationType == typeof(MapLocator) &&
                     s.Lifetime == ServiceLifetime.Singleton
            );

        serviceCollection.Should()
            .Contain(
                s => s.ServiceType == typeof(MapFactory) &&
                     s.Lifetime == ServiceLifetime.Singleton
            );
    }

    [Fact]
    public void Scan_ShouldAddAllClassesFromAssemblyThat_InheritFromITypeMap()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddMapr(config =>
        {
            config.Scan(typeof(TestMap).Assembly);
        });

        serviceCollection.Should()
            .Contain(
                s => s.ImplementationType == typeof(TestMap) && 
                     s.ServiceType == typeof(IMap<string, int>) &&
                     s.Lifetime == ServiceLifetime.Transient
            );

        serviceCollection.Should()
            .Contain(
                s => s.ImplementationType == typeof(TestMap) && 
                     s.ServiceType == typeof(IMap<int, string>) &&
                     s.Lifetime == ServiceLifetime.Transient
            );
        
        serviceCollection.Should()
            .Contain(
                s => s.ImplementationType == typeof(TestSingletonMap) && 
                     s.ServiceType == typeof(IMap<string, string>) &&
                     s.Lifetime == ServiceLifetime.Singleton
            );
    }

    [Fact]
    public void Scan_WithT_ShouldAddAllClassesFromAssemblyThat_InheritFromITypeMap()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddMapr(config =>
        {
            config.Scan<TestMap>();
        });

        serviceCollection.Should()
            .Contain(
                s => s.ImplementationType == typeof(TestMap) && 
                     s.ServiceType == typeof(IMap<string, int>) &&
                     s.Lifetime == ServiceLifetime.Transient
            );

        serviceCollection.Should()
            .Contain(
                s => s.ImplementationType == typeof(TestMap) && 
                     s.ServiceType == typeof(IMap<int, string>) &&
                     s.Lifetime == ServiceLifetime.Transient
            );
        
        serviceCollection.Should()
            .Contain(
                s => s.ImplementationType == typeof(TestSingletonMap) && 
                     s.ServiceType == typeof(IMap<string, string>) &&
                     s.Lifetime == ServiceLifetime.Singleton
            );
    }

    [Fact]
    public void AddMap_ShouldAddMap()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddMapr(config =>
        {
            config.AddMap<string, int, TestMap>()
                .AddMap<int, string, TestMap>();
        });

        serviceCollection.Should()
            .Contain(
                s => s.ImplementationType == typeof(TestMap) && 
                     s.ServiceType == typeof(IMap<string, int>) &&
                     s.Lifetime == ServiceLifetime.Transient
            );

        serviceCollection.Should()
            .Contain(
                s => s.ImplementationType == typeof(TestMap) && 
                     s.ServiceType == typeof(IMap<int, string>) &&
                     s.Lifetime == ServiceLifetime.Transient
            );
    }
    
    [Fact]
    public void AddMap_ShouldAddSingletonMap_WhenAttributeIsPresent()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddMapr(config =>
        {
            config.AddMap<string, string, TestSingletonMap>();
        });

        serviceCollection.Should()
            .Contain(
                s => s.ImplementationType == typeof(TestSingletonMap) && 
                     s.ServiceType == typeof(IMap<string, string>) &&
                     s.Lifetime == ServiceLifetime.Singleton
            );
    }
}