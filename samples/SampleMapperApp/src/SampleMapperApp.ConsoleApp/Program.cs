using System;
using Mapr;
using Mapr.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using SampleMapperApp.ConsoleApp.DataModels;
using SampleMapperApp.ConsoleApp.Domain;

// Create Services
var services = new ServiceCollection();

// Add Mapr
services.AddMapr(config =>
{
    // Use the Scan method to scan assemblies for types that implement IMap
    config.Scan(typeof(Person).Assembly);
});

var provider = services.BuildServiceProvider();

var mapper = provider.GetRequiredService<IMapper>();

var address = new Address("123", "Fake Street", new ZipCode("90120"), "USA");
var person = new Person(1, "John", "Smith", address);

// Map the Person to a Person Model
var personModel = mapper.Map<Person, PersonModel>(person);

Console.WriteLine($"Id {{original : mapped}}: {person.Id} : {personModel.Id}");
Console.WriteLine($"First Name {{original : mapped}}: {person.FirstName} : {personModel.FirstName}");
Console.WriteLine($"Last Name {{original : mapped}}: {person.LastName} : {personModel.LastName}");

// And so on...