using AutoFixture.Xunit2;
using AW.UI.Web.SharedKernel.Shift.Handlers.CreateShift;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using Moq;

namespace AW.UI.Web.SharedKernel.UnitTests.Shift.Handlers
{
    public class CreateShiftCommandUnitTests
    {
        [Theory, AutoMoqData]
        public async Task create_shift_given_command_is_valid(
            [Frozen] Mock<IShiftApiClient> shiftApiClient,
            CreateShiftCommandHandler sut,
            CreateShiftCommand command
        )
        {
            //Arrange

            //Act
            await sut.Handle(command, CancellationToken.None);

            //Assert
            shiftApiClient.Verify(_ => _.CreateShift(
                command.Shift
            ));
        }
    }
}
