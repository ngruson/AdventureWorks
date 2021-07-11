using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace AW.SharedKernel.UnitTesting
{
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute()
          : base(() => new Fixture().Customize(new AutoMoqCustomization()))
        {
        }


    }
}