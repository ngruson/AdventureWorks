using AutoMapper;
using AW.Application.Customer.AddCustomerAddress;
using AW.Application.Customer.GetCustomer;
using AW.Application.Customer.GetCustomers;
using AW.Application.Customer.UpdateCustomer;
using AW.CustomerService.Messages.AddCustomerAddress;
using AW.CustomerService.Messages.AddCustomerContact;
using AW.CustomerService.Messages.AddCustomerContactInfo;
using AW.CustomerService.Messages.DeleteCustomerAddress;
using AW.CustomerService.Messages.DeleteCustomerContact;
using AW.CustomerService.Messages.DeleteCustomerContactInfo;
using AW.CustomerService.Messages.GetCustomer;
using AW.CustomerService.Messages.ListCustomers;
using AW.CustomerService.Messages.UpdateCustomer;
using AW.CustomerService.Messages.UpdateCustomerAddress;
using AW.CustomerService.Messages.UpdateCustomerContact;
using FluentAssertions;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AW.CustomerService.UnitTests
{
    [TestClass]
    public class CustomerServiceUnitTests
    {
        [TestMethod]
        public async Task ListCustomers_ReturnsCustomers()
        {
            //Arrange
            var mapper = new MapperConfiguration(opts =>
            {
                opts.AddProfile<MappingProfile>();
            }).CreateMapper();

            var dto = new GetCustomersDto
            {
                Customers = new List<Application.Customer.GetCustomers.CustomerDto>
                {
                    new Application.Customer.GetCustomers.CustomerDto { AccountNumber = "AW00000001" },
                    new Application.Customer.GetCustomers.CustomerDto { AccountNumber = "AW00000002" }
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
        }

        [TestMethod]
        public async Task GetCustomer_ReturnsCustomer()
        {
            //Arrange
            var mapper = new MapperConfiguration(opts =>
            {
                opts.AddProfile<MappingProfile>();
            }).CreateMapper();

            var dto = new Application.Customer.GetCustomer.CustomerDto
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

        [TestMethod]
        public async Task UpdateCustomer_ReturnsCustomer()
        {
            //Arrange
            var mapper = new MapperConfiguration(opts =>
            {
                opts.AddProfile<MappingProfile>();
            }).CreateMapper();

            var dto = new Application.Customer.UpdateCustomer.CustomerDto
            {
                AccountNumber = "AW00000001"
            };

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<UpdateCustomerCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(dto);
            var customerService = new CustomerService(
                mockMediator.Object,
                mapper
            );

            //Act
            var request = new UpdateCustomerRequest();
            var result = await customerService.UpdateCustomer(request);

            //Assert
            result.Should().NotBeNull();
            result.Customer.Should().NotBeNull();
            result.Customer.AccountNumber.Should().Be("AW00000001");
        }

        [TestMethod]
        public async Task AddCustomerAddress_ReturnsResponse()
        {
            //Arrange
            var mapper = new MapperConfiguration(opts =>
            {
                opts.AddProfile<MappingProfile>();
            }).CreateMapper();

            var dto = new Application.Customer.UpdateCustomer.CustomerDto
            {
                AccountNumber = "AW00000001"
            };

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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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
            var request = new AddCustomerContactRequest();
            var result = await customerService.AddCustomerContact(request);

            //Assert
            result.Should().NotBeNull();
        }

        [TestMethod]
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
            var request = new UpdateCustomerContactRequest();
            var result = await customerService.UpdateCustomerContact(request);

            //Assert
            result.Should().NotBeNull();
        }

        [TestMethod]
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
            var request = new DeleteCustomerContactRequest();
            var result = await customerService.DeleteCustomerContact(request);

            //Assert
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task AddCustomerContactInfo_ReturnsResponse()
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
            var request = new AddCustomerContactInfoRequest();
            var result = await customerService.AddCustomerContactInfo(request);

            //Assert
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task DeleteCustomerContactInfo_ReturnsResponse()
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
            var request = new DeleteCustomerContactInfoRequest();
            var result = await customerService.DeleteCustomerContactInfo(request);

            //Assert
            result.Should().NotBeNull();
        }
    }
}