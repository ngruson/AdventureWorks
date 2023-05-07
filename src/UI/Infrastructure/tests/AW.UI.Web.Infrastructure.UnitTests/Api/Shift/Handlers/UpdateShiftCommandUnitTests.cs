using AutoFixture.Xunit2;
using Moq;
using AW.SharedKernel.UnitTesting;
using Xunit;
using AW.UI.Web.Infrastructure.Api.Shift.Handlers.UpdateShift;
using AW.UI.Web.Infrastructure.Api.Interfaces;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.Shift.Handlers
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
                It.IsAny<Infrastructure.Api.Shift.Handlers.UpdateShift.Shift>()
            ));
        }
    }
}
