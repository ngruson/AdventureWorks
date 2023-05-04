using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.Handlers.DeleteShift;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using FluentValidation.TestHelper;
using Moq;

namespace AW.Services.HumanResources.Core.UnitTests.Handlers
{
    public class DeleteShiftCommandValidatorUnitTests
    {
        [Theory, AutoMoqData]
        public async Task no_validation_errors_given_valid_command(
            [Frozen] Mock<IRepository<Entities.Shift>> shiftRepoMock,
            DeleteShiftCommandValidator sut,
            DeleteShiftCommand command
        )
        {
            //Arrange
            shiftRepoMock.Setup(_ => _.AnyAsync(
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
        public async Task validation_error_given_no_name(
            DeleteShiftCommandValidator sut,
            DeleteShiftCommand command
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
        public async Task validation_error_given_shift_does_not_exist(
            [Frozen] Mock<IRepository<Entities.Shift>> shiftRepoMock,
            DeleteShiftCommandValidator sut,
            DeleteShiftCommand command
        )
        {
            //Arrange
            shiftRepoMock.Setup(_ => _.AnyAsync(
                    It.IsAny<GetShiftSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(false);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Name)
                .WithErrorMessage("Shift does not exist");
        }
    }
}
