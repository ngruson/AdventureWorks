using AutoFixture.Xunit2;
using AW.Services.Customer.Core.AutoMapper;
using AW.Services.Customer.Core.Exceptions;
using AW.Services.Customer.Core.Handlers.GetPreferredAddress;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
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
                Entities.IndividualCustomer customer,
                Entities.Address address
            )
            {
                //Arrange
                query.AddressType = "Billing";
                customer.AddAddress(
                    new Entities.CustomerAddress(
                        "Billing",
                        address
                    )
                );

                customerRepoMock.Setup(_ => _.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(customer);

                //Act
                var result = await sut.Handle(query, CancellationToken.None);

                //Assert
                result.Should().BeEquivalentTo(
                    address,
                    opt => opt.Excluding(_ => _.Path.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase))
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
                Entities.IndividualCustomer customer,
                Entities.Address address
            )
            {
                //Arrange
                query.AddressType = "Billing";
                customer.AddAddress(
                    new Entities.CustomerAddress(
                        "Home",
                        address
                    )
                );

                customerRepoMock.Setup(_ => _.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(customer);

                //Act
                var result = await sut.Handle(query, CancellationToken.None);

                //Assert
                result.Should().BeEquivalentTo(
                    address,
                    opt => opt.Excluding(_ => _.Path.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase))
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
                Entities.IndividualCustomer customer,
                Entities.Address address
            )
            {
                //Arrange
                query.AddressType = "Shipping";
                customer.AddAddress(
                    new Entities.CustomerAddress(
                        "Shipping",
                        address
                    )
                );

                customerRepoMock.Setup(_ => _.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(customer);

                //Act
                var result = await sut.Handle(query, CancellationToken.None);

                //Assert
                result.Should().BeEquivalentTo(
                    address,
                    opt => opt.Excluding(_ => _.Path.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase))
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
                Entities.IndividualCustomer customer,
                Entities.Address address
            )
            {
                //Arrange
                query.AddressType = "Shipping";
                customer.AddAddress(
                    new Entities.CustomerAddress(
                        "Home",
                        address
                    )
                );

                customerRepoMock.Setup(_ => _.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(customer);

                //Act
                var result = await sut.Handle(query, CancellationToken.None);

                //Assert
                result.Should().BeEquivalentTo(
                    address,
                    opt => opt.Excluding(_ => _.Path.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase))
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
                Entities.StoreCustomer customer,
                Entities.Address address
            )
            {
                //Arrange
                query.AddressType = "Billing";
                customer.AddAddress(
                    new Entities.CustomerAddress(
                        "Billing",
                        address
                    )
                );

                customerRepoMock.Setup(_ => _.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(customer);

                //Act
                var result = await sut.Handle(query, CancellationToken.None);

                //Assert
                result.Should().BeEquivalentTo(
                    address,
                    opt => opt.Excluding(_ => _.Path.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase))
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
                Entities.StoreCustomer customer,
                Entities.Address address
            )
            {
                //Arrange
                query.AddressType = "Billing";
                customer.AddAddress(
                    new Entities.CustomerAddress(
                        "Main Office",
                        address
                    )
                );

                customerRepoMock.Setup(_ => _.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(customer);

                //Act
                var result = await sut.Handle(query, CancellationToken.None);

                //Assert
                result.Should().BeEquivalentTo(
                    address,
                    opt => opt.Excluding(_ => _.Path.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase))
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
                Entities.StoreCustomer customer,
                Entities.Address address
            )
            {
                //Arrange
                query.AddressType = "Billing";
                customer.AddAddress(
                    new Entities.CustomerAddress(
                        "Home",
                        address
                    )
                );

                customerRepoMock.Setup(_ => _.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(customer);

                //Act
                var result = await sut.Handle(query, CancellationToken.None);

                //Assert
                result.Should().BeEquivalentTo(
                    address,
                    opt => opt.Excluding(_ => _.Path.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase))
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
                Entities.StoreCustomer customer,
                Entities.Address address
            )
            {
                //Arrange
                query.AddressType = "Shipping";
                customer.AddAddress(
                    new Entities.CustomerAddress(
                        "Shipping",
                        address
                    )
                );

                customerRepoMock.Setup(_ => _.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(customer);

                //Act
                var result = await sut.Handle(query, CancellationToken.None);

                //Assert
                result.Should().BeEquivalentTo(
                    address,
                    opt => opt.Excluding(_ => _.Path.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase))
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
                Entities.StoreCustomer customer,
                Entities.Address address
            )
            {
                //Arrange
                query.AddressType = "Shipping";
                customer.AddAddress(
                    new Entities.CustomerAddress(
                        "Main Office",
                        address
                    )
                );

                customerRepoMock.Setup(_ => _.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(customer);

                //Act
                var result = await sut.Handle(query, CancellationToken.None);

                //Assert
                result.Should().BeEquivalentTo(
                    address,
                    opt => opt.Excluding(_ => _.Path.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase))
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
                Entities.StoreCustomer customer,
                Entities.Address address
            )
            {
                //Arrange
                query.AddressType = "Shipping";
                customer.AddAddress(
                    new Entities.CustomerAddress(
                        "Home",
                        address
                    )
                );

                customerRepoMock.Setup(_ => _.GetBySpecAsync(
                    It.IsAny<GetCustomerAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(customer);

                //Act
                var result = await sut.Handle(query, CancellationToken.None);

                //Assert
                result.Should().BeEquivalentTo(
                    address,
                    opt => opt.Excluding(_ => _.Path.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase))
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