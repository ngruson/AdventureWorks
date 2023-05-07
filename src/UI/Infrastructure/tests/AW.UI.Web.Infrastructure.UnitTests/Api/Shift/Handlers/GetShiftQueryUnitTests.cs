using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using Xunit;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using AW.UI.Web.Infrastructure.Api.Shift.Handlers.GetShift;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.Shift.Handlers
{
    public class GetShiftQueryUnitTests
    {
        [Theory, AutoMoqData]
        public async Task return_shift_given_shift_exists(
            [Frozen] Mock<IShiftApiClient> shiftApiClient,
            GetShiftQueryHandler sut,
            GetShiftQuery query,
            Infrastructure.Api.Shift.Handlers.GetShift.Shift expected
        )
        {
            //Arrange
            shiftApiClient.Setup(_ => _.GetShift(query.ObjectId))
                .ReturnsAsync(expected);

            //Act
            var actual = await sut.Handle(query, CancellationToken.None);

            //Assert
            actual.Should().Be(expected);
            shiftApiClient.Verify(_ => _.GetShift(query.ObjectId));
        }

        [Theory, AutoMoqData]
        public async Task throw_argumentnullexception_given_shift_is_null(
            [Frozen] Mock<IShiftApiClient> shiftApiClient,
            GetShiftQueryHandler sut,
            GetShiftQuery query
        )
        {
            //Arrange
            shiftApiClient.Setup(_ => _.GetShift(query.ObjectId))
                .ReturnsAsync((Infrastructure.Api.Shift.Handlers.GetShift.Shift?)null);

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>();

            shiftApiClient.Verify(_ => _.GetShift(query.ObjectId));
        }
    }
}
