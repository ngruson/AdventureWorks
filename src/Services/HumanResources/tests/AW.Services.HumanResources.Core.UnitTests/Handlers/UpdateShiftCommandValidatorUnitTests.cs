using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.Handlers.UpdateShift;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using FluentValidation.TestHelper;
using Moq;

namespace AW.Services.HumanResources.Core.UnitTests.Handlers
{
    public class UpdateShiftCommandValidatorUnitTests
    {
        [Theory, AutoMoqData]
        public async Task no_validation_errors_given_valid_command(
            [Frozen] Mock<IRepository<Entities.Shift>> repository,
            UpdateShiftCommandValidator sut,
            UpdateShiftCommand command
        )
        {
            //Arrange
            repository.Setup(_ => _.AnyAsync(
                It.IsAny<GetShiftSpecification>(),
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
        public async Task validation_error_given_no_shift_object_id(
            UpdateShiftCommandValidator sut,
            UpdateShiftCommand command
        )
        {
            //Arrange
            command.Shift!.ObjectId = Guid.Empty;

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Shift!.ObjectId)
                .WithErrorMessage("ObjectId is required");
        }

        [Theory, AutoMoqData]
        public async Task validation_error_given_no_shift_name(
            [Frozen] Mock<IRepository<Entities.Shift>> repository,
            UpdateShiftCommandValidator sut,
            UpdateShiftCommand command
        )
        {
            //Arrange
            repository.Setup(_ => _.AnyAsync(
                It.IsAny<GetShiftSpecification>(),
                It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(true);

            command.Shift!.Name = null;

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Shift!.Name)
                .WithErrorMessage("Name is required");
        }

        [Theory, AutoMoqData]
        public async Task validation_error_given_no_shift_starttime(
            [Frozen] Mock<IRepository<Entities.Shift>> repository,
            UpdateShiftCommandValidator sut,
            UpdateShiftCommand command
        )
        {
            //Arrange
            repository.Setup(_ => _.AnyAsync(
                It.IsAny<GetShiftSpecification>(),
                It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(true);

            command.Shift!.StartTime = default;

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Shift!.StartTime)
                .WithErrorMessage("Start time is required");
        }

        [Theory, AutoMoqData]
        public async Task validation_error_given_no_shift_endtime(
            [Frozen] Mock<IRepository<Entities.Shift>> repository,
            UpdateShiftCommandValidator sut,
            UpdateShiftCommand command
        )
        {
            //Arrange
            repository.Setup(_ => _.AnyAsync(
                It.IsAny<GetShiftSpecification>(),
                It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(true);

            command.Shift!.EndTime = default;

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Shift!.EndTime)
                .WithErrorMessage("End time is required");
        }
    }
}
