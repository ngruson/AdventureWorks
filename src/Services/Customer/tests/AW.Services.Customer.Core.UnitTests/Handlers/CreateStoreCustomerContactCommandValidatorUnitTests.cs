using AW.Services.Customer.Core.Handlers.CreateStoreCustomerContact;
using AW.SharedKernel.UnitTesting;
using AW.SharedKernel.ValueTypes;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests.Handlers
{
    public class CreateStoreCustomerContactCommandValidatorUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async Task no_validation_errors_given_valid_command(
            CreateStoreCustomerContactCommandValidator sut,
            CreateStoreCustomerContactCommand command
        )
        {
            //Arrange

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_customerid(
            CreateStoreCustomerContactCommandValidator sut,
            StoreCustomerContact contact
        )
        {
            //Arrange
            var command = new CreateStoreCustomerContactCommand(
                Guid.Empty,
                contact
            );

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(_ => _.CustomerId);
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_contacttype(
            CreateStoreCustomerContactCommandValidator sut,
            Person person
        )
        {
            //Arrange
            var command = new CreateStoreCustomerContactCommand(
                Guid.NewGuid(),
                new StoreCustomerContact
                {
                    ContactType = string.Empty,
                    ContactPerson = person
                }
            );

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(_ => _.CustomerContact.ContactType);
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_firstname(
            CreateStoreCustomerContactCommandValidator sut,
            StoreCustomerContact contact
        )
        {
            //Arrange
            var command = new CreateStoreCustomerContactCommand(
                Guid.NewGuid(),
                new StoreCustomerContact(
                    contact.ContactType!,
                    new Person(
                        contact.ContactPerson!.Title!,
                        new NameFactory(
                            string.Empty,
                            contact.ContactPerson!.Name!.MiddleName,
                            contact.ContactPerson.Name.LastName!
                        ),
                        contact.ContactPerson.Suffix!
                    )
                )
            );

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(_ => _.CustomerContact.ContactPerson!.Name!.FirstName);
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_lastname(
            CreateStoreCustomerContactCommandValidator sut,
            StoreCustomerContact contact
        )
        {
            //Arrange
            var command = new CreateStoreCustomerContactCommand(
                Guid.NewGuid(),
                new StoreCustomerContact(
                    contact.ContactType!,
                    new Person(
                        contact.ContactPerson!.Title!,
                        new NameFactory(
                            contact.ContactPerson.Name!.FirstName!,
                            contact.ContactPerson.Name.MiddleName,
                            string.Empty
                        ),
                        contact.ContactPerson.Suffix!
                    )
                )
            );

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(_ => _.CustomerContact.ContactPerson!.Name!.LastName);
        }
    }
}
