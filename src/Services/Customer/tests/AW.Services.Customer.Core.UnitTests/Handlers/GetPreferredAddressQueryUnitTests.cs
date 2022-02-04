using AutoFixture.Xunit2;
using AW.Services.Customer.Core.AutoMapper;
using AW.Services.Customer.Core.Exceptions;
using AW.Services.Customer.Core.Handlers.GetPreferredAddress;
using AW.Services.Customer.Core.Specifications;
using AW.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests.Handlers
{
    public class GetPreferredAddressQueryUnitTests
    {
        public class GetPreferredBillingAddressForIndividualCustomer
        {
            [Theory]
            [AutoMapperData(typeof(MappingProfile))]
            public async Task GetPreferredBillingAddress_BillingAddressExists_ReturnAddress(
                [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
                GetPreferredAddressQueryHandler sut,
                GetPreferredAddressQuery query,
                Entities.IndividualCustomer customer
            )
            {
                //Arrange
                query.AddressType = "Billing";
                customer.Addresses[0].AddressType = "Billing";

                customerRepoMock.Setup(_ => _.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(customer);

                //Act
                var result = await sut.Handle(query, CancellationToken.None);

                //Assert
                result.Should().BeEquivalentTo(
                    customer.Addresses[0].Address,
                    opt => opt.Excluding(_ => _.SelectedMemberPath.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase))
                );

                customerRepoMock.Verify(x => x.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ));
            }

            [Theory]
            [AutoMapperData(typeof(MappingProfile))]
            public async Task GetPreferredBillingAddress_HomeAddressExists_ReturnAddress(
                [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
                GetPreferredAddressQueryHandler sut,
                GetPreferredAddressQuery query,
                Entities.IndividualCustomer customer
            )
            {
                //Arrange
                query.AddressType = "Billing";
                customer.Addresses[0].AddressType = "Home";

                customerRepoMock.Setup(_ => _.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(customer);

                //Act
                var result = await sut.Handle(query, CancellationToken.None);

                //Assert
                result.Should().BeEquivalentTo(
                    customer.Addresses[0].Address,
                    opt => opt.Excluding(_ => _.SelectedMemberPath.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase))
                );

                customerRepoMock.Verify(x => x.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ));
            }

            [Theory]
            [AutoMapperData(typeof(MappingProfile))]
            public async Task GetPreferredBillingAddress_NoAddressFound_ReturnAddress(
                [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
                GetPreferredAddressQueryHandler sut,
                GetPreferredAddressQuery query,
                Entities.IndividualCustomer customer
            )
            {
                //Arrange
                query.AddressType = "Billing";

                customerRepoMock.Setup(_ => _.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(customer);

                //Act
                var result = await sut.Handle(query, CancellationToken.None);

                //Assert
                result.Should().BeNull();

                customerRepoMock.Verify(x => x.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ));
            }

            [Theory]
            [AutoMapperData(typeof(MappingProfile))]
            public async Task GetPreferredBillingAddress_CustomerDoesNotExist_ThrowCustomerNotFoundException(
                [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
                GetPreferredAddressQueryHandler sut,
                GetPreferredAddressQuery query
            )
            {
                //Arrange
                query.AddressType = "Billing";

                customerRepoMock.Setup(_ => _.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync((Entities.IndividualCustomer)null);

                //Act
                Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

                //Assert
                await func.Should().ThrowAsync<CustomerNotFoundException>();

                customerRepoMock.Verify(x => x.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ));
            }
        }

        public class GetPreferredShippingAddressForIndividualCustomer
        {
            [Theory]
            [AutoMapperData(typeof(MappingProfile))]
            public async Task GetPreferredShippingAddress_ShippingAddressExists_ReturnAddress(
                [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
                GetPreferredAddressQueryHandler sut,
                GetPreferredAddressQuery query,
                Entities.IndividualCustomer customer
            )
            {
                //Arrange
                query.AddressType = "Shipping";
                customer.Addresses[0].AddressType = "Shipping";

                customerRepoMock.Setup(_ => _.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(customer);

                //Act
                var result = await sut.Handle(query, CancellationToken.None);

                //Assert
                result.Should().BeEquivalentTo(
                    customer.Addresses[0].Address,
                    opt => opt.Excluding(_ => _.SelectedMemberPath.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase))
                );

                customerRepoMock.Verify(x => x.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ));
            }

            [Theory]
            [AutoMapperData(typeof(MappingProfile))]
            public async Task GetPreferredShippingAddress_HomeAddressExists_ReturnAddress(
                [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
                GetPreferredAddressQueryHandler sut,
                GetPreferredAddressQuery query,
                Entities.IndividualCustomer customer
            )
            {
                //Arrange
                query.AddressType = "Shipping";
                customer.Addresses[0].AddressType = "Home";

                customerRepoMock.Setup(_ => _.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(customer);

                //Act
                var result = await sut.Handle(query, CancellationToken.None);

                //Assert
                result.Should().BeEquivalentTo(
                    customer.Addresses[0].Address,
                    opt => opt.Excluding(_ => _.SelectedMemberPath.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase))
                );

                customerRepoMock.Verify(x => x.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ));
            }

            [Theory]
            [AutoMapperData(typeof(MappingProfile))]
            public async Task GetPreferredShippingAddress_NoAddressFound_ReturnAddress(
                [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
                GetPreferredAddressQueryHandler sut,
                GetPreferredAddressQuery query,
                Entities.IndividualCustomer customer
            )
            {
                //Arrange
                query.AddressType = "Shipping";

                customerRepoMock.Setup(_ => _.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(customer);

                //Act
                var result = await sut.Handle(query, CancellationToken.None);

                //Assert
                result.Should().BeNull();

                customerRepoMock.Verify(x => x.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ));
            }

            [Theory]
            [AutoMapperData(typeof(MappingProfile))]
            public async Task GetPreferredShippingAddress_CustomerDoesNotExist_ThrowCustomerNotFoundException(
                [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
                GetPreferredAddressQueryHandler sut,
                GetPreferredAddressQuery query
            )
            {
                //Arrange
                query.AddressType = "Shipping";

                customerRepoMock.Setup(_ => _.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync((Entities.IndividualCustomer)null);

                //Act
                Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

                //Assert
                await func.Should().ThrowAsync<CustomerNotFoundException>();

                customerRepoMock.Verify(x => x.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ));
            }
        }

        public class GetPreferredBillingAddressForStoreCustomer
        {
            [Theory]
            [AutoMapperData(typeof(MappingProfile))]
            public async Task GetPreferredBillingAddress_BillingAddressExists_ReturnAddress(
                [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
                GetPreferredAddressQueryHandler sut,
                GetPreferredAddressQuery query,
                Entities.StoreCustomer customer
            )
            {
                //Arrange
                query.AddressType = "Billing";
                customer.Addresses[0].AddressType = "Billing";

                customerRepoMock.Setup(_ => _.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(customer);

                //Act
                var result = await sut.Handle(query, CancellationToken.None);

                //Assert
                result.Should().BeEquivalentTo(
                    customer.Addresses[0].Address,
                    opt => opt.Excluding(_ => _.SelectedMemberPath.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase))
                );

                customerRepoMock.Verify(x => x.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ));
            }

            [Theory]
            [AutoMapperData(typeof(MappingProfile))]
            public async Task GetPreferredBillingAddress_MainOfficeAddressExists_ReturnAddress(
                [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
                GetPreferredAddressQueryHandler sut,
                GetPreferredAddressQuery query,
                Entities.StoreCustomer customer
            )
            {
                //Arrange
                query.AddressType = "Billing";
                customer.Addresses[0].AddressType = "Main Office";

                customerRepoMock.Setup(_ => _.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(customer);

                //Act
                var result = await sut.Handle(query, CancellationToken.None);

                //Assert
                result.Should().BeEquivalentTo(
                    customer.Addresses[0].Address,
                    opt => opt.Excluding(_ => _.SelectedMemberPath.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase))
                );

                customerRepoMock.Verify(x => x.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ));
            }

            [Theory]
            [AutoMapperData(typeof(MappingProfile))]
            public async Task GetPreferredBillingAddress_HomeAddressExists_ReturnAddress(
                [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
                GetPreferredAddressQueryHandler sut,
                GetPreferredAddressQuery query,
                Entities.StoreCustomer customer
            )
            {
                //Arrange
                query.AddressType = "Billing";
                customer.Addresses[0].AddressType = "Home";

                customerRepoMock.Setup(_ => _.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(customer);

                //Act
                var result = await sut.Handle(query, CancellationToken.None);

                //Assert
                result.Should().BeEquivalentTo(
                    customer.Addresses[0].Address,
                    opt => opt.Excluding(_ => _.SelectedMemberPath.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase))
                );

                customerRepoMock.Verify(x => x.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ));
            }

            [Theory]
            [AutoMapperData(typeof(MappingProfile))]
            public async Task GetPreferredBillingAddress_NoAddressFound_ReturnAddress(
                [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
                GetPreferredAddressQueryHandler sut,
                GetPreferredAddressQuery query,
                Entities.StoreCustomer customer
            )
            {
                //Arrange
                query.AddressType = "Billing";

                customerRepoMock.Setup(_ => _.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(customer);

                //Act
                var result = await sut.Handle(query, CancellationToken.None);

                //Assert
                result.Should().BeNull();

                customerRepoMock.Verify(x => x.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ));
            }

            [Theory]
            [AutoMapperData(typeof(MappingProfile))]
            public async Task GetPreferredBillingAddress_CustomerDoesNotExist_ThrowCustomerNotFoundException(
                [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
                GetPreferredAddressQueryHandler sut,
                GetPreferredAddressQuery query
            )
            {
                //Arrange
                query.AddressType = "Billing";

                customerRepoMock.Setup(_ => _.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync((Entities.StoreCustomer)null);

                //Act
                Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

                //Assert
                await func.Should().ThrowAsync<CustomerNotFoundException>();

                customerRepoMock.Verify(x => x.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ));
            }
        }

        public class GetPreferredShippingAddressForStoreCustomer
        {
            [Theory]
            [AutoMapperData(typeof(MappingProfile))]
            public async Task GetPreferredShippingAddress_ShippingAddressExists_ReturnAddress(
                [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
                GetPreferredAddressQueryHandler sut,
                GetPreferredAddressQuery query,
                Entities.StoreCustomer customer
            )
            {
                //Arrange
                query.AddressType = "Shipping";
                customer.Addresses[0].AddressType = "Shipping";

                customerRepoMock.Setup(_ => _.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(customer);

                //Act
                var result = await sut.Handle(query, CancellationToken.None);

                //Assert
                result.Should().BeEquivalentTo(
                    customer.Addresses[0].Address,
                    opt => opt.Excluding(_ => _.SelectedMemberPath.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase))
                );

                customerRepoMock.Verify(x => x.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ));
            }

            [Theory]
            [AutoMapperData(typeof(MappingProfile))]
            public async Task GetPreferredShippingAddress_MainOfficeAddressExists_ReturnAddress(
                [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
                GetPreferredAddressQueryHandler sut,
                GetPreferredAddressQuery query,
                Entities.StoreCustomer customer
            )
            {
                //Arrange
                query.AddressType = "Shipping";
                customer.Addresses[0].AddressType = "Main Office";

                customerRepoMock.Setup(_ => _.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(customer);

                //Act
                var result = await sut.Handle(query, CancellationToken.None);

                //Assert
                result.Should().BeEquivalentTo(
                    customer.Addresses[0].Address,
                    opt => opt.Excluding(_ => _.SelectedMemberPath.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase))
                );

                customerRepoMock.Verify(x => x.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ));
            }

            [Theory]
            [AutoMapperData(typeof(MappingProfile))]
            public async Task GetPreferredShippingAddress_HomeAddressExists_ReturnAddress(
                [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
                GetPreferredAddressQueryHandler sut,
                GetPreferredAddressQuery query,
                Entities.StoreCustomer customer
            )
            {
                //Arrange
                query.AddressType = "Shipping";
                customer.Addresses[0].AddressType = "Home";

                customerRepoMock.Setup(_ => _.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(customer);

                //Act
                var result = await sut.Handle(query, CancellationToken.None);

                //Assert
                result.Should().BeEquivalentTo(
                    customer.Addresses[0].Address,
                    opt => opt.Excluding(_ => _.SelectedMemberPath.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase))
                );

                customerRepoMock.Verify(x => x.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ));
            }

            [Theory]
            [AutoMapperData(typeof(MappingProfile))]
            public async Task GetPreferredShippingAddress_NoAddressFound_ReturnAddress(
                [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
                GetPreferredAddressQueryHandler sut,
                GetPreferredAddressQuery query,
                Entities.StoreCustomer customer
            )
            {
                //Arrange
                query.AddressType = "Shipping";

                customerRepoMock.Setup(_ => _.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(customer);

                //Act
                var result = await sut.Handle(query, CancellationToken.None);

                //Assert
                result.Should().BeNull();

                customerRepoMock.Verify(x => x.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ));
            }

            [Theory]
            [AutoMapperData(typeof(MappingProfile))]
            public async Task GetPreferredShippingAddress_CustomerDoesNotExist_ThrowCustomerNotFoundException(
                [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
                GetPreferredAddressQueryHandler sut,
                GetPreferredAddressQuery query
            )
            {
                //Arrange
                query.AddressType = "Shipping";

                customerRepoMock.Setup(_ => _.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync((Entities.StoreCustomer)null);

                //Act
                Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

                //Assert
                await func.Should().ThrowAsync<CustomerNotFoundException>();

                customerRepoMock.Verify(x => x.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ));
            }
        }
    }
}