using Ardalis.Specification;
using AutoFixture.Xunit2;
using AW.Services.SalesOrder.Core.Handlers.GetSalesOrder;
using AW.Services.SalesOrder.Core.Specifications;
using AW.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentValidation.TestHelper;
using Moq;
using System.Threading;
using Xunit;

namespace AW.Services.SalesOrder.Core.UnitTests
{
    public class GetSalesOrderQueryValidatorUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public void TestValidate_WithValidSalesOrderNumber_NoValidationError(            
            GetSalesOrderQueryValidator sut,
            GetSalesOrderQuery query
        )
        {
            //Arrange
            query.SalesOrderNumber = query.SalesOrderNumber.Substring(0, 25);

            //Act
            var result = sut.TestValidate(query);

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
        public void TestValidate_WithSalesOrderNumberDoesNotExist_ValidationError(
            [Frozen] Mock<IRepository<Entities.SalesOrder>> salesOrderRepoMock,
            GetSalesOrderQueryValidator sut,
            GetSalesOrderQuery query
        )
        {
            //Arrange
            salesOrderRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetSalesOrderSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.SalesOrder)null);

            //Act
            var result = sut.TestValidate(query);

            //Assert
            result.ShouldHaveValidationErrorFor(query => query.SalesOrderNumber)
                .WithErrorMessage("Sales order does not exist");
        }
    }
}