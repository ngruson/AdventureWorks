using Ardalis.Specification;
using AW.Application.SalesOrder.GetSalesOrder;
using AW.Application.Specifications;
using AW.Application.UnitTests.TestBuilders;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace AW.Application.UnitTests
{
    public class GetSalesOrderQueryValidatorUnitTests
    {
        [Fact]
        public void SalesOrderNumber_Empty_ValidationError()
        {
            var salesOrder = new SalesOrderBuilder().WithTestValues().Build();

            var salesOrderRepoMock = new Mock<IRepositoryBase<Domain.Sales.SalesOrderHeader>>();
            salesOrderRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetSalesOrderSpecification>()))
                .ReturnsAsync(salesOrder);

            var validator = new GetSalesOrderQueryValidator(
                salesOrderRepoMock.Object
            );

            var query = new GetSalesOrderQuery();
            validator.ShouldHaveValidationErrorFor(x => x.SalesOrderNumber, query);
        }

        [Fact]
        public void SalesOrderNumber_TooLong_ValidationError()
        {
            var salesOrder = new SalesOrderBuilder().WithTestValues().Build();

            var salesOrderRepoMock = new Mock<IRepositoryBase<Domain.Sales.SalesOrderHeader>>();
            salesOrderRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetSalesOrderSpecification>()))
                .ReturnsAsync(salesOrder);

            var validator = new GetSalesOrderQueryValidator(
                salesOrderRepoMock.Object
            );

            var query = new GetSalesOrderQuery
            {
                SalesOrderNumber = "a".PadRight(26)
            };
            validator.ShouldHaveValidationErrorFor(x => x.SalesOrderNumber, query);
        }

        [Fact]
        public void SalesOrder_DoesNotExist_ValidationError()
        {
            var salesOrderRepoMock = new Mock<IRepositoryBase<Domain.Sales.SalesOrderHeader>>();
            var validator = new GetSalesOrderQueryValidator(salesOrderRepoMock.Object);

            var query = new GetSalesOrderQuery();
            validator.ShouldHaveValidationErrorFor(x => x.SalesOrderNumber, query);
        }
    }
}