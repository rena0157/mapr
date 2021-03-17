using System;
using FluentAssertions;
using Mapr.Exceptions;
using NSubstitute;
using Xunit;

namespace Mapr.Tests
{
    public class MapLocatorTests
    {
        [Fact]
        public void Locate_ShouldReturnType_WhenFound()
        {
            var expectedTypeMap = Substitute.For<IMap<string, int>>();
            var locator = new MapLocator(_ => expectedTypeMap);

            var typeMap = locator.LocateMapFor<string, int>();

            typeMap.Should().Be(expectedTypeMap);
        }

        [Fact]
        public void Locate_ShouldThrow_WhenTypeMapIsNotFound()
        {
            var locator = new MapLocator(_ => null!);
            Action act = () => locator.LocateMapFor<string, int>();

            act.Should().Throw<MapLocatorException>()
                .WithInnerException<MapNotFoundException>();
        }

        [Fact]
        public void Locate_ShouldWrapException_FromFactory()
        {
            var locator = new MapLocator(_ => throw new Exception());
            Action act = () => locator.LocateMapFor<string, int>();

            act.Should().Throw<MapLocatorException>()
                .WithInnerException<Exception>();
        }
    }
}