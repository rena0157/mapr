using System;
using Mapr;
using Mapr.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using SampleMapperApp.ConsoleApp.DtoModels;
using SampleMapperApp.ConsoleApp.Models;


var services = new ServiceCollection();

services.AddMapr(config =>
{
    config.Scan(typeof(Person).Assembly);
});

var provider = services.BuildServiceProvider();

var mapper = provider.GetRequiredService<IMapper>();

var address = new Address("123", "Fake Street", new ZipCode("90120"), "USA");
var person = new Person(1, "John", "Smith", address);

var personModel = mapper.Map<Person, PersonModel>(person);
