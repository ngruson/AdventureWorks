using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using AW.Services.Sales.Core.Entities;
using Moq;
using System.Linq;
using System.Net.WebSockets;

namespace AW.SharedKernel.UnitTesting
{
    public class SalesOrderAutoMoqDataAttribute : AutoDataAttribute
    {
        public SalesOrderAutoMoqDataAttribute()
          : base(() => CreateFixture())
        {
        }

        private static IFixture CreateFixture()
        {
            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            fixture.Behaviors
                .OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));

            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            fixture.Register((Mock<ProductLine> m) => m.Object);
            fixture.Register((Mock<Class> m) => m.Object);
            fixture.Register((Mock<Style> m) => m.Object);

            return fixture;
        }
    }
}
