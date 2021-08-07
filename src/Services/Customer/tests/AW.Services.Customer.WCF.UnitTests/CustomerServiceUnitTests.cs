using AutoFixture.Xunit2;
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
using AW.SharedKernel.UnitTesting;
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
        [Theory, AutoMapperData(typeof(MappingProfile), typeof(Core.MappingProfile))]
        public async Task ListCustomers_Store_ReturnsCustomers(
            [Frozen] Mock<IMediator> mockMediator,
            List<Core.Handlers.GetCustomer.StoreCustomerDto> customers,
            CustomerService sut,
            ListCustomersRequest request
        )
        {
            //Arrange
            var dto = new GetCustomersDto
            {
                Customers = customers.ToList<Core.Handlers.GetCustomer.CustomerDto>(),
                TotalCustomers = customers.Count
            };

            mockMediator.Setup(x => x.Send(
                It.IsAny<GetCustomersQuery>(), 
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(dto);

            //Act
            var result = await sut.ListCustomers(request);

            //Assert
            result.Customers.Customer.Should().BeEquivalentTo(dto.Customers, options => options
                .Excluding(c => c.CustomerType)
            );
        }

        [Theory, AutoMapperData(typeof(MappingProfile), typeof(Core.MappingProfile))]
        public async Task ListCustomers_Individual_ReturnsCustomers(
            [Frozen] Mock<IMediator> mockMediator,
            List<Core.Handlers.GetCustomer.IndividualCustomerDto> customers,
            CustomerService sut,
            ListCustomersRequest request
        )
        {
            //Arrange
            var dto = new GetCustomersDto
            {
                Customers = customers.ToList<Core.Handlers.GetCustomer.CustomerDto>(),
                TotalCustomers = customers.Count
            };

            mockMediator.Setup(x => x.Send(
                It.IsAny<GetCustomersQuery>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(dto);

            //Act
            var result = await sut.ListCustomers(request);

            //Assert
            result.Should().NotBeNull();
            result.Customers.Customer.Should().BeEquivalentTo(dto.Customers, options => options
                .Excluding(c => c.CustomerType)
            );
        }

        [Theory, AutoMapperData(typeof(MappingProfile), typeof(Core.MappingProfile))]
        public async Task GetCustomer_Store_ReturnsCustomer(
            [Frozen] Mock<IMediator> mockMediator,
            [Frozen] Core.Handlers.GetCustomer.StoreCustomerDto dto,
            CustomerService sut,
            GetCustomerRequest request
        )
        {
            //Arrange
            mockMediator.Setup(x => x.Send(It.IsAny<GetCustomerQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(dto);

            //Act
            var result = await sut.GetCustomer(request);

            //Assert
            result.Should().NotBeNull();
            result.Customer.Should().BeEquivalentTo(dto, options => options
                .Excluding(c => c.CustomerType)
                .Excluding(c => c.SalesOrders)
            );
        }

        [Theory, AutoMapperData(typeof(MappingProfile), typeof(Core.MappingProfile))]
        public async Task GetCustomer_Individual_ReturnsCustomer(
            [Frozen] Mock<IMediator> mockMediator,
            [Frozen] Core.Handlers.GetCustomer.IndividualCustomerDto dto,
            CustomerService sut,
            GetCustomerRequest request
        )
        {
            //Arrange
            mockMediator.Setup(x => x.Send(It.IsAny<GetCustomerQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(dto);

            //Act
            var result = await sut.GetCustomer(request);

            //Assert
            result.Should().NotBeNull();
            result.Customer.Should().BeEquivalentTo(dto, options => options
                .Excluding(c => c.CustomerType)
                .Excluding(c => c.SalesOrders)
            );
        }

        [Theory, AutoMapperData(typeof(MappingProfile), typeof(Core.MappingProfile))]
        public async Task UpdateCustomer_ReturnsCustomer(
            [Frozen] Mock<IMediator> mockMediator,
            [Frozen] Core.Handlers.UpdateCustomer.StoreCustomerDto dto,
            CustomerService sut,
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
            var result = await sut.UpdateCustomer(new UpdateCustomerRequest
                {
                    Customer = customer
                });

            //Assert
            result.Should().NotBeNull();
            result.Customer.Should().NotBeNull();
            result.Customer.AccountNumber.Should().Be(dto.AccountNumber);
        }

        [Theory, AutoMapperData(typeof(MappingProfile), typeof(Core.MappingProfile))]
        public async Task AddCustomerAddress_ReturnsResponse(
            CustomerService sut,
            AddCustomerAddressRequest request
        )
        {
            //Act
            var result = await sut.AddCustomerAddress(request);

            //Assert
            result.Should().NotBeNull();
        }

        [Theory, AutoMapperData(typeof(MappingProfile), typeof(Core.MappingProfile))]
        public async Task UpdateCustomerAddress_ReturnsResponse(
            CustomerService sut,
            UpdateCustomerAddressRequest request
        )
        {
            //Act
            var result = await sut.UpdateCustomerAddress(request);

            //Assert
            result.Should().NotBeNull();
        }

        [Theory, AutoMapperData(typeof(MappingProfile), typeof(Core.MappingProfile))]
        public async Task DeleteCustomerAddress_ReturnsResponse(
            CustomerService sut,
            DeleteCustomerAddressRequest request
        )
        {
            //Act
            var result = await sut.DeleteCustomerAddress(request);

            //Assert
            result.Should().NotBeNull();
        }

        [Theory, AutoMapperData(typeof(MappingProfile), typeof(Core.MappingProfile))]
        public async Task AddCustomerContact_ReturnsResponse(
            CustomerService sut,
            AddStoreCustomerContactRequest request
        )
        {
            //Act
            var result = await sut.AddStoreCustomerContact(request);

            //Assert
            result.Should().NotBeNull();
        }

        [Theory, AutoMapperData(typeof(MappingProfile), typeof(Core.MappingProfile))]
        public async Task UpdateCustomerContact_ReturnsResponse(
            CustomerService sut,
            UpdateStoreCustomerContactRequest request
        )
        {
            //Act
            var result = await sut.UpdateStoreCustomerContact(request);

            //Assert
            result.Should().NotBeNull();
        }

        [Theory, AutoMapperData(typeof(MappingProfile), typeof(Core.MappingProfile))]
        public async Task DeleteCustomerContact_ReturnsResponse(
            CustomerService sut,
            DeleteStoreCustomerContactRequest request
        )
        {
            //Act
            var result = await sut.DeleteStoreCustomerContact(request);

            //Assert
            result.Should().NotBeNull();
        }

        [Theory, AutoMapperData(typeof(MappingProfile), typeof(Core.MappingProfile))]
        public async Task AddIndividualCustomerEmailAddress_ReturnsResponse(
            CustomerService sut,
            AddIndividualCustomerEmailAddressRequest request
        )
        {
            //Act
            var result = await sut.AddIndividualCustomerEmailAddress(request);

            //Assert
            result.Should().NotBeNull();
        }

        [Theory, AutoMapperData(typeof(MappingProfile), typeof(Core.MappingProfile))]
        public async Task DeleteIndividualCustomerEmailAddress_ReturnsResponse(
            CustomerService sut,
            DeleteIndividualCustomerEmailAddressRequest request
        )
        {
            //Act
            var result = await sut.DeleteIndividualCustomerEmailAddress(request);

            //Assert
            result.Should().NotBeNull();
        }
    }
}