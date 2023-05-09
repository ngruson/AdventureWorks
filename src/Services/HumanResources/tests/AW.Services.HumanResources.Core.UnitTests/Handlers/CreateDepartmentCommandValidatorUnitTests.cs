using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.Handlers.CreateDepartment;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using FluentValidation.TestHelper;
using Moq;

namespace AW.Services.HumanResources.Core.UnitTests.Handlers
{
    public class CreateDepartmentCommandValidatorUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async Task no_validation_errors_given_valid_command(
            CreateDepartmentCommandValidator sut,
            CreateDepartmentCommand command
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
        public async Task validation_error_given_no_department(
            CreateDepartmentCommandValidator sut,
            CreateDepartmentCommand command
        )
        {
            //Arrange
            command.Department = null;

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Department)
                .WithErrorMessage("Department is required");
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_department_name(
            CreateDepartmentCommandValidator sut,
            CreateDepartmentCommand command
        )
        {
            //Arrange
            command.Department!.Name = null;

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Department!.Name)
                .WithErrorMessage("Name is required");
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_department_groupname(
            CreateDepartmentCommandValidator sut,
            CreateDepartmentCommand command
        )
        {
            //Arrange
            command.Department!.GroupName = null;

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Department.GroupName)
                .WithErrorMessage("Group name is required");
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_department_already_exists(
            [Frozen] Mock<IRepository<Entities.Department>> departmentRepoMock,
            CreateDepartmentCommandValidator sut,
            CreateDepartmentCommand command,
            Entities.Department department
        )
        {
            //Arrange
            departmentRepoMock.Setup(_ => _.AnyAsync(
                    It.IsAny<GetDepartmentByNameSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(true);

            command.Department!.Name = department.Name;

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Department.Name)
                .WithErrorMessage("Department already exists");
        }
    }
}
