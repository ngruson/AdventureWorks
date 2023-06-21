using FluentAssertions;
using Moq;
using Xunit;
using AW.UI.Web.Admin.Mvc.Services;
using AW.UI.Web.Admin.Mvc.ViewModels.Customer;
using AW.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using AutoFixture.Xunit2;
using MediatR;
using AutoMapper;
using AW.UI.Web.Infrastructure.Api.ReferenceData.Handlers.GetContactTypes;
using AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetCustomers;
using AW.UI.Web.Infrastructure.Api.Customer.Handlers.UpdateCustomer;
using AW.UI.Web.Infrastructure.Api.ReferenceData.Handlers.GetStatesProvinces;
using AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetCustomer;
using AW.UI.Web.Infrastructure.Api.ReferenceData.Handlers.GetTerritories;
using AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetStoreCustomer;
using AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetIndividualCustomer;

namespace AW.UI.Web.Admin.Mvc.UnitTests.Services;

public class CustomerServiceUnitTests
{
    public class GetCustomers
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task return_customers_given_customers_exist(
            [Frozen] Mock<IMediator> mockMediator,
            CustomerService sut,
            List<Infrastructure.Api.Customer.Handlers.GetCustomers.StoreCustomer> customers
        )
        {
            //Arrange
            customers.ForEach(_ =>
                _.CustomerType = CustomerType.Store
            );

            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetCustomersQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(customers
                .Cast<Infrastructure.Api.Customer.Handlers.GetCustomers.Customer?>()
                .ToList()
            );

            //Act
            var response = await sut.GetCustomers();

            //Assert
            response.Should().BeEquivalentTo(customers);
        }
    }

    public class GetDetailStore
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task return_customer_given_customer_exists(
            [Frozen] Mock<IMediator> mockMediator,
            CustomerService sut,
            Infrastructure.Api.Customer.Handlers.GetCustomer.StoreCustomer customer
        )
        {
            //Arrange
            customer.CustomerType = CustomerType.Store;

            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(customer);

            //Act
            var viewModel = await sut.GetDetailStore(customer.ObjectId);

            //Assert
            viewModel.Should().BeEquivalentTo(customer);

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            );
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task throw_argumentnullexception_given_customer_not_found(
            [Frozen] Mock<IMediator> mockMediator,
            CustomerService sut,
            Infrastructure.Api.Customer.Handlers.GetCustomer.StoreCustomer customer
        )
        {
            //Arrange
            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync((Infrastructure.Api.Customer.Handlers.GetCustomer.Customer?)null);

            //Act
            Func<Task> func = async () => await sut.GetDetailStore(customer.ObjectId);

            //Assert
            await func.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Value cannot be null. (Parameter 'customer')");

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            );
        }
    }

    public class GetDetailIndividual
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task return_customer_given_customer_exists(
            [Frozen] Mock<IMediator> mockMediator,
            CustomerService sut,
            Infrastructure.Api.Customer.Handlers.GetCustomer.IndividualCustomer customer
        )
        {
            //Arrange
            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(customer);

            //Act
            var viewModel = await sut.GetDetailIndividual(customer.ObjectId);

            //Assert
            viewModel.Should().BeEquivalentTo(customer);

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            );
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task throw_argumentnullexception_given_customer_not_found(
            [Frozen] Mock<IMediator> mockMediator,
            CustomerService sut,
            Infrastructure.Api.Customer.Handlers.GetCustomer.IndividualCustomer customer
        )
        {
            //Arrange
            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync((Infrastructure.Api.Customer.Handlers.GetCustomer.Customer?)null);

            //Act
            Func<Task> func = async () => await sut.GetDetailIndividual(customer.ObjectId);

            //Assert
            await func.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Value cannot be null. (Parameter 'customer')");

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            );
        }
    }

    public class UpdateStore
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task return_customer_given_customer_was_updated(
            [Frozen] Mock<IMediator> mockMediator,
            CustomerService sut,
            StoreCustomerViewModel viewModel,
            Infrastructure.Api.Customer.Handlers.GetStoreCustomer.StoreCustomer customer,
            Infrastructure.Api.Customer.Handlers.GetCustomer.StoreCustomer customerGet
        )
        {
            //Arrange
            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetStoreCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
           .ReturnsAsync(customer);

            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
           .ReturnsAsync(customerGet);

            //Act
            await sut.UpdateStore(viewModel);

            //Assert
            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetStoreCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            );

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            );

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<UpdateCustomerCommand>(),
                    It.IsAny<CancellationToken>()
                )
            );
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task throw_argumentnullexception_given_customer_not_found(
            [Frozen] Mock<IMediator> mockMediator,
            CustomerService sut,
            StoreCustomerViewModel viewModel
        )
        {
            //Arrange
            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetStoreCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
           .ReturnsAsync((Infrastructure.Api.Customer.Handlers.GetStoreCustomer.StoreCustomer?)null);

            //Act
            Func<Task> func = async () => await sut.UpdateStore(viewModel);

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'customer')");

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetStoreCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            );

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<UpdateCustomerCommand>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }
    }

    public class UpdateIndividual
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task return_customer_given_customer_was_updated(
            [Frozen] Mock<IMediator> mockMediator,
            CustomerService sut,
            IndividualCustomerViewModel viewModel,
            Infrastructure.Api.Customer.Handlers.GetIndividualCustomer.IndividualCustomer customer,
            Infrastructure.Api.Customer.Handlers.GetCustomer.IndividualCustomer customerGet
        )
        {
            //Arrange
            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetIndividualCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
           .ReturnsAsync(customer);

            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
           .ReturnsAsync(customerGet);

            //Act
            var updatedCustomer = await sut.UpdateIndividual(viewModel);

            //Assert
            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetIndividualCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            );

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            );

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<UpdateCustomerCommand>(),
                    It.IsAny<CancellationToken>()
                )
            );
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task throw_argumentnullexception_given_customer_not_found(
            [Frozen] Mock<IMediator> mockMediator,
            CustomerService sut,
            IndividualCustomerViewModel viewModel
        )
        {
            //Arrange
            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetIndividualCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
           .ReturnsAsync((Infrastructure.Api.Customer.Handlers.GetIndividualCustomer.IndividualCustomer?)null);

            //Act
            Func<Task> func = async () => await sut.UpdateIndividual(viewModel);

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'customer')");

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetIndividualCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            );

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<UpdateCustomerCommand>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }
    }

    public class AddAddress
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task return_updated_customer_given_address_was_added(
            [Frozen] Mock<IMediator> mockMediator,
            CustomerService sut,
            EditCustomerAddressViewModel viewModel,
            Infrastructure.Api.Customer.Handlers.GetCustomer.StoreCustomer customer
        )
        {
            //Arrange
            customer.CustomerType = CustomerType.Store;

            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(customer);

            //Act
            var updatedCustomer = await sut.AddAddress<StoreCustomerViewModel>(viewModel);

            //Assert
            updatedCustomer.Should().BeEquivalentTo(customer);

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<UpdateCustomerCommand>(),
                    It.IsAny<CancellationToken>()
                )
            );
        }

        [Theory, AutoMoqData]
        public async Task throw_argumentexception_given_customer_not_found(
            [Frozen] Mock<IMediator> mockMediator,
            CustomerService sut,
            EditCustomerAddressViewModel viewModel
        )
        {
            //Arrange
            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync((Infrastructure.Api.Customer.Handlers.GetCustomer.Customer?)null);

            //Act
            Func<Task> func = async () => await sut.AddAddress<StoreCustomerViewModel>(viewModel);

            //Assert
            await func.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Value cannot be null. (Parameter 'customer')");

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<UpdateCustomerCommand>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }
    }

    public class UpdateAddress
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task return_customer_given_address_was_updated(
            [Frozen] Mock<IMediator> mockMediator,
            CustomerService sut,
            EditCustomerAddressViewModel viewModel,
            Infrastructure.Api.Customer.Handlers.GetCustomer.StoreCustomer customer,
            Infrastructure.Api.Customer.Handlers.GetCustomer.CustomerAddress customerAddress
        )
        {
            //Arrange
            customer.CustomerType = CustomerType.Store;
            customer.Addresses!.Add(customerAddress);
            viewModel.ObjectId = customerAddress.ObjectId;

            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(customer);

            //Act
            var updatedCustomer = await sut.UpdateAddress<StoreCustomerViewModel>(viewModel);

            //Assert
            updatedCustomer.Should().BeEquivalentTo(customer);

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Exactly(2)
            );

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<UpdateCustomerCommand>(),
                    It.IsAny<CancellationToken>()
                )
            );
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task throw_argumentnullexception_given_customer_not_found(
            [Frozen] Mock<IMediator> mockMediator,
            CustomerService sut,
            EditCustomerAddressViewModel viewModel,
            Infrastructure.Api.Customer.Handlers.GetCustomer.StoreCustomer customer
        )
        {
            //Arrange
            viewModel.AddressType = customer.Addresses?[0].AddressType;

            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(null as Infrastructure.Api.Customer.Handlers.GetCustomer.StoreCustomer);

            //Act

            Func<Task> func = async () => await sut.UpdateAddress<StoreCustomerViewModel>(viewModel);

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'customer')");

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<UpdateCustomerCommand>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task throw_argumentnullexception_given_address_not_found(
            [Frozen] Mock<IMediator> mockMediator,
            CustomerService sut,
            EditCustomerAddressViewModel viewModel,
            Infrastructure.Api.Customer.Handlers.GetCustomer.StoreCustomer customer
        )
        {
            //Arrange
            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(customer);

            //Act

            Func<Task> func = async () => await sut.UpdateAddress<StoreCustomerViewModel>(viewModel);

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'addressToUpdate')");

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<UpdateCustomerCommand>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }
    }

    public class GetStatesProvinces
    {
        [Theory, AutoMoqData]
        public async Task GetStatesProvinces_ReturnsList(
            [Frozen] Mock<IMediator> mockMediator,
            CustomerService sut,
            List<StateProvince> statesProvinces,
            string countryRegionCode
        )
        {
            //Arrange
            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetStatesProvincesQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(statesProvinces);

            //Act
            var actual = await sut.GetStatesProvinces(countryRegionCode);

            //Assert
            actual.Should().BeEquivalentTo(statesProvinces);
        }
    }

    public class GetStatesProvincesJson
    {
        [Theory, AutoMoqData]
        public async Task GetStatesProvincesJson_ReturnsViewModel(
            [Frozen] Mock<IMediator> mockMediator,
            CustomerService sut,
            string countryRegionCode,
            List<StateProvince> statesProvinces
        )
        {
            //Arrange
            mockMediator.Setup(x => x.Send(
                    It.IsAny<GetStatesProvincesQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(statesProvinces);

            //Act
            var viewModel = await sut.GetStatesProvincesJson(countryRegionCode);

            //Assert
            viewModel.Should().BeEquivalentTo(statesProvinces);
        }
    }

    public class DeleteAddress
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task return_updated_customer_given_address_was_deleted(
            [Frozen] Mock<IMediator> mockMediator,
            CustomerService sut,
            Infrastructure.Api.Customer.Handlers.GetCustomer.IndividualCustomer customer,
            Guid objectId
        )
        {
            //Arrange
            customer.Addresses!.Add(
                new Infrastructure.Api.Customer.Handlers.GetCustomer.CustomerAddress(
                    objectId
                )
            );

            mockMediator
                .Setup(_ => _.Send(
                    It.IsAny<GetCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
           )
           .ReturnsAsync(customer);

            //Act
            var updatedCustomer = await sut.DeleteAddress<IndividualCustomerViewModel>(
                customer.ObjectId,
                objectId
            );

            //Assert
            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Exactly(2)
            );
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task throw_argumentnullexception_given_address_not_found(
            [Frozen] Mock<IMediator> mockMediator,
            CustomerService sut,
            Infrastructure.Api.Customer.Handlers.GetCustomer.IndividualCustomer customer,
            Guid objectId
        )
        {
            //Arrange
            mockMediator
                .Setup(_ => _.Send(
                    It.IsAny<GetCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
           )
           .ReturnsAsync(customer);

            //Act
            Func<Task> func = async () => await sut.DeleteAddress<StoreCustomerViewModel>(
                customer.ObjectId,
                objectId
            );

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'addressToDelete')");

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );
        }
    }

    public class AddContact
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task update_customer_given_contact_was_added(
            [Frozen] Mock<IMediator> mockMediator,
            CustomerService sut,
            StoreCustomerContactViewModel viewModel,
            Infrastructure.Api.Customer.Handlers.GetStoreCustomer.StoreCustomer customer
        )
        {
            //Arrange
            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetStoreCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(customer);

            //Act
            await sut.AddContact(viewModel);

            //Assert
            mockMediator.Verify(_ => _.Send(
                    It.IsAny<UpdateCustomerCommand>(),
                    It.IsAny<CancellationToken>()
                )
            );
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task throw_argumentnullexception_given_customer_not_found(
            [Frozen] Mock<IMediator> mockMediator,
            CustomerService sut,
            StoreCustomerContactViewModel viewModel,
            Infrastructure.Api.Customer.Handlers.GetStoreCustomer.StoreCustomer customer
        )
        {
            //Arrange
            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetStoreCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync((Infrastructure.Api.Customer.Handlers.GetStoreCustomer.StoreCustomer?)null);

            //Act
            Func<Task> func = async () => await sut.AddContact(viewModel);

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'customer')");

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<UpdateCustomerCommand>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }
    }

    public class GetCustomerContact
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task return_contact_given_customer_and_contact_are_found(
            [Frozen] Mock<IMediator> mockMediator,
            CustomerService sut,
            Infrastructure.Api.Customer.Handlers.GetStoreCustomer.StoreCustomer customer,
            Infrastructure.Api.Customer.Handlers.GetStoreCustomer.StoreCustomerContact contact
        )
        {
            //Arrange
            customer.Contacts.Add(contact);

            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetStoreCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(customer);

            //Act
            var viewModel = await sut.GetCustomerContact(customer.ObjectId, contact.ObjectId);

            //Assert            
            viewModel.Should().BeEquivalentTo(contact);

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetStoreCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            );
        }

        [Theory, AutoMoqData]
        public async Task throw_argumentnullexception_given_customer_not_found(
            [Frozen] Mock<IMediator> mockMediator,
            CustomerService sut,
            Infrastructure.Api.Customer.Handlers.GetStoreCustomer.StoreCustomer customer,
            Guid contactId
        )
        {
            //Arrange
            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetStoreCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync((Infrastructure.Api.Customer.Handlers.GetStoreCustomer.StoreCustomer?)null);

            //Act
            Func<Task> func = async () => await sut.GetCustomerContact(customer.ObjectId, contactId);

            //Assert            
            await func.Should().ThrowAsync<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'customer')");

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetStoreCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            );
        }

        [Theory, AutoMoqData]
        public async Task throw_argumentnullexception_given_contact_not_found(
            [Frozen] Mock<IMediator> mockMediator,
            CustomerService sut,
            Infrastructure.Api.Customer.Handlers.GetStoreCustomer.StoreCustomer customer,
            Guid contactId
        )
        {
            //Arrange
            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetStoreCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(customer);

            //Act
            Func<Task> func = async () => await sut.GetCustomerContact(customer.ObjectId, contactId);

            //Assert            
            await func.Should().ThrowAsync<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'contact')");

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetStoreCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            );
        }
    }

    public class UpdateContact
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task return_customer_given_contact_was_updated(
            [Frozen] Mock<IMediator> mockMediator,
            CustomerService sut,
            Infrastructure.Api.Customer.Handlers.GetStoreCustomer.StoreCustomer customer,
            Infrastructure.Api.Customer.Handlers.GetStoreCustomer.StoreCustomerContact contact,
            EditStoreCustomerContactViewModel viewModel
        )
        {
            //Arrange
            customer.Contacts.Add(contact);
            viewModel.CustomerContact!.ObjectId = contact.ObjectId;

            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetStoreCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(customer);

            //Act
            var updatedCustomer = await sut.UpdateContact(
                customer.ObjectId,
                viewModel
            );

            //Assert
            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetStoreCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            );

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<UpdateCustomerCommand>(),
                    It.IsAny<CancellationToken>()
                )
            );
        }

        [Theory, AutoMoqData]
        public async Task throw_argumentnullexception_given_customer_not_found(
            [Frozen] Mock<IMediator> mockMediator,
            CustomerService sut,
            Infrastructure.Api.Customer.Handlers.GetStoreCustomer.StoreCustomer customer,
            EditStoreCustomerContactViewModel viewModel
        )
        {
            //Arrange
            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetStoreCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync((Infrastructure.Api.Customer.Handlers.GetStoreCustomer.StoreCustomer?)null);

            //Act
            Func<Task> func = async () => await sut.UpdateContact(customer.ObjectId, viewModel);

            //Assert            
            await func.Should().ThrowAsync<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'customer')");

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetStoreCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            );

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<UpdateCustomerCommand>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task throw_argumentnullexception_given_contact_not_found(
            [Frozen] Mock<IMediator> mockMediator,
            CustomerService sut,
            Infrastructure.Api.Customer.Handlers.GetStoreCustomer.StoreCustomer customer,
            EditStoreCustomerContactViewModel viewModel
        )
        {
            //Arrange
            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetStoreCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(customer);

            //Act
            Func<Task> func = async () => await sut.UpdateContact(customer.ObjectId, viewModel);

            //Assert            
            await func.Should().ThrowAsync<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'contact')");

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetStoreCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            );

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<UpdateCustomerCommand>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }
    }

    public class DeleteContact
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task ok_given_contact_was_deleted(
            [Frozen] Mock<IMediator> mockMediator,
            CustomerService sut,
            Infrastructure.Api.Customer.Handlers.GetStoreCustomer.StoreCustomer customer,
            Infrastructure.Api.Customer.Handlers.GetCustomer.StoreCustomer customerGet,
            Guid objectId
        )
        {
            //Arrange
            customer.Contacts.Add(new Infrastructure.Api.Customer.Handlers.GetStoreCustomer.StoreCustomerContact(
                    objectId
                )
            );

            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(customerGet);

            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetStoreCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(customer);

            //Act
            await sut.DeleteContact(
                customer.ObjectId,
                objectId
            );

            //Assert
            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            );

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetStoreCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            );
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task throw_argumentexception_given_customer_not_found(
            [Frozen] Mock<IMediator> mockMediator,
            CustomerService sut,
            Infrastructure.Api.Customer.Handlers.GetStoreCustomer.StoreCustomer customer,
            Guid objectId
        )
        {
            //Arrange
            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetStoreCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync((Infrastructure.Api.Customer.Handlers.GetStoreCustomer.StoreCustomer?)null);

            //Act
            Func<Task> func = async () => await sut.DeleteContact(
                customer.ObjectId,
                objectId
            );

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'customer')");

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetStoreCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            );
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task throw_argumentexception_given_contact_not_found(
            [Frozen] Mock<IMediator> mockMediator,
            CustomerService sut,
            Infrastructure.Api.Customer.Handlers.GetStoreCustomer.StoreCustomer customer,
            Guid objectId
        )
        {
            //Arrange
            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetStoreCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(customer);

            //Act
            Func<Task> func = async () => await sut.DeleteContact(
                customer.ObjectId,
                objectId
            );

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'contact')");

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetStoreCustomerQuery>(),
                    It.IsAny<CancellationToken>()
                )
            );
        }
    }
}
