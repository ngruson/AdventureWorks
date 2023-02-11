using Ardalis.Specification;
using AutoFixture.Xunit2;
using AW.Services.Sales.Core.Specifications;
using AW.Services.SharedKernel.EFCore;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace AW.Services.Sales.Infrastructure.EFCore.UnitTests
{
    public class EfRepositoryUnitTests
    {
        [Theory, OmitOnRecursion]
        public async Task GetByIdAsync_ReturnsObject(
            [Frozen] Mock<DbSet<Core.Entities.SalesOrder>> mockSet,
            [Frozen] Mock<AWContext> mockContext,
            string salesOrderNumber
        )
        {
            //Arrange
            var salesOrder = new Core.Entities.SalesOrder(salesOrderNumber);
            mockSet.Setup(x => x.FindAsync(It.IsAny<object[]>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(salesOrder);

            mockContext.Setup(x => x.Set<Core.Entities.SalesOrder>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.SalesOrder>(mockContext.Object);

            //Act
            var result = await repository.GetByIdAsync(1);

            //Assert
            result?.SalesOrderNumber.Should().Be(salesOrder.SalesOrderNumber);
        }

        [Theory, OmitOnRecursion]
        public async Task SingleOrDefaultAsync_ReturnsObject(
            [Frozen] Mock<AWContext> mockContext,
            List<string> salesOrderNumbers
        )
        {
            //Arrange
            var salesOrders = salesOrderNumbers
                .Select(salesOrderNumber => new Core.Entities.SalesOrder(salesOrderNumber))
                .ToList();
            
            var mockSet = salesOrders.AsQueryable().BuildMockDbSet();

            mockContext.Setup(x => x.Set<Core.Entities.SalesOrder>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.SalesOrder>(mockContext.Object);

            //Act
            var spec = new GetFullSalesOrderSpecification(salesOrders[0].SalesOrderNumber!);
            var result = await repository.SingleOrDefaultAsync(spec);

            //Assert
            result?.SalesOrderNumber.Should().Be(salesOrders[0].SalesOrderNumber);
        }

        [Theory, OmitOnRecursion]
        public async Task ListAllAsync_ReturnsObjects(
            [Frozen] Mock<AWContext> mockContext,
            List<Core.Entities.SalesOrder> salesOrders
        )
        {
            //Arrange
            var mockSet = salesOrders.AsQueryable().BuildMockDbSet();

            mockContext.Setup(x => x.Set<Core.Entities.SalesOrder>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.SalesOrder>(mockContext.Object);

            //Act
            var list = await repository.ListAsync();

            //Assert
            list.Count.Should().Be(salesOrders.Count);
        }

        [Theory, OmitOnRecursion]
        public async Task ListAsync_ReturnsObjects(
            [Frozen] Mock<AWContext> mockContext,
            string userId,
            string userName,
            Core.Entities.Customer customer,
            string shipMethod,
            Core.ValueTypes.Address address,
            Core.Entities.CreditCard creditCard,
            string cardSecurityNumber,
            string cardHolderName
        )
        {
            //Arrange
            creditCard.ExpYear = short.Parse(DateTime.Today.Year.ToString());
            creditCard.ExpMonth = byte.Parse(DateTime.Today.Month.ToString());

            var salesOrders = new List<Core.Entities.SalesOrder>();
            int orderCount = 2;
            for (int i = 0; i < orderCount; i++)
            {
                salesOrders.Add(
                    new Core.Entities.SalesOrder(
                        userId,
                        userName,
                        null,
                        customer,
                        shipMethod,
                        address,
                        address,
                        creditCard,
                        cardSecurityNumber,
                        cardHolderName
                    )
                );
            }

            var mockSet = salesOrders.AsQueryable().BuildMockDbSet();

            mockContext.Setup(x => x.Set<Core.Entities.SalesOrder>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.SalesOrder>(mockContext.Object);

            //Act
            var spec = new GetSalesOrdersForCustomerSpecification(salesOrders[0].Customer!.CustomerNumber!);
            var list = await repository.ListAsync(spec);

            //Assert
            list.Count.Should().Be(orderCount);
        }

        [Theory, OmitOnRecursion]
        public async Task ListAsync_WithResultSpec_ReturnsObjects(
            [Frozen] Mock<AWContext> mockContext,
            List<Core.Entities.SalesOrder> salesOrders
        )
        {
            //Arrange
            var mockSet = salesOrders.AsQueryable().BuildMockDbSet();

            mockContext.Setup(x => x.Set<Core.Entities.SalesOrder>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.SalesOrder>(mockContext.Object);

            //Act
            var spec = new GetSalesOrdersSalesOrderNumberSpecification();
            var list = await repository.ListAsync(spec);

            //Assert
            list.Count.Should().Be(salesOrders.Count);
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Should().Be(salesOrders[i].SalesOrderNumber);
            }
        }

        //[Fact]
        //public void ListAsync_WithNullResultSpec_ThrowsArgumentNullException()
        //{
        //    //Arrange
        //    var mockContext = new Mock<AWContext>();
        //    var repository = new EfRepository<Core.Entities.SalesOrder>(mockContext.Object);

        //    //Act
        //    Func<Task> func = async () => await repository.ListAsync<string>(null);

        //    //Assert
        //    func.Should().ThrowAsync<ArgumentNullException>();
        //}

        [Fact]
        public void ListAsync_WithResultSpecWithoutSelector_ThrowsSelectorNotFoundException()
        {
            //Arrange
            var mockContext = new Mock<AWContext>();
            var repository = new EfRepository<Core.Entities.SalesOrder>(mockContext.Object);

            //Act
            var spec = new GetSalesOrdersWithoutSelectorSpecification();
            Func<Task> func = async () => await repository.ListAsync(spec);

            //Assert
            func.Should().ThrowAsync<SelectorNotFoundException>();
        }

        [Theory, OmitOnRecursion]
        public async Task CountAsync_ReturnsCount(
            [Frozen] Mock<AWContext> mockContext,
            string userId,
            string userName,
            Core.Entities.Customer customer,
            string shipMethod,
            Core.ValueTypes.Address address,
            Core.Entities.CreditCard creditCard,
            string cardSecurityNumber,
            string cardHolderName
        )
        {
            //Arrange
            creditCard.ExpYear = short.Parse(DateTime.Today.Year.ToString());
            creditCard.ExpMonth = byte.Parse(DateTime.Today.Month.ToString());

            var salesOrders = new List<Core.Entities.SalesOrder>();
            int orderCount = 2;
            for (int i = 0; i < orderCount; i++)
            {
                salesOrders.Add(
                    new Core.Entities.SalesOrder(
                        userId,
                        userName,
                        null,
                        customer,
                        shipMethod,
                        address,
                        address,
                        creditCard,
                        cardSecurityNumber,
                        cardHolderName
                    )
                );
            }

            var mockSet = salesOrders.AsQueryable().BuildMockDbSet();

            mockContext.Setup(x => x.Set<Core.Entities.SalesOrder>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.SalesOrder>(mockContext.Object);

            //Act
            var spec = new GetSalesOrdersForCustomerSpecification(salesOrders[0].Customer!.CustomerNumber!);
            var count = await repository.CountAsync(spec);

            //Assert
            count.Should().Be(orderCount);
        }

        [Theory, OmitOnRecursion]
        public async Task AddAsync_SavesObject(
            List<Core.Entities.SalesOrder> salesOrders,
            Core.Entities.SalesOrder newSalesOrder
        )
        {
            //Arrange
            var mockSet = salesOrders.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Core.Entities.SalesOrder>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.SalesOrder>(mockContext.Object);

            //Act
            var savedSalesOrder = await repository.AddAsync(newSalesOrder);

            //Assert
            mockSet.Verify(x => x.Add(It.IsAny<Core.Entities.SalesOrder>()));
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
            newSalesOrder.Should().BeEquivalentTo(savedSalesOrder);
        }

        [Theory, OmitOnRecursion]
        public async Task UpdateAsync_SavesObject(
            List<Core.Entities.SalesOrder> salesOrders
        )
        {
            //Arrange
            var mockSet = salesOrders.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Core.Entities.SalesOrder>())
                .Returns(mockSet.Object);
            mockContext.Setup(x => x.SetModified(It.IsAny<Core.Entities.SalesOrder>()));
            var repository = new EfRepository<Core.Entities.SalesOrder>(mockContext.Object);

            //Act
            await repository.UpdateAsync(salesOrders[0]);

            //Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Theory, OmitOnRecursion]
        public async Task DeleteAsync_ReturnsObject(
            List<Core.Entities.SalesOrder> salesOrders
        )
        {
            //Arrange
            var mockSet = salesOrders.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Core.Entities.SalesOrder>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.SalesOrder>(mockContext.Object);

            //Act
            await repository.DeleteAsync(salesOrders[0]);

            //Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Theory, OmitOnRecursion]
        public async Task DeleteRangeAsync_ReturnsObject(
            List<Core.Entities.SalesOrder> salesOrders
        )
        {
            //Arrange
            var mockSet = salesOrders.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Core.Entities.SalesOrder>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.SalesOrder>(mockContext.Object);

            //Act
            await repository.DeleteRangeAsync(salesOrders);

            //Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }
    }
}