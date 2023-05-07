using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using AW.UI.Web.Infrastructure.Api.Shift.Handlers.GetShifts;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.Shift.Handlers
{
    public class GetShiftsQueryUnitTests
    {
        [Theory, AutoMoqData]
        public async Task return_shifts_given_shifts_exist(
            [Frozen] Mock<IShiftApiClient> mockShiftApiClient,
            GetShiftsQueryHandler sut,
            GetShiftsQuery query,
            List<Infrastructure.Api.Shift.Handlers.GetShifts.Shift> shifts
        )
        {
            //Arrange
            mockShiftApiClient.Setup(_ => _.GetShifts())
                .ReturnsAsync(shifts);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().BeEquivalentTo(shifts);

            mockShiftApiClient.Verify(_ => _.GetShifts());
        }

        [Theory, AutoMoqData]
        public async Task throw_argumentnullexception_given_shifts_are_null(
            [Frozen] Mock<IShiftApiClient> mockShiftApiClient,
            GetShiftsQueryHandler sut,
            GetShiftsQuery query
        )
        {
            //Arrange
            mockShiftApiClient.Setup(_ => _.GetShifts())
                .ReturnsAsync((List<Infrastructure.Api.Shift.Handlers.GetShifts.Shift>?)null);

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>();

            mockShiftApiClient.Verify(_ => _.GetShifts());
        }
    }
}
