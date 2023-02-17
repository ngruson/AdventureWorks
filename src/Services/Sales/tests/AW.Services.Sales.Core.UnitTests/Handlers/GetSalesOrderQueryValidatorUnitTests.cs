using AutoFixture.Xunit2;
using AW.Services.Sales.Core.AutoMapper;
using AW.Services.Sales.Core.Handlers.GetSalesOrder;
using AW.Services.Sales.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace AW.Services.Sales.Core.UnitTests.Handlers
{
    public class GetSalesOrderQueryValidatorUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task TestValidate_WithValidSalesOrderNumber_NoValidationError(
            GetSalesOrderQueryValidator sut,
            GetSalesOrderQuery query
        )
        {
            //Arrange
            query.SalesOrderNumber = query.SalesOrderNumber?.Substring(0, 25);

            //Act
            var result = await sut.TestValidateAsync(query);

            //Assert
            result.ShouldNotHaveValidationErrorFor(query => query.SalesOrderNumber);
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public void TestValidate_WithoutSalesOrderNumber_ValidationError(
            GetSalesOrderQueryValidator sut,
            GetSalesOrderQuery query
        )
        {
            //Arrange
            query.SalesOrderNumber = "";

            //Act
            var result = sut.TestValidate(query);

            //Assert
            result.ShouldHaveValidationErrorFor(query => query.SalesOrderNumber)
                .WithErrorMessage("Sales order number is required");
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public void TestValidate_WithSalesOrderNumberTooLong_ValidationError(
            GetSalesOrderQueryValidator sut,
            GetSalesOrderQuery query
        )
        {
            //Act
            var result = sut.TestValidate(query);

            //Assert
            result.ShouldHaveValidationErrorFor(query => query.SalesOrderNumber)
                .WithErrorMessage("Sales order number must not exceed 25 characters");
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task TestValidate_WithSalesOrderNumberDoesNotExist_ValidationError(
            [Frozen] Mock<IRepository<Core.Entities.SalesOrder>> salesOrderRepoMock,
            GetSalesOrderQueryValidator sut,
            GetSalesOrderQuery query
        )
        {
            //Arrange
            query.SalesOrderNumber = query.SalesOrderNumber?.Substring(0, 25);
            salesOrderRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetFullSalesOrderSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Core.Entities.SalesOrder?)null);

            //Act
            var result = await sut.TestValidateAsync(query);

            //Assert
            result.ShouldHaveValidationErrorFor(query => query.SalesOrderNumber)
                .WithErrorMessage("Sales order does not exist");
        }
    }
}
