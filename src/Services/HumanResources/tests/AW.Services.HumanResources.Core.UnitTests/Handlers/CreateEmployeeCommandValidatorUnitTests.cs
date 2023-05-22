using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.Handlers.CreateEmployee;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using FluentValidation.TestHelper;
using Moq;

namespace AW.Services.HumanResources.Core.UnitTests.Handlers
{
    public class CreateEmployeeCommandValidatorUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async Task no_validation_errors_given_valid_command(
            CreateEmployeeCommandValidator sut,
            CreateEmployeeCommand command
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
        public async Task validation_error_given_no_firstname(
            CreateEmployeeCommandValidator sut,
            CreateEmployeeCommand command
        )
        {
            //Arrange
            command.Employee.Name!.FirstName = null;

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Employee.Name!.FirstName)
                .WithErrorMessage("'Employee Name First Name' must not be empty.");
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_lastname(
            CreateEmployeeCommandValidator sut,
            CreateEmployeeCommand command
        )
        {
            //Arrange
            command.Employee.Name!.LastName = null;

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Employee.Name!.LastName)
                .WithErrorMessage("'Employee Name Last Name' must not be empty.");
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_login_id(
            CreateEmployeeCommandValidator sut,
            CreateEmployeeCommand command
        )
        {
            //Arrange
            command.Employee.LoginID = null;

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Employee.LoginID)
                .WithErrorMessage("'Employee Login ID' must not be empty.");
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_login_id_already_exists(
            [Frozen] Mock<IRepository<Entities.Employee>> repository,
            CreateEmployeeCommandValidator sut,
            CreateEmployeeCommand command
        )
        {
            //Arrange
            repository.Setup(_ => _.AnyAsync(
                    It.IsAny<GetEmployeeByLoginIDSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(true);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Employee.LoginID)
                .WithErrorMessage("'Employee Login ID' already exists");
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_national_id_number(
            CreateEmployeeCommandValidator sut,
            CreateEmployeeCommand command
        )
        {
            //Arrange
            command.Employee.NationalIDNumber = null;

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Employee.NationalIDNumber)
                .WithErrorMessage("'Employee National ID Number' must not be empty.");
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_job_title(
            CreateEmployeeCommandValidator sut,
            CreateEmployeeCommand command
        )
        {
            //Arrange
            command.Employee.JobTitle = null;

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Employee.JobTitle)
                .WithErrorMessage("'Employee Job Title' must not be empty.");
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_birth_date(
            CreateEmployeeCommandValidator sut,
            CreateEmployeeCommand command
        )
        {
            //Arrange
            command.Employee.BirthDate = DateTime.MinValue;

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Employee.BirthDate)
                .WithErrorMessage("'Employee Birth Date' must not be empty.");
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_marital_status(
            CreateEmployeeCommandValidator sut,
            CreateEmployeeCommand command
        )
        {
            //Arrange
            command.Employee.MaritalStatus = null;

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Employee.MaritalStatus)
                .WithErrorMessage("'Employee Marital Status' must not be empty.");
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_gender(
            CreateEmployeeCommandValidator sut,
            CreateEmployeeCommand command
        )
        {
            //Arrange
            command.Employee.Gender = null;

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Employee.Gender)
                .WithErrorMessage("'Employee Gender' must not be empty.");
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_hire_date(
            CreateEmployeeCommandValidator sut,
            CreateEmployeeCommand command
        )
        {
            //Arrange
            command.Employee.HireDate = DateTime.MinValue;

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Employee.HireDate)
                .WithErrorMessage("'Employee Hire Date' must not be empty.");
        }
    }
}
