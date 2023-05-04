using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.Exceptions;
using AW.Services.HumanResources.Core.Handlers.DeleteShift;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;

namespace AW.Services.HumanResources.Core.UnitTests.Handlers
{
    public class DeleteShiftCommandUnitTests
    {
        [Theory, AutoMoqData]
        public async Task delete_shift_given_shift_exists(
            [Frozen] Mock<IRepository<Entities.Shift>> shiftRepoMock,
            DeleteShiftCommandHandler sut,
            DeleteShiftCommand command,
            Entities.Shift shift
        )
        {
            //Arrange
            shiftRepoMock.Setup(_ => _.SingleOrDefaultAsync(
                    It.IsAny<GetShiftSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(shift);

            //Act
            await sut.Handle(command, CancellationToken.None);

            //Assert
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
        public async Task throw_shiftnotfoundexception_given_shift_does_not_exist(
            [Frozen] Mock<IRepository<Entities.Shift>> shiftRepoMock,
            DeleteShiftCommandHandler sut,
            DeleteShiftCommand command,
            Entities.Shift shift
        )
        {
            //Arrange

            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ShiftNotFoundException>();

            shiftRepoMock.Verify(x => x.SingleOrDefaultAsync(
                    It.IsAny<GetShiftSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            );

            shiftRepoMock.Verify(x => x.DeleteAsync(
                    shift,
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }
    }
}
