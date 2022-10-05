﻿using AutoFixture.Xunit2;
using AW.Services.Customer.Core.Handlers.AddCustomer;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentValidation.TestHelper;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests.Handlers
{
    public class AddCustomerCommandValidatorUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async Task TestValidate_ValidCommand_NoValidationError(
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            AddCustomerCommandValidator sut,
            AddCustomerCommand command
        )
        {
            //Arrange
            command.Customer.AccountNumber = "1";

            customerRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.Customer)null);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldNotHaveValidationErrorFor(command => command.Customer);
            result.ShouldNotHaveValidationErrorFor(command => command.Customer.AccountNumber);
        }

        [Theory]
        [AutoMoqData]
        public async Task TestValidate_WithoutCustomer_ValidationError(
            AddCustomerCommandValidator sut,
            AddCustomerCommand command
        )
        {
            //Arrange
            command.Customer = null;

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Customer)
                .WithErrorMessage("Customer is required");
        }

        [Theory]
        [AutoMoqData]
        public async Task TestValidate_WithEmptyAccountNumber_ValidationError(
            AddCustomerCommandValidator sut,
            AddCustomerCommand command
        )
        {
            //Arrange
            command.Customer.AccountNumber = null;

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Customer.AccountNumber)
                .WithErrorMessage("Account number is required");
        }

        [Theory]
        [AutoMoqData]
        public async Task TestValidate_WithAccountNumberTooLong_ValidationError(
            AddCustomerCommandValidator sut,
            AddCustomerCommand command
        )
        {
            //Arrange
            command.Customer.AccountNumber = "AW000000011";

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Customer.AccountNumber)
                .WithErrorMessage("Account number must not exceed 10 characters");
        }

        [Theory]
        [AutoMoqData]
        public async Task TestValidate_WithAccountNumberAlreadyExists_ValidationError(
            //[Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            AddCustomerCommandValidator sut,
            AddCustomerCommand command
        )
        {
            //Arrange
            command.Customer.AccountNumber = "AW00000001";

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Customer.AccountNumber)
                .WithErrorMessage("Account number already exists");
        }
    }
}