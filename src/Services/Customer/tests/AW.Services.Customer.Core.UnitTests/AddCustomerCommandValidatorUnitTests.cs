using AW.Services.Customer.Core.Handlers.AddCustomer;
using AW.Services.Customer.Core.Specifications;
using AW.SharedKernel.Interfaces;
using FluentValidation.TestHelper;
using Moq;
using System.Threading;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests
{
    public class AddCustomerCommandValidatorUnitTests
    {
        [Fact]
        public void TestValidate_WithCustomer_NoValidationError()
        {
            //Arrange
            var mockRepository = new Mock<IRepository<Entities.Customer>>();

            var sut = new AddCustomerCommandValidator(mockRepository.Object);
            var command = new AddCustomerCommand
            {
                Customer = new StoreCustomerDto
                {
                    AccountNumber = "AW00000001"
                }
            };

            var result = sut.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(command => command.Customer);
        }

        [Fact]
        public void TestValidate_WithoutCustomer_ValidationError()
        {
            //Arrange
            var mockRepository = new Mock<IRepository<Entities.Customer>>();

            var sut = new AddCustomerCommandValidator(mockRepository.Object);
            var command = new AddCustomerCommand();

            var result = sut.TestValidate(command);
            result.ShouldHaveValidationErrorFor(command => command.Customer)
                .WithErrorMessage("Customer is required");
        }

        [Fact]
        public void TestValidate_WithAccountNumber_NoValidationError()
        {
            //Arrange
            var mockRepository = new Mock<IRepository<Entities.Customer>>();

            var sut = new AddCustomerCommandValidator(mockRepository.Object);
            var command = new AddCustomerCommand
            {
                Customer = new StoreCustomerDto
                {
                    AccountNumber = "AW00000001"
                }
            };

            var result = sut.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(command => command.Customer.AccountNumber);
        }

        [Fact]
        public void TestValidate_WithEmptyAccountNumber_ValidationError()
        {
            //Arrange
            var mockRepository = new Mock<IRepository<Entities.Customer>>();

            var sut = new AddCustomerCommandValidator(mockRepository.Object);
            var command = new AddCustomerCommand
            {
                Customer = new StoreCustomerDto
                {
                    AccountNumber = ""
                }
            };

            var result = sut.TestValidate(command);
            result.ShouldHaveValidationErrorFor(command => command.Customer.AccountNumber)
                .WithErrorMessage("Account number is required");
        }

        [Fact]
        public void TestValidate_WithAccountNumberTooLong_ValidationError()
        {
            //Arrange
            var mockRepository = new Mock<IRepository<Entities.Customer>>();

            var sut = new AddCustomerCommandValidator(mockRepository.Object);
            var command = new AddCustomerCommand
            {
                Customer = new StoreCustomerDto
                {
                    AccountNumber = "AW000000011"
                }
            };

            var result = sut.TestValidate(command);
            result.ShouldHaveValidationErrorFor(command => command.Customer.AccountNumber)
                .WithErrorMessage("Account number must not exceed 10 characters");
        }

        [Fact]
        public void TestValidate_WithAccountNumberAlreadyExists_ValidationError()
        {
            //Arrange
            var mockRepository = new Mock<IRepository<Entities.Customer>>();
            mockRepository.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new Entities.StoreCustomer
            {
                AccountNumber = "AW00000001"
            });

            var sut = new AddCustomerCommandValidator(mockRepository.Object);
            var command = new AddCustomerCommand
            {
                Customer = new StoreCustomerDto
                {
                    AccountNumber = "AW00000001"
                }
            };

            var result = sut.TestValidate(command);
            result.ShouldHaveValidationErrorFor(command => command.Customer.AccountNumber)
                .WithErrorMessage("Account number already exists");
        }
    }
}