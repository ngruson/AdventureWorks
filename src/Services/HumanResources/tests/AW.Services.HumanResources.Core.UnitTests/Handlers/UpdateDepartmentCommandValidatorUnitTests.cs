using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.Handlers.UpdateDepartment;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using FluentValidation.TestHelper;
using Moq;

namespace AW.Services.HumanResources.Core.UnitTests.Handlers
{
    public class UpdateDepartmentCommandValidatorUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async Task no_validation_errors_given_valid_command(
            [Frozen] Mock<IRepository<Entities.Department>> repository,
            UpdateDepartmentCommandValidator sut,
            UpdateDepartmentCommand command
        )
        {
            //Arrange
            repository.Setup(_ => _.AnyAsync(
                It.IsAny<GetDepartmentSpecification>(),
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
        public async Task validation_error_given_no_department_name(
            [Frozen] Mock<IRepository<Entities.Department>> repository,
            UpdateDepartmentCommandValidator sut,
            UpdateDepartmentCommand command
        )
        {
            //Arrange
            repository.Setup(_ => _.AnyAsync(
                It.IsAny<GetDepartmentSpecification>(),
                It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(true);

            command.Department.Name = null;

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Department.Name)
                .WithErrorMessage("Name is required");
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_department_groupname(
            [Frozen] Mock<IRepository<Entities.Department>> repository,
            UpdateDepartmentCommandValidator sut,
            UpdateDepartmentCommand command
        )
        {
            //Arrange
            repository.Setup(_ => _.AnyAsync(
                It.IsAny<GetDepartmentSpecification>(),
                It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(true);

            command.Department!.GroupName = null;

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Department.GroupName)
                .WithErrorMessage("Group name is required");
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_department_does_not_exist(
            [Frozen] Mock<IRepository<Entities.Department>> departmentRepoMock,
            UpdateDepartmentCommandValidator sut,
            UpdateDepartmentCommand command
        )
        {
            //Arrange
            departmentRepoMock.Setup(_ => _.AnyAsync(
                    It.IsAny<GetDepartmentSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(false);

            //command.Department!.Name = department.Name;

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Department.ObjectId)
                .WithErrorMessage("Department does not exist");
        }
    }
}
