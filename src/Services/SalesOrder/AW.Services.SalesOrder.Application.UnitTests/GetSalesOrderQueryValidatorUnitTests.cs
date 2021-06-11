using Ardalis.Specification;
using AW.Services.SalesOrder.Application.GetSalesOrder;
using AW.Services.SalesOrder.Application.Specifications;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace AW.Services.SalesOrder.Application.UnitTests
{
    public class GetSalesOrderQueryValidatorUnitTests
    {
        [Fact]
        public void TestValidate_WithValidSalesOrderNumber_NoValidationError()
        {
            //Arrange
            var mockRepository = new Mock<IRepositoryBase<Domain.SalesOrder>>();
            mockRepository.Setup(x => x.GetBySpecAsync(It.IsAny<GetSalesOrderSpecification>()))
                .ReturnsAsync(new Domain.SalesOrder { SalesOrderNumber = "SO43659" });

            var sut = new GetSalesOrderQueryValidator(mockRepository.Object);
            var query = new GetSalesOrderQuery
            {
                SalesOrderNumber = "SO43659"
            };

            //Act
            var result = sut.TestValidate(query);

            //Assert
            result.ShouldNotHaveValidationErrorFor(query => query.SalesOrderNumber);
        }

        [Fact]
        public void TestValidate_WithoutSalesOrderNumber_ValidationError()
        {
            //Arrange
            var mockRepository = new Mock<IRepositoryBase<Domain.SalesOrder>>();
            var sut = new GetSalesOrderQueryValidator(mockRepository.Object);
            var query = new GetSalesOrderQuery();
           
            //Act
            var result = sut.TestValidate(query);

            //Assert
            result.ShouldHaveValidationErrorFor(query => query.SalesOrderNumber)
                .WithErrorMessage("Sales order number is required");
        }

        [Fact]
        public void TestValidate_WithSalesOrderNumberTooLong_ValidationError()
        {
            //Arrange
            var mockRepository = new Mock<IRepositoryBase<Domain.SalesOrder>>();
            mockRepository.Setup(x => x.GetBySpecAsync(It.IsAny<GetSalesOrderSpecification>()))
                .ReturnsAsync(new Domain.SalesOrder { SalesOrderNumber = "SO43659" });

            var sut = new GetSalesOrderQueryValidator(mockRepository.Object);
            var query = new GetSalesOrderQuery
            {
                SalesOrderNumber = "1".PadRight(26)
            };

            var result = sut.TestValidate(query);
            result.ShouldHaveValidationErrorFor(query => query.SalesOrderNumber)
                .WithErrorMessage("Sales order number must not exceed 25 characters");
        }

        [Fact]
        public void TestValidate_WithSalesOrderNumberDoesNotExist_ValidationError()
        {
            //Arrange
            var mockRepository = new Mock<IRepositoryBase<Domain.SalesOrder>>();
            var sut = new GetSalesOrderQueryValidator(mockRepository.Object);
            var query = new GetSalesOrderQuery
            {
                SalesOrderNumber = "SO43659"
            };

            var result = sut.TestValidate(query);
            result.ShouldHaveValidationErrorFor(query => query.SalesOrderNumber)
                .WithErrorMessage("Sales order does not exist");
        }
    }
}