using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.Handlers.CreateShift;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using FluentValidation.TestHelper;
using Moq;

namespace AW.Services.HumanResources.Core.UnitTests.Handlers
{
    public class CreateShiftCommandValidatorUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async Task no_validation_errors_given_valid_command(
            CreateShiftCommandValidator sut,
            CreateShiftCommand command
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
        public async Task validation_error_given_no_shift(
            CreateShiftCommandValidator sut,
            CreateShiftCommand command
        )
        {
            //Arrange
            command.Shift = null;

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Shift)
                .WithErrorMessage("Shift is required");
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_shift_name(
            CreateShiftCommandValidator sut,
            CreateShiftCommand command
        )
        {
            //Arrange
            command.Shift!.Name = null;

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Shift!.Name)
                .WithErrorMessage("Name is required");
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_shift_startdate(
            CreateShiftCommandValidator sut,
            CreateShiftCommand command
        )
        {
            //Arrange
            command.Shift!.StartTime = default;

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Shift!.StartTime)
                .WithErrorMessage("Start time is required");
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_shift_enddate(
            CreateShiftCommandValidator sut,
            CreateShiftCommand command
        )
        {
            //Arrange
            command.Shift!.EndTime = default;

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Shift!.EndTime)
                .WithErrorMessage("End time is required");
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_shift_already_exists(
            [Frozen] Mock<IRepository<Entities.Shift>> shiftRepoMock,
            CreateShiftCommandValidator sut,
            CreateShiftCommand command,
            Entities.Shift shift
        )
        {
            //Arrange
            shiftRepoMock.Setup(_ => _.AnyAsync(
                    It.IsAny<GetShiftSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(true);

            command.Shift!.Name = shift.Name;

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Shift!.Name)
                .WithErrorMessage("Shift already exists");
        }
    }
}
