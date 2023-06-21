using AW.Services.Customer.Core.Handlers.DeleteCustomer;
using AW.Services.Customer.Core.Handlers.GetPreferredAddress;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests.Handlers
{
    public class GetPreferredAddressQueryValidatorUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async Task no_validation_errors_given_valid_command(
            GetPreferredAddressQueryValidator sut,
            GetPreferredAddressQuery query
        )
        {
            //Arrange

            //Act
            var result = await sut.TestValidateAsync(query);

            //Assert
            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_customerid(
            GetPreferredAddressQueryValidator sut,
            string addressType
        )
        {
            //Arrange
            var query = new GetPreferredAddressQuery(
                Guid.Empty,
                addressType
            );

            //Act
            var result = await sut.TestValidateAsync(query);

            //Assert
            result.ShouldHaveValidationErrorFor(_ => _.CustomerId);
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_addresstype(
            GetPreferredAddressQueryValidator sut,
            Guid customerId
        )
        {
            //Arrange
            var query = new GetPreferredAddressQuery(
                customerId,
                string.Empty
            );

            //Act
            var result = await sut.TestValidateAsync(query);

            //Assert
            result.ShouldHaveValidationErrorFor(_ => _.AddressType);
        }
    }
}
