using AW.UI.Web.Internal.ViewModels.Customer;
using FluentAssertions;
using Xunit;

namespace AW.UI.Web.Internal.UnitTests
{
    public class EnumHelperUnitTests
    {
        [Fact]
        public void GetValues_ReturnsEnumList()
        {
            //Act
            var list = EnumHelper<EmailPromotionViewModel>.GetValues();

            //Assert
            list.Count.Should().Be(3);
            list[0].Should().Be(EmailPromotionViewModel.NoPromotions);
            list[1].Should().Be(EmailPromotionViewModel.AWPromotions);
            list[2].Should().Be(EmailPromotionViewModel.AWAndPartnerPromotions);
        }

        [Fact]
        public void GetValuesWithEnum_ReturnsEnumList()
        {
            //Act
            var list = EnumHelper<EmailPromotionViewModel>.GetValues(
                EmailPromotionViewModel.AWAndPartnerPromotions
            );

            //Assert
            list.Count.Should().Be(3);
            list[0].Should().Be(EmailPromotionViewModel.NoPromotions);
            list[1].Should().Be(EmailPromotionViewModel.AWPromotions);
            list[2].Should().Be(EmailPromotionViewModel.AWAndPartnerPromotions);
        }

        [Fact]
        public void Parse_ReturnsEnumList()
        {
            //Act
            var result = EnumHelper<EmailPromotionViewModel>.Parse(
                "AWPromotions"
            );

            //Assert
            result.Should().Be(EmailPromotionViewModel.AWPromotions);
        }

        [Fact]
        public void GetNames_ReturnsList()
        {
            //Act
            var list = EnumHelper<EmailPromotionViewModel>.GetNames(
                EmailPromotionViewModel.AWAndPartnerPromotions
            );

            //Assert
            list.Count.Should().Be(3);
            list[0].Should().Be("NoPromotions");
            list[1].Should().Be("AWPromotions");
            list[2].Should().Be("AWAndPartnerPromotions");
        }

        [Fact]
        public void GetDisplayValues_ReturnsList()
        {
            //Act
            var list = EnumHelper<EmailPromotionViewModel>.GetDisplayValues(
                EmailPromotionViewModel.AWAndPartnerPromotions
            );

            //Assert
            list.Count.Should().Be(3);
            list[0].Should().Be("Contact does not wish to receive e-mail promotions");
            list[1].Should().Be("Contact does wish to receive e-mail promotions from AdventureWorks");
            list[2].Should().Be("Contact does wish to receive e-mail promotions from AdventureWorks and selected partners");
        }

        [Fact]
        public void GetDisplayValue_ReturnsList()
        {
            //Act
            var result = EnumHelper<EmailPromotionViewModel>.GetDisplayValue(
                EmailPromotionViewModel.AWPromotions
            );

            //Assert
            result.Should().Be("Contact does wish to receive e-mail promotions from AdventureWorks");            
        }
    }
}