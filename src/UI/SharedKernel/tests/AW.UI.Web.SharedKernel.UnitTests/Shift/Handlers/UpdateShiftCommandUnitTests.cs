using AutoFixture.Xunit2;
using AW.UI.Web.SharedKernel.Shift.Handlers.UpdateShift;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using FluentAssertions;
using Moq;

namespace AW.UI.Web.SharedKernel.UnitTests.Shift.Handlers
{
    public class UpdateShiftCommandUnitTests
    {
        [Theory, AutoMoqData]
        public async Task update_shift_given_command_is_valid(
            [Frozen] Mock<IShiftApiClient> shiftApiClient,
            UpdateShiftCommandHandler sut,
            UpdateShiftCommand command
        )
        {
            //Arrange

            //Act
            await sut.Handle(command, CancellationToken.None);

            //Assert
            shiftApiClient.Verify(_ => _.UpdateShift(
                It.IsAny<UpdateShiftCommand>()
            ));
        }

        [Theory, AutoMoqData]
        public async Task throw_argumentnullexception_given_command_is_invalid(
            [Frozen] Mock<IShiftApiClient> shiftApiClient,
            UpdateShiftCommandHandler sut
        )
        {
            //Arrange

            //Act
            Func<Task> func = async () => await sut.Handle(
                new UpdateShiftCommand(null!, null!), CancellationToken.None
            );

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>();

            shiftApiClient.Verify(_ => _.UpdateShift(
                    It.IsAny<UpdateShiftCommand>()
                )
                , Times.Never
            );
        }
    }
}
