using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.Handlers.DeleteEmployee;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using FluentValidation.TestHelper;
using Moq;

namespace AW.Services.HumanResources.Core.UnitTests.Handlers
{
    public class DeleteEmployeeCommandValidatorUnitTests
    {
        [Theory, AutoMoqData]
        public async Task no_validation_errors_given_valid_command(
            [Frozen] Mock<IRepository<Entities.Employee>> employeeRepo,
            DeleteEmployeeCommandValidator sut,
            DeleteEmployeeCommand command
        )
        {
            //Arrange
            employeeRepo.Setup(_ => _.AnyAsync(
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

        [Theory, AutoMoqData]
        public async Task validation_error_given_no_object_id(
            DeleteEmployeeCommandValidator sut,
            DeleteEmployeeCommand command
        )
        {
            //Arrange
            command.ObjectId = Guid.Empty;

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.ObjectId)
                .WithErrorMessage("ObjectId is required");
        }

        [Theory, AutoMoqData]
        public async Task validation_error_given_employee_does_not_exist(
            [Frozen] Mock<IRepository<Entities.Employee>> employeeRepo,
            DeleteEmployeeCommandValidator sut,
            DeleteEmployeeCommand command
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
            result.ShouldHaveValidationErrorFor(command => command.ObjectId)
                .WithErrorMessage("Employee does not exist");
        }
    }
}
