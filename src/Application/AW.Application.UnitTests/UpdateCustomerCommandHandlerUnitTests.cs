using AW.Application.Customer;
using AW.Application.Customer.UpdateCustomer;
using Ardalis.Specification;
using AW.Application.Specifications;
using AW.Application.UnitTests.AutoMapper;
using AW.Application.UnitTests.TestBuilders;
using FluentAssertions;
using Moq;
using System.Threading;
using Xunit;

namespace AW.Application.UnitTests
{
    public class UpdateCustomerCommandHandlerUnitTests
    {
        [Fact]
        public async void Handle_Store_UpdateCustomer()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var customer = new CustomerBuilder().WithTestValues().Build();
            var salesTerritory = new SalesTerritoryBuilder().WithTestValues().Build();
            var salesPerson = new SalesPersonBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var salesTerritoryRepoMock = new Mock<IRepositoryBase<Domain.Sales.SalesTerritory>>();
            salesTerritoryRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetSalesTerritorySpecification>()))
                .ReturnsAsync(salesTerritory);

            var salesPersonRepoMock = new Mock<IRepositoryBase<Domain.Sales.SalesPerson>>();
            salesPersonRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetSalesPersonSpecification>()))
                .ReturnsAsync(salesPerson);

            var handler = new UpdateCustomerCommandHandler(
                customerRepoMock.Object,
                salesTerritoryRepoMock.Object,
                salesPersonRepoMock.Object,
                mapper
            );

            //Act
            var command = new UpdateCustomerCommand
            {
                Customer = mapper.Map<CustomerDto>(customer)
            };

            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            customerRepoMock.Verify(x => x.UpdateAsync(It.IsAny<Domain.Sales.Customer>()));
        }

        [Fact]
        public async void Handle_Person_UpdateCustomer()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var customer = new CustomerBuilder()
                .WithTestValues()
                .Person(new PersonBuilder().WithTestValues().Build())
                .Build();
            var salesTerritory = new SalesTerritoryBuilder().WithTestValues().Build();
            var salesPerson = new SalesPersonBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var salesTerritoryRepoMock = new Mock<IRepositoryBase<Domain.Sales.SalesTerritory>>();
            salesTerritoryRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetSalesTerritorySpecification>()))
                .ReturnsAsync(salesTerritory);

            var salesPersonRepoMock = new Mock<IRepositoryBase<Domain.Sales.SalesPerson>>();
            salesPersonRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetSalesPersonSpecification>()))
                .ReturnsAsync(salesPerson);

            var handler = new UpdateCustomerCommandHandler(
                customerRepoMock.Object,
                salesTerritoryRepoMock.Object,
                salesPersonRepoMock.Object,
                mapper
            );

            //Act
            var command = new UpdateCustomerCommand
            {
                Customer = mapper.Map<CustomerDto>(customer)
            };

            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            customerRepoMock.Verify(x => x.UpdateAsync(It.IsAny<Domain.Sales.Customer>()));
        }

        [Fact]
        public async void Handle_Store_SetSalesTerritory()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var customer = new CustomerBuilder().WithTestValues().SalesTerritory(null).Build();
            var salesTerritory = new SalesTerritoryBuilder().WithTestValues().Build();
            var salesPerson = new SalesPersonBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var salesTerritoryRepoMock = new Mock<IRepositoryBase<Domain.Sales.SalesTerritory>>();
            salesTerritoryRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetSalesTerritorySpecification>()))
                .ReturnsAsync(salesTerritory);

            var salesPersonRepoMock = new Mock<IRepositoryBase<Domain.Sales.SalesPerson>>();
            salesPersonRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetSalesPersonSpecification>()))
                .ReturnsAsync(salesPerson);

            var handler = new UpdateCustomerCommandHandler(
                customerRepoMock.Object,
                salesTerritoryRepoMock.Object,
                salesPersonRepoMock.Object,
                mapper
            );

            //Act
            var command = new UpdateCustomerCommand
            {
                Customer = mapper.Map<CustomerDto>(customer)
            };
            command.Customer.SalesTerritoryName = salesTerritory.Name;

            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            customer.SalesTerritoryID.Should().Be(salesTerritory.Id);
        }

        [Fact]
        public async void Handle_Person_SetSalesTerritory()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var customer = new CustomerBuilder()
                .WithTestValues()
                .Person(new PersonBuilder().WithTestValues().Build())
                .SalesTerritory(null)
                .Build();
            var salesTerritory = new SalesTerritoryBuilder().WithTestValues().Build();
            var salesPerson = new SalesPersonBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var salesTerritoryRepoMock = new Mock<IRepositoryBase<Domain.Sales.SalesTerritory>>();
            salesTerritoryRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetSalesTerritorySpecification>()))
                .ReturnsAsync(salesTerritory);

            var salesPersonRepoMock = new Mock<IRepositoryBase<Domain.Sales.SalesPerson>>();
            salesPersonRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetSalesPersonSpecification>()))
                .ReturnsAsync(salesPerson);

            var handler = new UpdateCustomerCommandHandler(
                customerRepoMock.Object,
                salesTerritoryRepoMock.Object,
                salesPersonRepoMock.Object,
                mapper
            );

            //Act
            var command = new UpdateCustomerCommand
            {
                Customer = mapper.Map<CustomerDto>(customer)
            };
            command.Customer.SalesTerritoryName = salesTerritory.Name;

            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            customer.SalesTerritoryID.Should().Be(salesTerritory.Id);
        }

        [Fact]
        public async void Handle_Store_ClearSalesTerritory()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var customer = new CustomerBuilder().WithTestValues().Build();
            var salesTerritory = new SalesTerritoryBuilder().WithTestValues().Build();
            var salesPerson = new SalesPersonBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var salesTerritoryRepoMock = new Mock<IRepositoryBase<Domain.Sales.SalesTerritory>>();
            salesTerritoryRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetSalesTerritorySpecification>()))
                .ReturnsAsync(salesTerritory);

            var salesPersonRepoMock = new Mock<IRepositoryBase<Domain.Sales.SalesPerson>>();
            salesPersonRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetSalesPersonSpecification>()))
                .ReturnsAsync(salesPerson);

            var handler = new UpdateCustomerCommandHandler(
                customerRepoMock.Object,
                salesTerritoryRepoMock.Object,
                salesPersonRepoMock.Object,
                mapper
            );

            //Act
            var command = new UpdateCustomerCommand
            {
                Customer = mapper.Map<CustomerDto>(customer)
            };
            command.Customer.SalesTerritoryName = "";

            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            customer.SalesTerritory.Should().BeNull();
        }

        [Fact]
        public async void Handle_Person_ClearSalesTerritory()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var customer = new CustomerBuilder()
                .WithTestValues()
                .Person(new PersonBuilder().WithTestValues().Build())
                .Build();
            var salesTerritory = new SalesTerritoryBuilder().WithTestValues().Build();
            var salesPerson = new SalesPersonBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var salesTerritoryRepoMock = new Mock<IRepositoryBase<Domain.Sales.SalesTerritory>>();
            salesTerritoryRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetSalesTerritorySpecification>()))
                .ReturnsAsync(salesTerritory);

            var salesPersonRepoMock = new Mock<IRepositoryBase<Domain.Sales.SalesPerson>>();
            salesPersonRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetSalesPersonSpecification>()))
                .ReturnsAsync(salesPerson);

            var handler = new UpdateCustomerCommandHandler(
                customerRepoMock.Object,
                salesTerritoryRepoMock.Object,
                salesPersonRepoMock.Object,
                mapper
            );

            //Act
            var command = new UpdateCustomerCommand
            {
                Customer = mapper.Map<CustomerDto>(customer)
            };
            command.Customer.SalesTerritoryName = "";

            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            customer.SalesTerritory.Should().BeNull();
        }

        [Fact]
        public async void Handle_Store_SetSalesPerson()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var customer = new CustomerBuilder()
                .WithTestValues()
                .Store(new StoreBuilder().WithTestValues().SalesPerson(null).Build())
                .Build();
            var salesTerritory = new SalesTerritoryBuilder().WithTestValues().Build();
            var salesPerson = new SalesPersonBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var salesTerritoryRepoMock = new Mock<IRepositoryBase<Domain.Sales.SalesTerritory>>();
            salesTerritoryRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetSalesTerritorySpecification>()))
                .ReturnsAsync(salesTerritory);

            var salesPersonRepoMock = new Mock<IRepositoryBase<Domain.Sales.SalesPerson>>();
            salesPersonRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetSalesPersonSpecification>()))
                .ReturnsAsync(salesPerson);

            var handler = new UpdateCustomerCommandHandler(
                customerRepoMock.Object,
                salesTerritoryRepoMock.Object,
                salesPersonRepoMock.Object,
                mapper
            );

            //Act
            var command = new UpdateCustomerCommand
            {
                Customer = mapper.Map<CustomerDto>(customer)
            };

            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            customer.Store.SalesPerson.Should().BeNull();
        }

        [Fact]
        public async void Handle_Store_ClearSalesPerson()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var customer = new CustomerBuilder().WithTestValues().Build();
            var salesTerritory = new SalesTerritoryBuilder().WithTestValues().Build();
            var salesPerson = new SalesPersonBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var salesTerritoryRepoMock = new Mock<IRepositoryBase<Domain.Sales.SalesTerritory>>();
            salesTerritoryRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetSalesTerritorySpecification>()))
                .ReturnsAsync(salesTerritory);

            var salesPersonRepoMock = new Mock<IRepositoryBase<Domain.Sales.SalesPerson>>();
            salesPersonRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetSalesPersonSpecification>()))
                .ReturnsAsync(salesPerson);

            var handler = new UpdateCustomerCommandHandler(
                customerRepoMock.Object,
                salesTerritoryRepoMock.Object,
                salesPersonRepoMock.Object,
                mapper
            );

            //Act
            var command = new UpdateCustomerCommand
            {
                Customer = mapper.Map<CustomerDto>(customer)
            };
            command.Customer.Store.SalesPerson = null;

            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            customer.Store.SalesPerson.Should().BeNull();
        }
    }
}