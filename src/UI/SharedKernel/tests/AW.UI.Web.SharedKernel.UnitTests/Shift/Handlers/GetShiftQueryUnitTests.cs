using AutoFixture.Xunit2;
using AW.UI.Web.SharedKernel.Shift.Handlers.GetShift;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using FluentAssertions;
using Moq;

namespace AW.UI.Web.SharedKernel.UnitTests.Shift.Handlers
{
    public class GetShiftQueryUnitTests
    {
        [Theory, AutoMoqData]
        public async Task return_shift_given_shift_exists(
            [Frozen] Mock<IShiftApiClient> shiftApiClient,
            GetShiftQueryHandler sut,
            GetShiftQuery query,
            SharedKernel.Shift.Handlers.GetShift.Shift expected
        )
        {
            //Arrange
            shiftApiClient.Setup(_ => _.GetShift(query.Name))
                .ReturnsAsync(expected);

            //Act
            var actual = await sut.Handle(query, CancellationToken.None);

            //Assert
            actual.Should().Be(expected);
            shiftApiClient.Verify(_ => _.GetShift(query.Name));
        }

        [Theory, AutoMoqData]
        public async Task throw_argumentnullexception_given_shift_is_null(
            [Frozen] Mock<IShiftApiClient> shiftApiClient,
            GetShiftQueryHandler sut,
            GetShiftQuery query
        )
        {
            //Arrange

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>();

            shiftApiClient.Verify(_ => _.GetShift(query.Name));
        }
    }
}
