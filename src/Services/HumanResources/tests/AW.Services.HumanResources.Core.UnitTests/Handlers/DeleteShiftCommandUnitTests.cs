using Ardalis.Result;
using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.Handlers.DeleteShift;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace AW.Services.HumanResources.Core.UnitTests.Handlers
{
    public class DeleteShiftCommandUnitTests
    {
        [Theory, AutoMoqData]
        public async Task return_success_given_shift_was_deleted(
            [Frozen] Mock<IValidator<DeleteShiftCommand>> validatorMock,
            [Frozen] Mock<IRepository<Entities.Shift>> shiftRepoMock,
            DeleteShiftCommandHandler sut,
            DeleteShiftCommand command,
            Entities.Shift shift
        )
        {
            //Arrange
            validatorMock.Setup(_ => _.ValidateAsync(
                    command,
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(new ValidationResult());

            shiftRepoMock.Setup(_ => _.SingleOrDefaultAsync(
                    It.IsAny<GetShiftSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(shift);

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();

            shiftRepoMock.Verify(x => x.SingleOrDefaultAsync(
                    It.IsAny<GetShiftSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            );

            shiftRepoMock.Verify(x => x.DeleteAsync(
                shift,
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory, AutoMoqData]
        public async Task return_invalid_given_command_was_invalid(
            [Frozen] Mock<IRepository<Entities.Shift>> shiftRepoMock,
            [Frozen] Mock<IValidator<DeleteShiftCommand>> validatorMock,
            DeleteShiftCommandHandler sut,
            DeleteShiftCommand command,
            List<ValidationFailure> failures
        )
        {
            //Arrange
            validatorMock.Setup(_ => _.ValidateAsync(
                    command,
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(new ValidationResult(failures));

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Status.Should().Be(ResultStatus.Invalid);

            shiftRepoMock.Verify(x => x.SingleOrDefaultAsync(
                    It.IsAny<GetShiftSpecification>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );

            shiftRepoMock.Verify(x => x.DeleteAsync(
                    It.IsAny<Entities.Shift>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }

        [Theory, AutoMoqData]
        public async Task return_notfound_given_shift_does_not_exist(
            [Frozen] Mock<IValidator<DeleteShiftCommand>> validatorMock,
            [Frozen] Mock<IRepository<Entities.Shift>> shiftRepoMock,
            DeleteShiftCommandHandler sut,
            DeleteShiftCommand command
        )
        {
            //Arrange
            validatorMock.Setup(_ => _.ValidateAsync(
                    command,
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(new ValidationResult());

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Status.Should().Be(ResultStatus.NotFound);

            shiftRepoMock.Verify(x => x.SingleOrDefaultAsync(
                    It.IsAny<GetShiftSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            );

            shiftRepoMock.Verify(x => x.DeleteAsync(
                    It.IsAny<Entities.Shift>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }

        [Theory, AutoMoqData]
        public async Task return_error_given_exception_was_thrown(
            DeleteShiftCommandHandler sut,
            DeleteShiftCommand command
        )
        {
            //Arrange

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Status.Should().Be(ResultStatus.Error);
        }
    }
}
