using AutoFixture.Xunit2;
using Moq;
using Xunit;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.Shift.Handlers.DeleteShift;
using AW.UI.Web.Infrastructure.Api.Interfaces;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.Shift.Handlers
{
    public class DeleteShiftCommandUnitTests
    {
        [Theory, AutoMoqData]
        public async Task delete_shift_given_name(
            [Frozen] Mock<IShiftApiClient> shiftApiClient,
            DeleteShiftCommandHandler sut,
            DeleteShiftCommand command
        )
        {
            //Arrange

            //Act
            await sut.Handle(command, CancellationToken.None);

            //Assert
            shiftApiClient.Verify(_ => _.DeleteShift(
                command.ObjectId
            ));
        }
    }
}
