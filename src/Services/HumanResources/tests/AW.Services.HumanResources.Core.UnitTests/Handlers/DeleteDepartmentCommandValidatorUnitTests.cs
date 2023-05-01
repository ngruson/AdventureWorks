using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.Handlers.DeleteDepartment;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using FluentValidation.TestHelper;
using Moq;

namespace AW.Services.HumanResources.Core.UnitTests.Handlers
{
    public class DeleteDepartmentCommandValidatorUnitTests
    {
        [Theory, AutoMoqData]
        public async Task no_validation_errors_given_valid_command(
            [Frozen] Mock<IRepository<Entities.Department>> departmentRepoMock,
            DeleteDepartmentCommandValidator sut,
            DeleteDepartmentCommand command
        )
        {
            //Arrange
            departmentRepoMock.Setup(_ => _.AnyAsync(
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

        [Theory, AutoMoqData]
        public async Task validation_error_given_no_name(
            DeleteDepartmentCommandValidator sut,
            DeleteDepartmentCommand command
        )
        {
            //Arrange
            command.Name = null;

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Name)
                .WithErrorMessage("Name is required");
        }

        [Theory, AutoMoqData]
        public async Task validation_error_given_department_does_not_exist(
            [Frozen] Mock<IRepository<Entities.Department>> departmentRepoMock,
            DeleteDepartmentCommandValidator sut,
            DeleteDepartmentCommand command
        )
        {
            //Arrange
            departmentRepoMock.Setup(_ => _.AnyAsync(
                    It.IsAny<GetDepartmentSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(false);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Name)
                .WithErrorMessage("Department does not exist");
        }
    }
}
