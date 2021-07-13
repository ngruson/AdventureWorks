using AutoFixture.Xunit2;
using AW.Services.Customer.Core.Handlers.UpdateCustomer;
using AW.Services.Customer.Core.Specifications;
using AW.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentValidation.TestHelper;
using Moq;
using System.Threading;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests
{
    public class UpdateCustomerCommandValidatorUnitTests
    {
        [Theory]
        [AutoMoqData]
        public void TestValidate_ValidCommand_NoValidationError(
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            Entities.StoreCustomer customer,
            UpdateCustomerCommandValidator sut,
            UpdateCustomerCommand command
        )
        {
            //Arrange
            command.Customer.AccountNumber = "1";

            customerRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(customer);

            //Act
            var result = sut.TestValidate(command);

            //Assert
            result.ShouldNotHaveValidationErrorFor(command => command.Customer);
            result.ShouldNotHaveValidationErrorFor(command => command.Customer.AccountNumber);
        }

        [Theory]
        [AutoMoqData]
        public void TestValidate_WithoutCustomer_ValidationError(
            UpdateCustomerCommandValidator sut,
            UpdateCustomerCommand command
        )
        {
            //Arrange
            command.Customer = null;

            //Act
            var result = sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Customer)
                .WithErrorMessage("Customer is required");
        }

        [Theory]
        [AutoMoqData]
        public void TestValidate_WithEmptyAccountNumber_ValidationError(
            UpdateCustomerCommandValidator sut,
            UpdateCustomerCommand command
        )
        {
            //Arrange
            command.Customer.AccountNumber = null;

            //Act
            var result = sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Customer.AccountNumber)
                .WithErrorMessage("Account number is required");
        }

        [Theory]
        [AutoMoqData]
        public void TestValidate_WithAccountNumberTooLong_ValidationError(
            UpdateCustomerCommandValidator sut,
            UpdateCustomerCommand command
        )
        {
            //Arrange
            command.Customer.AccountNumber = "AW000000011";
            
            //Act
            var result = sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Customer.AccountNumber)
                .WithErrorMessage("Account number must not exceed 10 characters");
        }

        [Theory]
        [AutoMoqData]
        public void TestValidate_CustomerNotFound_ValidationError(
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            UpdateCustomerCommandValidator sut,
            UpdateCustomerCommand command,
            StoreCustomerDto customerDto
        )
        {
            //Arrange
            command.Customer = customerDto; 
            command.Customer.AccountNumber = command.Customer.AccountNumber.Substring(0, 10);

            customerRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.StoreCustomer)null);

            //Act
            var result = sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Customer.AccountNumber)
                .WithErrorMessage("Customer does not exist");
        }
    }
}