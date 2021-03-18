# Mapr

A simple object to object mapper without any of the magic configuration.

## Why Mapr?

- Does not rely on a naming contract for properties
- Mapping of immutable objects.
- Easily test and mock maps.
- Maps can be shared across projects.
- Simple Configuration.

Although there are many mapping libraries out there, Mapr takes a different approach. 
No Expression tree configuration or magic behind the scenes. Just a simple mapper.

## How does it work?

In a nutshell, all inheritors of `IMap<TSource, TDestination>` are registered in a DI container, 
see Mapr.DependencyInjection for `IServiceCollection`.

These `IMap` implementations are then loaded and injected using the DI container in the mapper whenever a 
`Map<TSource, TDestination>(sourceObject)` is called.

## Simple Types

TestMap.cs
```cs

// a map between int and string as well as a map between string and int.
public class TestMap : IMap<string, int>, IMap<int, string>
{
    // maps the string source to an int.
    public int Map(string source)
    {
        // insert custom mapping here.
        return 0;
    }

    // maps the int source to a string.
    public string Map(int source)
    {
        // insert custom mapping here.
        return "";
    }
}

```

## Service Collection Setup

Service Collection Setup

```cs

var serviceCollection = new ServiceCollection();

serviceCollection.AddMapr(config =>
{
    // to add specific type maps.
    config.AddMap<string, int, TestMap>()
        .AddMap<int, string, TestMap>();

    // To scan an assembly for IMap inheritors.
    // config.Scan(typeof(TestMap).Assembly);

    // Gets the assembly that contains TestMap and scans it for all IMap inheritors
    // config.Scan<TestMap>();

});

var serviceProvider = serviceCollection.BuildServiceProvider();

var mapper = serviceProvider.GetService<IMapper>();

```
