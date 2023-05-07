using Ardalis.Result;
using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.AutoMapper;
using AW.Services.HumanResources.Core.Handlers.CreateShift;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace AW.Services.HumanResources.Core.UnitTests.Handlers
{
    public class CreateShiftCommandUnitTests
    {
        [Theory]
        [AutoMapperData(typeof(MappingProfile))]
        public async Task return_success_given_shift_was_created(
            [Frozen] Mock<IValidator<CreateShiftCommand>> validatorMock,
            [Frozen] Mock<IRepository<Entities.Shift>> shiftRepoMock,
            CreateShiftCommandHandler sut,
            CreateShiftCommand command,
            Entities.Shift shift
        )
        {
            //Arrange
            command.Shift.StartTime = "07:00:00";
            command.Shift.EndTime = "09:00:00";

            validatorMock.Setup(_ => _.ValidateAsync(
                    command,
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(new ValidationResult());

            shiftRepoMock.Setup(_ => _.AddAsync(
                    It.IsAny<Entities.Shift>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(shift);

            //Act
            var actual = await sut.Handle(command, CancellationToken.None);

            //Assert

            shiftRepoMock.Verify(x => x.AddAsync(
                It.IsAny<Entities.Shift>(),
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory, AutoMoqData]
        public async Task return_invalid_given_command_was_invalid(
            [Frozen] Mock<IRepository<Entities.Shift>> shiftRepoMock,
            [Frozen] Mock<IValidator<CreateShiftCommand>> validatorMock,
            CreateShiftCommandHandler sut,
            CreateShiftCommand command,
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

            shiftRepoMock.Verify(x => x.AddAsync(
                    It.IsAny<Entities.Shift>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }

        [Theory, AutoMoqData]
        public async Task return_error_given_exception_was_thrown(
            [Frozen] Mock<IRepository<Entities.Shift>> shiftRepoMock,
            CreateShiftCommandHandler sut,
            CreateShiftCommand command
        )
        {
            //Arrange

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Status.Should().Be(ResultStatus.Error);

            shiftRepoMock.Verify(x => x.AddAsync(
                    It.IsAny<Entities.Shift>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }
    }
}
