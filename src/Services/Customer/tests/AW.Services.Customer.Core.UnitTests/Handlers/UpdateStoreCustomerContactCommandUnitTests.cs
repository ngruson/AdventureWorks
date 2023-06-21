using Ardalis.Result;
using AutoFixture.Xunit2;
using AW.Services.Customer.Core.AutoMapper;
using AW.Services.Customer.Core.Handlers.UpdateStoreCustomerContact;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.Services.SharedKernel.ValueTypes;
using AW.SharedKernel.UnitTesting;
using AW.SharedKernel.ValueTypes;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests.Handlers
{
    public class UpdateStoreCustomerContactCommandUnitTests
    {
        [Theory]
        [AutoMapperData(typeof(MappingProfile))]
        public async Task return_success_given_customer_and_contact_exist(
            [Frozen] Mock<IRepository<Entities.StoreCustomer>> customerRepoMock,
            Entities.StoreCustomer customer,
            UpdateStoreCustomerContactCommandHandler sut,
            Entities.Person contactPerson,
            Guid customerId,
            string contactType
        )
        {
            //Arrange
            var command = new UpdateStoreCustomerContactCommand(
                customerId,
                new StoreCustomerContact
                {
                    ContactType = contactType,
                    ContactPerson = new Person
                    {
                        Name = contactPerson.Name,
                        EmailAddresses = new List<PersonEmailAddress>
                        {
                            new PersonEmailAddress
                            {
                                EmailAddress = EmailAddress.Create("test@test.com").Value
                            }
                        }
                    }
                }
            );

            customer.AddContact(
                new Entities.StoreCustomerContact(
                    command.CustomerContact.ContactType!,
                    contactPerson
                )
            );

            customerRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetStoreCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(customer);

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();

            customerRepoMock.Verify(x => x.UpdateAsync(
                It.IsAny<Entities.StoreCustomer>(),
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory, AutoMoqData]
        public async Task return_invalid_given_command_is_invalid(
            [Frozen] Mock<IRepository<Entities.StoreCustomer>> customerRepoMock,
            [Frozen] Mock<IValidator<UpdateStoreCustomerContactCommand>> validator,
            UpdateStoreCustomerContactCommandHandler sut,
            UpdateStoreCustomerContactCommand command,
            List<ValidationFailure> failures
        )
        {
            // Arrange
            validator.Setup(_ => _.ValidateAsync(
                    command,
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(new ValidationResult(failures));

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Status.Should().Be(ResultStatus.Invalid);

            customerRepoMock.Verify(x => x.UpdateAsync(
                    It.IsAny<Entities.StoreCustomer>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }

        [Theory]
        [AutoMoqData]
        public async Task return_notfound_given_customer_does_not_exist(
            [Frozen] Mock<IRepository<Entities.StoreCustomer>> customerRepoMock,
            UpdateStoreCustomerContactCommandHandler sut,
            Guid customerId,
            string contactType
        )
        {
            // Arrange
            var command = new UpdateStoreCustomerContactCommand(
                customerId,
                new StoreCustomerContact
                {
                    ContactType = contactType,
                    ContactPerson = new Person
                    {
                        EmailAddresses = new List<Core.Handlers.UpdateStoreCustomerContact.PersonEmailAddress>()
                    }
                }
            );

            customerRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetStoreCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.StoreCustomer?)null);

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Status.Should().Be(ResultStatus.NotFound);
        }

        [Theory]
        [AutoMoqData]
        public async Task return_notfound_given_contact_does_not_exist(
            UpdateStoreCustomerContactCommandHandler sut,
            Guid customerId,
            string contactType,
            NameFactory contactName
        )
        {
            //Arrange
            var command = new UpdateStoreCustomerContactCommand(
                customerId,
                new StoreCustomerContact
                {
                    ContactType = contactType,
                    ContactPerson = new Person
                    {
                        Name = contactName,
                        EmailAddresses = new List<Core.Handlers.UpdateStoreCustomerContact.PersonEmailAddress>()
                    }
                }
            );

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Status.Should().Be(ResultStatus.NotFound);
        }

        [Theory, AutoMoqData]
        public async Task return_error_given_exception_was_thrown(
            [Frozen] Mock<IValidator<UpdateStoreCustomerContactCommand>> validator,
            Mock<IRepository<Entities.StoreCustomer>> customerRepo,
            UpdateStoreCustomerContactCommandHandler sut,
            UpdateStoreCustomerContactCommand command
        )
        {
            //Arrange
            validator.Setup(_ => _.ValidateAsync(
                    command,
                    It.IsAny<CancellationToken>()
                )
            )
            .ThrowsAsync(new Exception());

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Status.Should().Be(ResultStatus.Error);

            customerRepo.Verify(x => x.UpdateAsync(
                    It.IsAny<Entities.StoreCustomer>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }
    }
}
