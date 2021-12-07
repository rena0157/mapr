using AutoFixture;
using AutoFixture.AutoNSubstitute;

namespace Mapr.DependencyInjection.Tests;

public class TestBed
{
    protected TestBed()
    {
        Fixture = new Fixture()
            .Customize(new AutoNSubstituteCustomization());
    }

    protected IFixture Fixture { get; }
}