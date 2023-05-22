using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.Handlers.UpdateEmployee;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using FluentValidation.TestHelper;
using Moq;

namespace AW.Services.HumanResources.Core.UnitTests.Handlers
{
    public class UpdateEmployeeCommandValidatorUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async Task no_validation_errors_given_valid_command(
            [Frozen] Mock<IRepository<Entities.Employee>> repository,
            UpdateEmployeeCommandValidator sut,
            UpdateEmployeeCommand command
        )
        {
            //Arrange
            repository.Setup(_ => _.AnyAsync(
                It.IsAny<GetEmployeeSpecification>(),
                It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(true);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_first_name(
            [Frozen] Mock<IRepository<Entities.Employee>> repository,
            UpdateEmployeeCommandValidator sut,
            UpdateEmployeeCommand command
        )
        {
            //Arrange
            repository.Setup(_ => _.AnyAsync(
                It.IsAny<GetEmployeeSpecification>(),
                It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(true);

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
            [Frozen] Mock<IRepository<Entities.Employee>> repository,
            UpdateEmployeeCommandValidator sut,
            UpdateEmployeeCommand command
        )
        {
            //Arrange
            repository.Setup(_ => _.AnyAsync(
                It.IsAny<GetEmployeeSpecification>(),
                It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(true);

            command.Employee.Name!.LastName = null;

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Employee.Name!.LastName)
                .WithErrorMessage("'Employee Name Last Name' must not be empty.");
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_employee_does_not_exist(
            [Frozen] Mock<IRepository<Entities.Employee>> employeeRepo,
            UpdateEmployeeCommandValidator sut,
            UpdateEmployeeCommand command
        )
        {
            //Arrange
            employeeRepo.Setup(_ => _.AnyAsync(
                    It.IsAny<GetEmployeeSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(false);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Employee.ObjectId)
                .WithErrorMessage("Employee does not exist");
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_national_id_number(
            [Frozen] Mock<IRepository<Entities.Employee>> repository,
            UpdateEmployeeCommandValidator sut,
            UpdateEmployeeCommand command
        )
        {
            //Arrange
            repository.Setup(_ => _.AnyAsync(
                It.IsAny<GetEmployeeSpecification>(),
                It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(true);

            command.Employee.NationalIDNumber = null;

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Employee.NationalIDNumber)
                .WithErrorMessage("'Employee National ID Number' must not be empty.");
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_login_id(
            [Frozen] Mock<IRepository<Entities.Employee>> repository,
            UpdateEmployeeCommandValidator sut,
            UpdateEmployeeCommand command
        )
        {
            //Arrange
            repository.Setup(_ => _.AnyAsync(
                It.IsAny<GetEmployeeSpecification>(),
                It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(true);

            command.Employee.LoginID = null;

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Employee.LoginID)
                .WithErrorMessage("'Employee Login ID' must not be empty.");
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_job_title(
            [Frozen] Mock<IRepository<Entities.Employee>> repository,
            UpdateEmployeeCommandValidator sut,
            UpdateEmployeeCommand command
        )
        {
            //Arrange
            repository.Setup(_ => _.AnyAsync(
                It.IsAny<GetEmployeeSpecification>(),
                It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(true);

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
            [Frozen] Mock<IRepository<Entities.Employee>> repository,
            UpdateEmployeeCommandValidator sut,
            UpdateEmployeeCommand command
        )
        {
            //Arrange
            repository.Setup(_ => _.AnyAsync(
                It.IsAny<GetEmployeeSpecification>(),
                It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(true);

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
            [Frozen] Mock<IRepository<Entities.Employee>> repository,
            UpdateEmployeeCommandValidator sut,
            UpdateEmployeeCommand command
        )
        {
            //Arrange
            repository.Setup(_ => _.AnyAsync(
                It.IsAny<GetEmployeeSpecification>(),
                It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(true);

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
            [Frozen] Mock<IRepository<Entities.Employee>> repository,
            UpdateEmployeeCommandValidator sut,
            UpdateEmployeeCommand command
        )
        {
            //Arrange
            repository.Setup(_ => _.AnyAsync(
                It.IsAny<GetEmployeeSpecification>(),
                It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(true);

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
            [Frozen] Mock<IRepository<Entities.Employee>> repository,
            UpdateEmployeeCommandValidator sut,
            UpdateEmployeeCommand command
        )
        {
            //Arrange
            repository.Setup(_ => _.AnyAsync(
                It.IsAny<GetEmployeeSpecification>(),
                It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(true);

            command.Employee.HireDate = DateTime.MinValue;

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Employee.HireDate)
                .WithErrorMessage("'Employee Hire Date' must not be empty.");
        }
    }
}
