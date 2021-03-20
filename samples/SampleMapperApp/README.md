# Sample Map App

The purpose of this sample app is to present the intended use and setup of Mapr.

It consists of four parts:

1. A `Domain` folder that contains the domain models
2. A `DataModels` folder that contains DTOs (Normally this would be in different project.)
3. A `Maps` folder that contains the maps for the models and DTOs
4. A `Program.cs` file that configures and uses the `IMapper`.

The example domain object is a `Person` class. It contains a couple of properties as well as a 
value object `Address`, which also contains some properties and another value object `ZipCode`.

The `Maps` folder contains the definitions for the mapping between the domain models and the 
DTOs. Note that the `IMapper` interface is injected into the mapper. This allows you to map 
more complex objects such as `Person` that in tern contains objects that need to be mapped.