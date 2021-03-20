# Mapr

[![Mapr Build](https://github.com/rena0157/mapr/actions/workflows/build.yml/badge.svg)](https://github.com/rena0157/mapr/actions/workflows/build.yml)

A simple mediator pattern based object to object mapper.

## How to Install

Release Version on [NuGet](https://www.nuget.org/packages/Mapr/)
```
dotnet add package Mapr --version xxx
```

Pre-Release Version (Alpha / Beta) on [GitHub](https://github.com/rena0157/mapr/packages/)
```
dotnet add package Mapr --version xxx
```

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

Here is an example of a mapper that converts from `string` to `int` and back.

```cs
// StringMapper.cs

// a map between int and string as well as a map between string and int.
public class StringMapper : IMap<string, int>, IMap<int, string>
{
    public int Map(string source)
    {
        return int.Parse(source);
    }

    public string Map(int source)
    {
        return source.ToString();
    }
}

```

The above map then is set up in the service registration below.

```cs

// Service Registration file
// This could be in Startup.cs for an ASP.NET Project.

var services = new ServiceCollection();

services.AddMapr(config =>
{
    // to add specific type maps.
    config.AddMap<string, int, TestMap>()
        .AddMap<int, string, TestMap>();

    // To scan an assembly for IMap inheritors.
    // config.Scan(typeof(TestMap).Assembly);

    // Gets the assembly that contains TestMap and scans it for all IMap inheritors
    // config.Scan<TestMap>();

});

var provider = services.BuildServiceProvider();

// Get the mapper from the service provider.
var mapper = provider.GetService<IMapper>();

var testString = "2";
var testInt = 10;

// will return the int: 2
int mappedStringToInt = mapper.Map<string, int>(testString);

// will return the string: "10"
string mappedIntToString = mapper.Map<int, string>(testInt);

```

## More Complex Mapping

See samples for more complex examples
