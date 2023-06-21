using Ardalis.Result;
using AutoFixture.Xunit2;
using AW.Services.Customer.Core.AutoMapper;
using AW.Services.Customer.Core.Exceptions;
using AW.Services.Customer.Core.Handlers.GetPreferredAddress;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests.Handlers
{
    public class GetPreferredAddressQueryUnitTests
    {
        public class GetPreferredBillingAddressForIndividualCustomer
        {
            [Theory]
            [AutoMapperData(typeof(MappingProfile))]
            public async Task return_success_given_billing_address_exists(
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

                customerRepoMock.Setup(_ => _.SingleOrDefaultAsync(
                    It.IsAny<GetCustomerWithAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(customer);

                //Act
                var result = await sut.Handle(query, CancellationToken.None);

                //Assert
                result.IsSuccess.Should().BeTrue();

                result.Value.Should().BeEquivalentTo(
                    address,
                    opt => opt.Excluding(_ => _.Path.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase))
                );

                customerRepoMock.Verify(x => x.SingleOrDefaultAsync(
                    It.IsAny<GetCustomerWithAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ));
            }

            [Theory]
            [AutoMapperData(typeof(MappingProfile))]
            public async Task return_success_given_home_address_exists(
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

                customerRepoMock.Setup(_ => _.SingleOrDefaultAsync(
                    It.IsAny<GetCustomerWithAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(customer);

                //Act
                var result = await sut.Handle(query, CancellationToken.None);

                //Assert
                result.IsSuccess.Should().BeTrue();

                result.Value.Should().BeEquivalentTo(
                    address,
                    opt => opt.Excluding(_ => _.Path.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase))
                );

                customerRepoMock.Verify(x => x.SingleOrDefaultAsync(
                    It.IsAny<GetCustomerWithAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ));
            }

            [Theory, AutoMoqData]
            public async Task return_invalid_given_command_is_invalid(
                [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
                [Frozen] Mock<IValidator<GetPreferredAddressQuery>> validator,
                GetPreferredAddressQueryHandler sut,
                GetPreferredAddressQuery query,
                List<ValidationFailure> failures
            )
            {
                // Arrange
                validator.Setup(_ => _.ValidateAsync(
                        query,
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(new ValidationResult(failures));

                //Act
                var result = await sut.Handle(query, CancellationToken.None);

                //Assert
                result.Status.Should().Be(ResultStatus.Invalid);

                customerRepoMock.Verify(x => x.DeleteAsync(
                        It.IsAny<Entities.Customer>(),
                        It.IsAny<CancellationToken>()
                    ),
                    Times.Never
                );
            }

            [Theory]
            [AutoMapperData(typeof(MappingProfile))]
            public async Task return_notfound_given_no_address_found(
                [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
                GetPreferredAddressQueryHandler sut,
                GetPreferredAddressQuery query,
                Entities.IndividualCustomer customer
            )
            {
                //Arrange
                query.AddressType = "Billing";

                customerRepoMock.Setup(_ => _.SingleOrDefaultAsync(
                    It.IsAny<GetCustomerWithAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(customer);

                //Act
                var result = await sut.Handle(query, CancellationToken.None);

                //Assert
                result.Status.Should().Be(ResultStatus.NotFound);

                customerRepoMock.Verify(x => x.SingleOrDefaultAsync(
                    It.IsAny<GetCustomerWithAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ));
            }

            [Theory]
            [AutoMapperData(typeof(MappingProfile))]
            public async Task return_notfound_given_customer_does_not_exist(
                [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
                GetPreferredAddressQueryHandler sut,
                GetPreferredAddressQuery query
            )
            {
                //Arrange
                query.AddressType = "Billing";

                customerRepoMock.Setup(_ => _.SingleOrDefaultAsync(
                    It.IsAny<GetCustomerWithAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync((Entities.IndividualCustomer?)null);

                //Act
                var result = await sut.Handle(query, CancellationToken.None);

                //Assert
                result.Status.Should().Be(ResultStatus.NotFound);

                customerRepoMock.Verify(x => x.SingleOrDefaultAsync(
                    It.IsAny<GetCustomerWithAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ));
            }
        }

        public class GetPreferredShippingAddressForIndividualCustomer
        {
            [Theory]
            [AutoMapperData(typeof(MappingProfile))]
            public async Task return_success_given_shipping_address_exists(
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

                customerRepoMock.Setup(_ => _.SingleOrDefaultAsync(
                    It.IsAny<GetCustomerWithAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(customer);

                //Act
                var result = await sut.Handle(query, CancellationToken.None);

                //Assert
                result.IsSuccess.Should().BeTrue();

                result.Value.Should().BeEquivalentTo(
                    address,
                    opt => opt.Excluding(_ => _.Path.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase))
                );

                customerRepoMock.Verify(x => x.SingleOrDefaultAsync(
                    It.IsAny<GetCustomerWithAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ));
            }

            [Theory]
            [AutoMapperData(typeof(MappingProfile))]
            public async Task return_success_given_home_address_exists(
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

                customerRepoMock.Setup(_ => _.SingleOrDefaultAsync(
                    It.IsAny<GetCustomerWithAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(customer);

                //Act
                var result = await sut.Handle(query, CancellationToken.None);

                //Assert
                result.IsSuccess.Should().BeTrue();

                result.Value.Should().BeEquivalentTo(
                    address,
                    opt => opt.Excluding(_ => _.Path.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase))
                );

                customerRepoMock.Verify(x => x.SingleOrDefaultAsync(
                    It.IsAny<GetCustomerWithAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ));
            }

            [Theory]
            [AutoMapperData(typeof(MappingProfile))]
            public async Task return_notfound_given_no_address_found(
                [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
                GetPreferredAddressQueryHandler sut,
                GetPreferredAddressQuery query,
                Entities.IndividualCustomer customer
            )
            {
                //Arrange
                query.AddressType = "Shipping";

                customerRepoMock.Setup(_ => _.SingleOrDefaultAsync(
                    It.IsAny<GetCustomerWithAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(customer);

                //Act
                var result = await sut.Handle(query, CancellationToken.None);

                //Assert
                result.Status.Should().Be(ResultStatus.NotFound);

                customerRepoMock.Verify(x => x.SingleOrDefaultAsync(
                    It.IsAny<GetCustomerWithAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ));
            }

            [Theory]
            [AutoMapperData(typeof(MappingProfile))]
            public async Task return_notfound_given_customer_does_not_exist(
                [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
                GetPreferredAddressQueryHandler sut,
                GetPreferredAddressQuery query
            )
            {
                //Arrange
                query.AddressType = "Shipping";

                customerRepoMock.Setup(_ => _.SingleOrDefaultAsync(
                    It.IsAny<GetCustomerWithAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync((Entities.IndividualCustomer?)null);

                //Act
                var result = await sut.Handle(query, CancellationToken.None);

                //Assert
                result.Status.Should().Be(ResultStatus.NotFound);

                customerRepoMock.Verify(x => x.SingleOrDefaultAsync(
                    It.IsAny<GetCustomerWithAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ));
            }
        }

        public class GetPreferredBillingAddressForStoreCustomer
        {
            [Theory]
            [AutoMapperData(typeof(MappingProfile))]
            public async Task return_success_given_billing_address_exists(
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

                customerRepoMock.Setup(_ => _.SingleOrDefaultAsync(
                    It.IsAny<GetCustomerWithAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(customer);

                //Act
                var result = await sut.Handle(query, CancellationToken.None);

                //Assert
                result.IsSuccess.Should().BeTrue();

                result.Value.Should().BeEquivalentTo(
                    address,
                    opt => opt.Excluding(_ => _.Path.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase))
                );

                customerRepoMock.Verify(x => x.SingleOrDefaultAsync(
                    It.IsAny<GetCustomerWithAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ));
            }

            [Theory]
            [AutoMapperData(typeof(MappingProfile))]
            public async Task return_success_given_main_office_address_exists(
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

                customerRepoMock.Setup(_ => _.SingleOrDefaultAsync(
                    It.IsAny<GetCustomerWithAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(customer);

                //Act
                var result = await sut.Handle(query, CancellationToken.None);

                //Assert
                result.IsSuccess.Should().BeTrue();

                result.Value.Should().BeEquivalentTo(
                    address,
                    opt => opt.Excluding(_ => _.Path.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase))
                );

                customerRepoMock.Verify(x => x.SingleOrDefaultAsync(
                    It.IsAny<GetCustomerWithAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ));
            }

            [Theory]
            [AutoMapperData(typeof(MappingProfile))]
            public async Task return_success_given_home_address_exists(
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

                customerRepoMock.Setup(_ => _.SingleOrDefaultAsync(
                    It.IsAny<GetCustomerWithAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(customer);

                //Act
                var result = await sut.Handle(query, CancellationToken.None);

                //Assert
                result.IsSuccess.Should().BeTrue();

                result.Value.Should().BeEquivalentTo(
                    address,
                    opt => opt.Excluding(_ => _.Path.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase))
                );

                customerRepoMock.Verify(x => x.SingleOrDefaultAsync(
                    It.IsAny<GetCustomerWithAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ));
            }

            [Theory]
            [AutoMapperData(typeof(MappingProfile))]
            public async Task return_notfound_given_no_address_found(
                [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
                GetPreferredAddressQueryHandler sut,
                GetPreferredAddressQuery query,
                Entities.StoreCustomer customer
            )
            {
                //Arrange
                query.AddressType = "Billing";

                customerRepoMock.Setup(_ => _.SingleOrDefaultAsync(
                    It.IsAny<GetCustomerWithAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(customer);

                //Act
                var result = await sut.Handle(query, CancellationToken.None);

                //Assert
                result.Status.Should().Be(ResultStatus.NotFound);

                customerRepoMock.Verify(x => x.SingleOrDefaultAsync(
                    It.IsAny<GetCustomerWithAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ));
            }

            [Theory]
            [AutoMapperData(typeof(MappingProfile))]
            public async Task return_notfound_given_customer_does_not_exist(
                [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
                GetPreferredAddressQueryHandler sut,
                GetPreferredAddressQuery query
            )
            {
                //Arrange
                query.AddressType = "Billing";

                customerRepoMock.Setup(_ => _.SingleOrDefaultAsync(
                    It.IsAny<GetCustomerWithAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync((Entities.StoreCustomer?)null);

                //Act
                var result = await sut.Handle(query, CancellationToken.None);

                //Assert
                result.Status.Should().Be(ResultStatus.NotFound);

                customerRepoMock.Verify(x => x.SingleOrDefaultAsync(
                    It.IsAny<GetCustomerWithAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ));
            }
        }

        public class GetPreferredShippingAddressForStoreCustomer
        {
            [Theory]
            [AutoMapperData(typeof(MappingProfile))]
            public async Task return_success_given_shipping_address_exists(
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

                customerRepoMock.Setup(_ => _.SingleOrDefaultAsync(
                    It.IsAny<GetCustomerWithAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(customer);

                //Act
                var result = await sut.Handle(query, CancellationToken.None);

                //Assert
                result.IsSuccess.Should().BeTrue();

                result.Value.Should().BeEquivalentTo(
                    address,
                    opt => opt.Excluding(_ => _.Path.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase))
                );

                customerRepoMock.Verify(x => x.SingleOrDefaultAsync(
                    It.IsAny<GetCustomerWithAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ));
            }

            [Theory]
            [AutoMapperData(typeof(MappingProfile))]
            public async Task return_success_given_main_office_address_exists(
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

                customerRepoMock.Setup(_ => _.SingleOrDefaultAsync(
                    It.IsAny<GetCustomerWithAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(customer);

                //Act
                var result = await sut.Handle(query, CancellationToken.None);

                //Assert
                result.IsSuccess.Should().BeTrue();

                result.Value.Should().BeEquivalentTo(
                    address,
                    opt => opt.Excluding(_ => _.Path.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase))
                );

                customerRepoMock.Verify(x => x.SingleOrDefaultAsync(
                    It.IsAny<GetCustomerWithAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ));
            }

            [Theory]
            [AutoMapperData(typeof(MappingProfile))]
            public async Task return_success_given_home_address_exists(
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

                customerRepoMock.Setup(_ => _.SingleOrDefaultAsync(
                    It.IsAny<GetCustomerWithAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(customer);

                //Act
                var result = await sut.Handle(query, CancellationToken.None);

                //Assert
                result.IsSuccess.Should().BeTrue();

                result.Value.Should().BeEquivalentTo(
                    address,
                    opt => opt.Excluding(_ => _.Path.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase))
                );

                customerRepoMock.Verify(x => x.SingleOrDefaultAsync(
                    It.IsAny<GetCustomerWithAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ));
            }

            [Theory]
            [AutoMapperData(typeof(MappingProfile))]
            public async Task return_notfound_given_no_address_found(
                [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
                GetPreferredAddressQueryHandler sut,
                GetPreferredAddressQuery query,
                Entities.StoreCustomer customer
            )
            {
                //Arrange
                query.AddressType = "Shipping";

                customerRepoMock.Setup(_ => _.SingleOrDefaultAsync(
                    It.IsAny<GetCustomerWithAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(customer);

                //Act
                var result = await sut.Handle(query, CancellationToken.None);

                //Assert
                result.Status.Should().Be(ResultStatus.NotFound);

                customerRepoMock.Verify(x => x.SingleOrDefaultAsync(
                    It.IsAny<GetCustomerWithAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ));
            }

            [Theory]
            [AutoMapperData(typeof(MappingProfile))]
            public async Task return_notfound_given_customer_does_not_exist(
                [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
                GetPreferredAddressQueryHandler sut,
                GetPreferredAddressQuery query
            )
            {
                //Arrange
                query.AddressType = "Shipping";

                customerRepoMock.Setup(_ => _.SingleOrDefaultAsync(
                    It.IsAny<GetCustomerWithAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync((Entities.StoreCustomer?)null);

                //Act
                var result = await sut.Handle(query, CancellationToken.None);

                //Assert
                result.Status.Should().Be(ResultStatus.NotFound);

                customerRepoMock.Verify(x => x.SingleOrDefaultAsync(
                    It.IsAny<GetCustomerWithAddressesSpecification>(),
                    It.IsAny<CancellationToken>()
                ));
            }
        }
    }
}
