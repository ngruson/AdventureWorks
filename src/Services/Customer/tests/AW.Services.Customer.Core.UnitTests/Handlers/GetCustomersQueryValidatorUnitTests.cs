using AW.Services.Customer.Core.Handlers.GetCustomers;
using AW.SharedKernel.UnitTesting;
using FluentValidation.TestHelper;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests.Handlers
{
    public class GetCustomersQueryValidatorUnitTests
    {
        [Theory]
        [AutoMoqData]
        public void TestValidate_ValidQuery_NoValidationError(
            GetCustomersQueryValidator sut,
            GetCustomersQuery query
        )
        {
            //Act
            var result = sut.TestValidate(query);
            result.ShouldNotHaveValidationErrorFor(query => query.PageIndex);
            result.ShouldNotHaveValidationErrorFor(query => query.PageSize);
        }

        [Theory]
        [AutoMoqData]
        public void TestValidate_InvalidPageIndex_ValidationErrorForPageIndex(
            GetCustomersQueryValidator sut,
            GetCustomersQuery query
        )
        {
            //Arrange
            query.PageIndex = -1;

            var result = sut.TestValidate(query);
            result.ShouldHaveValidationErrorFor(query => query.PageIndex);
        }

        [Theory]
        [AutoMoqData]
        public void TestValidate_InvalidPageSize_ValidationErrorForPageSize(
            GetCustomersQueryValidator sut,
            GetCustomersQuery query
        )
        {
            //Arrange
            query.PageSize = 0;

            var result = sut.TestValidate(query);
            result.ShouldHaveValidationErrorFor(query => query.PageSize);
        }
    }
}