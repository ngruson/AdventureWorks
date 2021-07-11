using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using System.Linq;

namespace AW.SharedKernel.UnitTesting
{
    public class MockHttpDataAttribute : AutoDataAttribute
    {
        public MockHttpDataAttribute()
          : base(() => CreateFixture())
        {
        }

        private static IFixture CreateFixture()
        {
            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization())
                .AddMockHttp();

            fixture.Behaviors
                .OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));

            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            return fixture;
        }
    }
}