using AutoFixture;
using AutoFixture.AutoNSubstitute;

namespace Mapr.Tests
{
    public class TestBed
    {
        protected TestBed()
        {
            Fixture = new Fixture()
                .Customize(new AutoNSubstituteCustomization());
        }

        protected IFixture Fixture { get; }
    }
}