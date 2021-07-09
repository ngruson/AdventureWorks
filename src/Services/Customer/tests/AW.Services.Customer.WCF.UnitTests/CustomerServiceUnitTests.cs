using AutoMapper;
using AW.Services.Customer.Core.Handlers.GetCustomer;
using AW.Services.Customer.Core.Handlers.GetCustomers;
using AW.Services.Customer.Core.Handlers.UpdateCustomer;
using AW.Services.Customer.WCF.Messages.AddCustomerAddress;
using AW.Services.Customer.WCF.Messages.AddIndividualCustomerEmailAddress;
using AW.Services.Customer.WCF.Messages.AddStoreCustomerContact;
using AW.Services.Customer.WCF.Messages.DeleteCustomerAddress;
using AW.Services.Customer.WCF.Messages.DeleteIndividualCustomerEmailAddress;
using AW.Services.Customer.WCF.Messages.DeleteStoreCustomerContact;
using AW.Services.Customer.WCF.Messages.GetCustomer;
using AW.Services.Customer.WCF.Messages.ListCustomers;
using AW.Services.Customer.WCF.Messages.UpdateCustomer;
using AW.Services.Customer.WCF.Messages.UpdateCustomerAddress;
using AW.Services.Customer.WCF.Messages.UpdateStoreCustomerContact;
using FluentAssertions;
using MediatR;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Customer.WCF.UnitTests
{
    public class CustomerServiceUnitTests
    {
        [Fact]
        public async Task ListCustomers_ReturnsCustomers()
        {
            //Arrange
            var mapper = new MapperConfiguration(opts =>
            {
                opts.AddProfile<MappingProfile>();
            }).CreateMapper();

            var dto = new GetCustomersDto
            {
                Customers = new List<Core.Handlers.GetCustomers.CustomerDto>
                {
                    new TestBuilders.GetCustomers.IndividualCustomerBuilder().WithTestValues().Build(),
                    new TestBuilders.GetCustomers.StoreCustomerBuilder().WithTestValues().Build()
                },
                TotalCustomers = 2
            };

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<GetCustomersQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(dto);
            var customerService = new CustomerService(
                mockMediator.Object,
                mapper
            );

            //Act
            var request = new ListCustomersRequest();
            var result = await customerService.ListCustomers(request);

            //Assert
            result.Should().NotBeNull();
            result.Customers.Customer.Count().Should().Be(2);
            result.Customers.Customer[0].AccountNumber.Should().Be("AW00011000"); 
            result.Customers.Customer[1].AccountNumber.Should().Be("AW00000001");
        }

        [Fact]
        public async Task GetCustomer_ReturnsCustomer()
        {
            //Arrange
            var mapper = new MapperConfiguration(opts =>
            {
                opts.AddProfile<MappingProfile>();
            }).CreateMapper();

            var dto = new Core.Handlers.GetCustomer.IndividualCustomerDto
            {
                AccountNumber = "AW00000001"
            };

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<GetCustomerQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(dto);
            var customerService = new CustomerService(
                mockMediator.Object,
                mapper
            );

            //Act
            var request = new GetCustomerRequest();
            var result = await customerService.GetCustomer(request);

            //Assert
            result.Should().NotBeNull();
            result.Customer.Should().NotBeNull();
            result.Customer.AccountNumber.Should().Be("AW00000001");
        }

        [Fact]
        public async Task UpdateCustomer_ReturnsCustomer()
        {
            //Arrange
            var mapper = new MapperConfiguration(opts =>
            {
                opts.AddProfile<MappingProfile>();
            }).CreateMapper();

            var customer = new Core.Handlers.UpdateCustomer.IndividualCustomerDto
            {
                AccountNumber = "AW00000001"
            };

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<UpdateCustomerCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(customer);
            var customerService = new CustomerService(
                mockMediator.Object,
                mapper
            );

            //Act
            var request = new UpdateCustomerRequest
            {
                Customer = new Messages.UpdateCustomer.IndividualCustomer
                {
                    AccountNumber = "AW00000001"
                }
            };
            var result = await customerService.UpdateCustomer(request);

            //Assert
            result.Should().NotBeNull();
            result.Customer.Should().NotBeNull();
            result.Customer.AccountNumber.Should().Be("AW00000001");
        }

        [Fact]
        public async Task AddCustomerAddress_ReturnsResponse()
        {
            //Arrange
            var mapper = new MapperConfiguration(opts =>
            {
                opts.AddProfile<MappingProfile>();
            }).CreateMapper();

            var mockMediator = new Mock<IMediator>();
            var customerService = new CustomerService(
                mockMediator.Object,
                mapper
            );

            //Act
            var request = new AddCustomerAddressRequest();
            var result = await customerService.AddCustomerAddress(request);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task UpdateCustomerAddress_ReturnsResponse()
        {
            //Arrange
            var mapper = new MapperConfiguration(opts =>
            {
                opts.AddProfile<MappingProfile>();
            }).CreateMapper();

            var mockMediator = new Mock<IMediator>();
            var customerService = new CustomerService(
                mockMediator.Object,
                mapper
            );

            //Act
            var request = new UpdateCustomerAddressRequest();
            var result = await customerService.UpdateCustomerAddress(request);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task DeleteCustomerAddress_ReturnsResponse()
        {
            //Arrange
            var mapper = new MapperConfiguration(opts =>
            {
                opts.AddProfile<MappingProfile>();
            }).CreateMapper();

            var mockMediator = new Mock<IMediator>();
            var customerService = new CustomerService(
                mockMediator.Object,
                mapper
            );

            //Act
            var request = new DeleteCustomerAddressRequest();
            var result = await customerService.DeleteCustomerAddress(request);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task AddCustomerContact_ReturnsResponse()
        {
            //Arrange
            var mapper = new MapperConfiguration(opts =>
            {
                opts.AddProfile<MappingProfile>();
            }).CreateMapper();

            var mockMediator = new Mock<IMediator>();
            var customerService = new CustomerService(
                mockMediator.Object,
                mapper
            );

            //Act
            var request = new AddStoreCustomerContactRequest();
            var result = await customerService.AddStoreCustomerContact(request);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task UpdateCustomerContact_ReturnsResponse()
        {
            //Arrange
            var mapper = new MapperConfiguration(opts =>
            {
                opts.AddProfile<MappingProfile>();
            }).CreateMapper();

            var mockMediator = new Mock<IMediator>();
            var customerService = new CustomerService(
                mockMediator.Object,
                mapper
            );

            //Act
            var request = new UpdateStoreCustomerContactRequest();
            var result = await customerService.UpdateStoreCustomerContact(request);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task DeleteCustomerContact_ReturnsResponse()
        {
            //Arrange
            var mapper = new MapperConfiguration(opts =>
            {
                opts.AddProfile<MappingProfile>();
            }).CreateMapper();

            var mockMediator = new Mock<IMediator>();
            var customerService = new CustomerService(
                mockMediator.Object,
                mapper
            );

            //Act
            var request = new DeleteStoreCustomerContactRequest();
            var result = await customerService.DeleteStoreCustomerContact(request);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task AddIndividualCustomerEmailAddress_ReturnsResponse()
        {
            //Arrange
            var mapper = new MapperConfiguration(opts =>
            {
                opts.AddProfile<MappingProfile>();
            }).CreateMapper();

            var mockMediator = new Mock<IMediator>();
            var customerService = new CustomerService(
                mockMediator.Object,
                mapper
            );

            //Act
            var request = new AddIndividualCustomerEmailAddressRequest();
            var result = await customerService.AddIndividualCustomerEmailAddress(request);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task DeleteIndividualCustomerEmailAddress_ReturnsResponse()
        {
            //Arrange
            var mapper = new MapperConfiguration(opts =>
            {
                opts.AddProfile<MappingProfile>();
            }).CreateMapper();

            var mockMediator = new Mock<IMediator>();
            var customerService = new CustomerService(
                mockMediator.Object,
                mapper
            );

            //Act
            var request = new DeleteIndividualCustomerEmailAddressRequest();
            var result = await customerService.DeleteIndividualCustomerEmailAddress(request);

            //Assert
            result.Should().NotBeNull();
        }
    }
}