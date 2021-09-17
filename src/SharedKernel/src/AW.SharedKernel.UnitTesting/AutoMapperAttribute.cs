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
        public AutoMapperDataAttribute(params Type[] profileTypes)
          : base(() => CreateFixture(profileTypes))
        {
        }

        private static IFixture CreateFixture(params Type[] profileTypes)
        {
            var fixture = new Fixture()
                .Customize(new AutoMapperCustomization(CreateMapper(profileTypes)))
                .Customize(new AutoMoqCustomization());
            
            fixture.Behaviors
                .OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));

            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            return fixture;
        }

        private static IMapper CreateMapper(params Type[] profileTypes)
        {
            return new MapperConfiguration(opts =>
            {
                foreach (var profileType in profileTypes)
                {
                    opts.AddProfile(profileType);
                }
            })
            .CreateMapper();
        }
    }
}