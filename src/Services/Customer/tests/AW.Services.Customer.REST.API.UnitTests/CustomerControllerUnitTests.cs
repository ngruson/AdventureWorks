﻿using AutoMapper;
using AW.Services.Customer.Core.Handlers.AddCustomer;
using AW.Services.Customer.Core.Handlers.DeleteCustomer;
using AW.Services.Customer.Core.Handlers.GetCustomer;
using AW.Services.Customer.Core.Handlers.GetCustomers;
using AW.Services.Customer.Core.Handlers.UpdateCustomer;
using AW.Services.Customer.REST.API.Controllers;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Customer.REST.API.UnitTests
{
    public class CustomerControllerUnitTests
    {
        public class GetCustomers
        {
            [Fact]
            public async Task GetCustomers_ShouldReturnCustomers_WhenGivenCustomers()
            {
                //Arrange
                var mapper = new MapperConfiguration(opts =>
                {
                    opts.AddProfile<MappingProfile>();
                }).CreateMapper();

                var dto = new GetCustomersDto
                {
                    TotalCustomers = 2,
                    Customers = new List<Core.Handlers.GetCustomers.CustomerDto>
                    {
                        new TestBuilders.GetCustomers.StoreCustomerBuilder().WithTestValues().Build(),
                        new TestBuilders.GetCustomers.IndividualCustomerBuilder().WithTestValues().Build()
                    }
                };

                var mockLogger = new Mock<ILogger<CustomerController>>();
                var mockMediator = new Mock<IMediator>();
                mockMediator.Setup(x => x.Send(It.IsAny<GetCustomersQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(dto);

                var controller = new CustomerController(
                    mockLogger.Object,
                    mockMediator.Object,
                    mapper
                );

                //Act
                var request = new GetCustomersQuery();
                var actionResult = await controller.GetCustomers(request);

                //Assert
                var okObjectResult = actionResult as OkObjectResult;
                okObjectResult.Should().NotBeNull();

                var response = okObjectResult.Value as Models.GetCustomers.GetCustomersResult;
                response.TotalCustomers.Should().Be(2);
                response.Customers.Count.Should().Be(2);
                response.Customers[0].AccountNumber.Should().Be("AW00000001");
                response.Customers[1].AccountNumber.Should().Be("AW00011000");

                var address = response.Customers[0].Addresses[0];
                address.AddressType.Should().Be("Main Office");
                address.Address.AddressLine1.Should().Be("2251 Elliot Avenue");
                address.Address.AddressLine2.Should().BeNullOrEmpty();
                address.Address.PostalCode.Should().Be("98104");
                address.Address.City.Should().Be("Seattle");
                address.Address.StateProvinceCode.Should().Be("WA");
                address.Address.CountryRegionCode.Should().Be("US");

                address = response.Customers[1].Addresses[0];
                address.AddressType.Should().Be("Home");
                address.Address.AddressLine1.Should().Be("3761 N. 14th St");
                address.Address.AddressLine2.Should().BeNullOrEmpty();
                address.Address.PostalCode.Should().Be("4700");
                address.Address.City.Should().Be("Rockhampton");
                address.Address.StateProvinceCode.Should().Be("QLD");
                address.Address.CountryRegionCode.Should().Be("AU");
            }

            [Fact]
            public async Task GetCustomers_ShouldReturnNotFound_WhenGivenNoCustomers()
            {
                //Arrange
                var mapper = new MapperConfiguration(opts =>
                {
                    opts.AddProfile<MappingProfile>();
                }).CreateMapper();

                var mockLogger = new Mock<ILogger<CustomerController>>();
                var mockMediator = new Mock<IMediator>();

                var controller = new CustomerController(
                    mockLogger.Object,
                    mockMediator.Object,
                    mapper
                );

                //Act
                var request = new GetCustomersQuery();
                var actionResult = await controller.GetCustomers(request);

                //Assert
                var notFoundResult = actionResult as NotFoundResult;
                notFoundResult.Should().NotBeNull();
            }
        }

        public class GetCustomer
        {
            [Fact]
            public async Task GetCustomer_ShouldReturnCustomer_GivenCustomer()
            {
                //Arrange
                var mapper = new MapperConfiguration(opts =>
                {
                    opts.AddProfile<MappingProfile>();
                }).CreateMapper();

                var dto = new TestBuilders.GetCustomer.IndividualCustomerBuilder()
                    .WithTestValues().
                    Build();

                var mockLogger = new Mock<ILogger<CustomerController>>();
                var mockMediator = new Mock<IMediator>();
                mockMediator.Setup(x => x.Send(It.IsAny<GetCustomerQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(dto);

                var controller = new CustomerController(
                    mockLogger.Object,
                    mockMediator.Object,
                    mapper
                );

                //Act
                var query = new GetCustomerQuery { AccountNumber = "AW00000001" };
                var actionResult = await controller.GetCustomer(query);

                //Assert
                var okObjectResult = actionResult as OkObjectResult;
                okObjectResult.Should().NotBeNull();

                var customer = okObjectResult.Value as Models.GetCustomer.Customer;
                customer.Should().NotBeNull();

                var address = customer.Addresses[0];
                address.AddressType.Should().Be("Home");
                address.Address.AddressLine1.Should().Be("3761 N. 14th St");
                address.Address.AddressLine2.Should().BeNullOrEmpty();
                address.Address.PostalCode.Should().Be("4700");
                address.Address.City.Should().Be("Rockhampton");
                address.Address.StateProvinceCode.Should().Be("QLD");
                address.Address.CountryRegionCode.Should().Be("AU");
            }

            [Fact]
            public async Task GetCustomer_ShouldReturnNotFound_WhenGivenNoCustomer()
            {
                //Arrange
                var mapper = new MapperConfiguration(opts =>
                {
                    opts.AddProfile<MappingProfile>();
                }).CreateMapper();

                var mockLogger = new Mock<ILogger<CustomerController>>();
                var mockMediator = new Mock<IMediator>();

                var controller = new CustomerController(
                    mockLogger.Object,
                    mockMediator.Object,
                    mapper
                );

                //Act
                var query = new GetCustomerQuery();
                var actionResult = await controller.GetCustomer(query);

                //Assert
                var notFoundResult = actionResult as NotFoundResult;
                notFoundResult.Should().NotBeNull();
            }
        }

        public class AddCustomer
        {
            [Fact]
            public async Task AddCustomer_ShouldReturnCustomer_GivenCustomer()
            {
                //Arrange
                var mapper = new MapperConfiguration(opts =>
                {
                    opts.AddProfile<MappingProfile>();
                }).CreateMapper();

                var dto = new Core.Handlers.AddCustomer.IndividualCustomerDto { AccountNumber = "AW00000001" };

                var mockLogger = new Mock<ILogger<CustomerController>>();
                var mockMediator = new Mock<IMediator>();
                mockMediator.Setup(x => x.Send(It.IsAny<AddCustomerCommand>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(dto);

                var controller = new CustomerController(
                    mockLogger.Object,
                    mockMediator.Object,
                    mapper
                );

                //Act
                var command = new AddCustomerCommand();
                var actionResult = await controller.AddCustomer(command);

                //Assert
                var createdResult = actionResult as CreatedResult;
                createdResult.Should().NotBeNull();

                var customer = createdResult.Value as Core.Handlers.AddCustomer.CustomerDto;
                customer.Should().NotBeNull();
            }
        }

        public class UpdateCustomer
        {
            [Fact]
            public async Task UpdateCustomer_ShouldReturnCustomer_GivenCustomer()
            {
                //Arrange
                var mapper = new MapperConfiguration(opts =>
                {
                    opts.AddProfile<MappingProfile>();
                }).CreateMapper();

                var dto = new Core.Handlers.UpdateCustomer.IndividualCustomerDto { AccountNumber = "AW00000001" };

                var mockLogger = new Mock<ILogger<CustomerController>>();
                var mockMediator = new Mock<IMediator>();
                mockMediator.Setup(x => x.Send(It.IsAny<UpdateCustomerCommand>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(dto);

                var controller = new CustomerController(
                    mockLogger.Object,
                    mockMediator.Object,
                    mapper
                );

                //Act
                var customer = new Models.UpdateCustomer.IndividualCustomer();
                var actionResult = await controller.UpdateCustomer("AW00000001", customer);

                //Assert
                var okResult = actionResult as OkObjectResult;
                okResult.Should().NotBeNull();

                var updatedCustomer = okResult.Value as Models.UpdateCustomer.Customer;
                updatedCustomer.Should().NotBeNull();
            }
        }

        public class DeleteCustomer
        {
            [Fact]
            public async Task DeleteCustomer_ShouldDeleteCustomer_GivenCustomer()
            {
                //Arrange
                var mapper = new MapperConfiguration(opts =>
                {
                    opts.AddProfile<MappingProfile>();
                }).CreateMapper();

                var mockLogger = new Mock<ILogger<CustomerController>>();
                var mockMediator = new Mock<IMediator>();
                mockMediator.Setup(x => x.Send(It.IsAny<DeleteCustomerCommand>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(Unit.Value);

                var controller = new CustomerController(
                    mockLogger.Object,
                    mockMediator.Object,
                    mapper
                );

                //Act
                var command = new DeleteCustomerCommand();
                var actionResult = await controller.DeleteCustomer(command);

                //Assert
                var noContentResult = actionResult as NoContentResult;
                noContentResult.Should().NotBeNull();
            }
        }
    }
}