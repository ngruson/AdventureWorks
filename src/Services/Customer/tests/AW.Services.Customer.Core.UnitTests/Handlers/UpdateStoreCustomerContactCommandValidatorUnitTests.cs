using AW.Services.Customer.Core.Handlers.UpdateStoreCustomerContact;
using AW.SharedKernel.UnitTesting;
using AW.SharedKernel.ValueTypes;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests.Handlers
{
    public class UpdateStoreCustomerContactCommandValidatorUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async Task no_validation_errors_given_valid_command(
            UpdateStoreCustomerContactCommandValidator sut,
            UpdateStoreCustomerContactCommand command
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
            UpdateStoreCustomerContactCommandValidator sut,
            StoreCustomerContact contact
        )
        {
            //Arrange
            var command = new UpdateStoreCustomerContactCommand(
                Guid.Empty,
                contact
            );

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerId);
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_objectid(
            UpdateStoreCustomerContactCommandValidator sut,
            Guid customerId,
            StoreCustomerContact contact
        )
        {
            //Arrange
            var command = new UpdateStoreCustomerContactCommand(
                customerId,
                new StoreCustomerContact(
                    Guid.Empty,
                    contact.ContactType!,
                    contact.ContactPerson!
                )
            );

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerContact.ObjectId);
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_contacttype(
            UpdateStoreCustomerContactCommandValidator sut,
            Guid customerId,
            StoreCustomerContact contact
        )
        {
            //Arrange
            var command = new UpdateStoreCustomerContactCommand(
                customerId,
                new StoreCustomerContact(
                    contact.ObjectId,
                    string.Empty,
                    contact.ContactPerson!
                )
            );

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerContact.ContactType);
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_firstname(
            UpdateStoreCustomerContactCommandValidator sut,
            Guid customerId,
            StoreCustomerContact contact
        )
        {
            //Arrange
            var command = new UpdateStoreCustomerContactCommand(
                customerId,
                new StoreCustomerContact(
                    contact.ObjectId,
                    contact.ContactType!,
                    new Person(
                        contact.ContactPerson!.Title!,
                        new NameFactory(
                            string.Empty,
                            contact.ContactPerson.Name!.MiddleName,
                            contact.ContactPerson!.Name.LastName!
                        ),
                        contact.ContactPerson.Suffix!
                    )
                )
            );

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerContact.ContactPerson!.Name!.FirstName);
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_lastname(
            UpdateStoreCustomerContactCommandValidator sut,
            Guid customerId,
            StoreCustomerContact contact
        )
        {
            //Arrange
            var command = new UpdateStoreCustomerContactCommand(
                customerId,
                new StoreCustomerContact(
                    contact.ObjectId,
                    contact.ContactType!,
                    new Person(
                        contact.ContactPerson!.Title!,
                        new NameFactory(
                            contact.ContactPerson!.Name!.FirstName!,
                            contact.ContactPerson.Name!.MiddleName,
                            string.Empty
                        ),
                        contact.ContactPerson.Suffix!
                    )
                )
            );

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerContact.ContactPerson!.Name!.LastName);
        }
    }
}
