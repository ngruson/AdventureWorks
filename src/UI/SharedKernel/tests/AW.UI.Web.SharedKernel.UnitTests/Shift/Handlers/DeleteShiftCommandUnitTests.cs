using AutoFixture.Xunit2;
using AW.UI.Web.SharedKernel.Shift.Handlers.DeleteShift;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using FluentAssertions;
using Moq;

namespace AW.UI.Web.SharedKernel.UnitTests.Shift.Handlers
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
                command
            ));
        }

        [Theory, AutoMoqData]
        public async Task throw_argumentexception_given_empty_name(
            [Frozen] Mock<IShiftApiClient> shiftApiClient,
            DeleteShiftCommandHandler sut
        )
        {
            //Arrange

            //Act
            Func<Task> func = async () => await sut.Handle(
                new DeleteShiftCommand(null!), 
                CancellationToken.None
            );

            //Assert
            await func.Should().ThrowAsync<ArgumentException>();

            shiftApiClient.Verify(_ => _.DeleteShift(
                    It.IsAny<DeleteShiftCommand>()
                )
                , Times.Never
            );
        }
    }
}
