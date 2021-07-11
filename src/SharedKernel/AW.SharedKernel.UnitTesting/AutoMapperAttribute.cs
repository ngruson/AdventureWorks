using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using AutoMapper;
using System;
using System.Linq;

namespace AW.SharedKernel.UnitTesting
{
    public class AutoMapperDataAttribute : AutoDataAttribute
    {
        public AutoMapperDataAttribute(Type type)
          : base(() => CreateFixture(type))
        {
        }

        private static IFixture CreateFixture(Type type)
        {
            var fixture = new Fixture()
                .Customize(new AutoMapperCustomization(CreateMapper(type)))
                .Customize(new AutoMoqCustomization());
            
            fixture.Behaviors
                .OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));

            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            return fixture;
        }

        private static IMapper CreateMapper(Type profileType)
        {
            return new MapperConfiguration(opts =>
            {
                opts.AddProfile(profileType);
            })
            .CreateMapper();
        }
    }
}