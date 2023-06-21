using AutoFixture.Xunit2;
using AW.Services.Customer.Core.Handlers.CreateCustomer;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests.Handlers
{
    public class CreateCustomerCommandValidatorUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async Task no_validation_errors_given_valid_command(
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            CreateCustomerCommandValidator sut,
            Core.Handlers.CreateCustomer.Customer customer,
            string accountNumber
        )
        {
            //Arrange
            customer.AccountNumber = accountNumber[..10];
            var command = new CreateCustomerCommand(customer);

            customerRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.Customer?)null);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_customer(
            CreateCustomerCommandValidator sut
        )
        {
            //Arrange
            var command = new CreateCustomerCommand(null);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Customer)
                .WithErrorMessage("Customer is required");
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_accountnumber(
            CreateCustomerCommandValidator sut
        )
        {
            //Arrange
            var command = new CreateCustomerCommand(new StoreCustomer());

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Customer!.AccountNumber)
                .WithErrorMessage("Account number is required");
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_accountnumber_too_long(
            CreateCustomerCommandValidator sut,
            CreateCustomerCommand command,
            string accountNumber
        )
        {
            //Arrange
            command.Customer!.AccountNumber = accountNumber;

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Customer!.AccountNumber)
                .WithErrorMessage("Account number must not exceed 10 characters");
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_accountnumber_already_exists(
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            CreateCustomerCommandValidator sut,
            CreateCustomerCommand command,
            string accountNumber
        )
        {
            //Arrange
            command.Customer!.AccountNumber = accountNumber![..10];

            customerRepoMock.Setup(_ => _.AnyAsync(
                    It.IsAny<GetCustomerByAccountNumberSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(true);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Customer!.AccountNumber)
                .WithErrorMessage("Account number already exists");
        }
    }
}
