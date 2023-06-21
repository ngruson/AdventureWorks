using AutoFixture.Xunit2;
using AW.Services.Customer.Core.Handlers.UpdateCustomer;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests.Handlers
{
    public class UpdateCustomerCommandValidatorUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async Task no_validation_errors_given_valid_command(
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            UpdateCustomerCommandValidator sut,
            Entities.StoreCustomer customer,
            string accountNumber
        )
        {
            //Arrange
            var command = new UpdateCustomerCommand(
                new StoreCustomer
                {
                    ObjectId = customer.ObjectId,
                    AccountNumber = accountNumber![0..10]
                }
            );

            customerRepoMock.Setup(x => x.AnyAsync(
                It.IsAny<GetCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(true);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [AutoMoqData]
        public void validation_error_given_no_customer(
            UpdateCustomerCommandValidator sut
        )
        {
            //Arrange
            var command = new UpdateCustomerCommand(null);

            //Act
            var result = sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Customer)
                .WithErrorMessage("Customer is required");
        }

        [Theory]
        [AutoMoqData]
        public void validation_error_given_no_accountnumber(
            UpdateCustomerCommandValidator sut
        )
        {
            //Arrange
            var command = new UpdateCustomerCommand(new StoreCustomer());

            //Act
            var result = sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Customer!.AccountNumber)
                .WithErrorMessage("Account number is required");
        }

        [Theory]
        [AutoMoqData]
        public void validation_error_given_accountnumber_too_long(
            UpdateCustomerCommandValidator sut,
            string accountNumber
        )
        {
            //Arrange
            var command = new UpdateCustomerCommand(
                new StoreCustomer {  AccountNumber = accountNumber }
            );

            //Act
            var result = sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Customer!.AccountNumber)
                .WithErrorMessage("Account number must not exceed 10 characters");
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_customer_does_not_exist(
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepo,
            UpdateCustomerCommandValidator sut,
            StoreCustomer customer
        )
        {
            //Arrange
            customer.AccountNumber = customer.AccountNumber![0..10];

            var command = new UpdateCustomerCommand(
                customer
            );

            customerRepo.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.StoreCustomer?)null);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Customer!.ObjectId)
                .WithErrorMessage("Customer does not exist");
        }
    }
}
