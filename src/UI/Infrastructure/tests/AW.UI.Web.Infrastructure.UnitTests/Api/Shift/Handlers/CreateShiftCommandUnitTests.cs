using AutoFixture.Xunit2;
using Moq;
using Xunit;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using AW.UI.Web.Infrastructure.Api.Shift.Handlers.CreateShift;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.Shift.Handlers
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
