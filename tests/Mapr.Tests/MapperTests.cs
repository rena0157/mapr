using System;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Mapr.Tests
{
    public class MapperTests : TestBed
    {
        [Fact]
        public void Map_ShouldMap_WhenTypeMapExists()
        {
            var typeMap = Substitute.For<IMap<int, string>>();
            var locator = Substitute.For<IMapLocator>();
            locator.LocateMapFor<int, string>().Returns(typeMap);

            var mapper = new Mapper(locator);

            var source = Fixture.Create<int>();
            var expectedResult = Fixture.Create<string>();

            typeMap.Map(source).Returns(expectedResult);

            var result = mapper.Map<int, string>(source);

            result.Should().Be(expectedResult);
        }

        [Fact]
        public void Map_ShouldNotThrow_WhenPassedNull()
        {
            var locator = Substitute.For<IMapLocator>();

            var mapper = new Mapper(locator);

            Action act = () => mapper.Map<string?, string?>((string)null!);

            act.Should().NotThrow<ArgumentNullException>();
        }
        
        [Fact]
        public void Map_WithCollection_ShouldMap_WhenTypeMapExists()
        {
            var typeMap = Substitute.For<IMap<int, string>>();
            var locator = Substitute.For<IMapLocator>();
            locator.LocateMapFor<int, string>().Returns(typeMap);

            var mapper = new Mapper(locator);

            var source = Fixture.CreateMany<int>().ToList();
            var expectedResult = source.Select(s => s.ToString()).ToList();

            typeMap.Map(Arg.Any<int>()).Returns(c => c.Arg<int>().ToString());

            var result = mapper.Map<int, string>(source);

            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}