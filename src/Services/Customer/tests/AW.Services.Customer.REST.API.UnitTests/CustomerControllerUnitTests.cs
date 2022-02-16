using AutoFixture.Xunit2;
using AW.Services.Customer.Core.Handlers.AddCustomer;
using AW.Services.Customer.Core.Handlers.DeleteCustomer;
using AW.Services.Customer.Core.Handlers.GetCustomer;
using AW.Services.Customer.Core.Handlers.GetCustomers;
using AW.Services.Customer.Core.Handlers.GetPreferredAddress;
using AW.Services.Customer.Core.Handlers.UpdateCustomer;
using AW.Services.Customer.REST.API.Controllers;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Customer.REST.API.UnitTests
{
    public class CustomerControllerUnitTests
    {
        public class GetCustomers
        {
            [Theory, AutoMapperData(typeof(MappingProfile), typeof(Core.AutoMapper.MappingProfile))]
            public async Task GetCustomers_ShouldReturnCustomers_WhenGivenCustomers(
                
                [Frozen] Mock<IMediator> mockMediator,
                List<Core.Handlers.GetCustomer.StoreCustomerDto> customers,
                [Greedy] CustomerController sut,
                GetCustomersQuery query
            )
            {
                //Arrange
                var dto = new GetCustomersDto
                {
                    TotalCustomers = customers.Count,
                    Customers = customers.ToList<Core.Handlers.GetCustomer.CustomerDto>()
                };

                mockMediator.Setup(x => x.Send(It.IsAny<GetCustomersQuery>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(dto);

                //Act
                var actionResult = await sut.GetCustomers(query);

                //Assert
                var okObjectResult = actionResult as OkObjectResult;
                okObjectResult.Should().NotBeNull();

                var response = okObjectResult.Value as Models.GetCustomers.GetCustomersResult;
                response.TotalCustomers.Should().Be(customers.Count);
                response.Customers.Count.Should().Be(customers.Count);
                response.Customers[0].AccountNumber.Should().Be(customers[0].AccountNumber);
                response.Customers[1].AccountNumber.Should().Be(customers[1].AccountNumber);
            }

            [Theory]
            [AutoMoqData]
            public async Task GetCustomers_ShouldReturnNotFound_WhenGivenNoCustomers(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] CustomerController sut,
                GetCustomersQuery query
            )
            {
                //Arrange                
                mockMediator.Setup(x => x.Send(
                    It.IsAny<GetCustomersQuery>(), 
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync((GetCustomersDto)null);

                //Act
                var actionResult = await sut.GetCustomers(query);

                //Assert
                var notFoundResult = actionResult as NotFoundResult;
                notFoundResult.Should().NotBeNull();
            }
        }

        public class GetCustomer
        {
            [Theory]
            [AutoMoqData]
            public async Task GetCustomer_ShouldReturnCustomer_GivenCustomer(
                [Frozen] Mock<IMediator> mockMediator,
                Core.Handlers.GetCustomer.StoreCustomerDto customer,
                [Greedy] CustomerController sut,
                GetCustomerQuery query
            )
            {
                //Arrange
                mockMediator.Setup(x => x.Send(
                    It.IsAny<GetCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(customer);

                //Act
                var actionResult = await sut.GetCustomer(query);

                //Assert
                var okObjectResult = actionResult as OkObjectResult;
                okObjectResult.Should().NotBeNull();

                var result = okObjectResult.Value as Models.GetCustomer.Customer;
                result.Should().NotBeNull();
            }

            [Theory]
            [AutoMoqData]
            public async Task GetCustomer_ShouldReturnNotFound_WhenGivenNoCustomer(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] CustomerController sut,
                GetCustomerQuery query
            )
            {
                //Arrange
                mockMediator.Setup(x => x.Send(
                    It.IsAny<GetCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync((Core.Handlers.GetCustomer.CustomerDto)null);

                //Act
                var actionResult = await sut.GetCustomer(query);

                //Assert
                var notFoundResult = actionResult as NotFoundResult;
                notFoundResult.Should().NotBeNull();
            }
        }

        public class GetPreferredAddress
        {
            [Theory]
            [AutoMoqData]
            public async Task GetPreferredAddress_ShouldReturnAddress_GivenAddress(
                [Frozen] Mock<IMediator> mockMediator,
                Core.Handlers.GetPreferredAddress.AddressDto address,
                [Greedy] CustomerController sut,
                GetPreferredAddressQuery query
            )
            {
                //Arrange
                mockMediator.Setup(x => x.Send(
                    It.IsAny<GetPreferredAddressQuery>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(address);

                //Act
                var actionResult = await sut.GetPreferredAddress(query);

                //Assert
                var okObjectResult = actionResult as OkObjectResult;
                okObjectResult.Should().NotBeNull();

                var result = okObjectResult.Value as Core.Handlers.GetPreferredAddress.AddressDto;
                result.Should().Be(address);
            }

            [Theory]
            [AutoMoqData]
            public async Task GetPreferredAddress_ShouldReturnNotFound_WhenGivenNoCustomer(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] CustomerController sut,
                GetPreferredAddressQuery query
            )
            {
                //Arrange
                mockMediator.Setup(x => x.Send(
                    It.IsAny<GetPreferredAddressQuery>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync((Core.Handlers.GetPreferredAddress.AddressDto)null);

                //Act
                var actionResult = await sut.GetPreferredAddress(query);

                //Assert
                var notFoundResult = actionResult as NotFoundResult;
                notFoundResult.Should().NotBeNull();
            }
        }

        public class AddCustomer
        {
            [Theory]
            [AutoMoqData]
            public async Task AddCustomer_ShouldReturnCustomer_GivenCustomer(
                [Greedy] CustomerController sut,
                AddCustomerCommand command
            )
            {
                //Act
                var actionResult = await sut.AddCustomer(command);

                //Assert
                var createdResult = actionResult as CreatedResult;
                createdResult.Should().NotBeNull();

                var customer = createdResult.Value as Core.Handlers.AddCustomer.CustomerDto;
                customer.Should().NotBeNull();
            }
        }

        public class UpdateCustomer
        {
            [Theory, AutoMapperData(typeof(MappingProfile), typeof(Core.AutoMapper.MappingProfile))]
            public async Task UpdateCustomer_ShouldReturnCustomer_GivenCustomer(
                Core.Handlers.UpdateCustomer.StoreCustomerDto dto,
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] CustomerController sut,
                Core.Models.UpdateCustomer.StoreCustomer customer
            )
            {
                //Arrange
                mockMediator.Setup(x => x.Send(
                    It.IsAny<UpdateCustomerCommand>(), 
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(dto);

                //Act
                var actionResult = await sut.UpdateCustomer("1", customer);

                //Assert
                var okResult = actionResult as OkObjectResult;
                okResult.Should().NotBeNull();

                var updatedCustomer = okResult.Value as Core.Models.UpdateCustomer.Customer;
                updatedCustomer.Should().NotBeNull();
            }
        }

        public class DeleteCustomer
        {
            [Theory]
            [AutoMoqData]
            public async Task DeleteCustomer_ShouldDeleteCustomer_GivenCustomer(
                [Greedy] CustomerController sut,
                DeleteCustomerCommand command
            )
            {
                //Act
                var actionResult = await sut.DeleteCustomer(command);

                //Assert
                var noContentResult = actionResult as NoContentResult;
                noContentResult.Should().NotBeNull();
            }
        }
    }
}