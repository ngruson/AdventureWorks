using AutoFixture.Xunit2;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using AW.UI.Web.SharedKernel.Shift.Handlers.GetShifts;
using FluentAssertions;
using Moq;

namespace AW.UI.Web.SharedKernel.UnitTests.Shift.Handlers
{
    public class GetShiftsQueryUnitTests
    {
        [Theory, AutoMoqData]
        public async Task ReturnShiftsGivenShiftsExist(
            [Frozen] Mock<IShiftApiClient> mockShiftApiClient,
            GetShiftsQueryHandler sut,
            GetShiftsQuery query,
            List<SharedKernel.Shift.Handlers.GetShifts.Shift> shifts
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
        public async Task ThrowArgumentNullExceptionGivenShiftsAreNull(
            [Frozen] Mock<IShiftApiClient> mockShiftApiClient,
            GetShiftsQueryHandler sut,
            GetShiftsQuery query
        )
        {
            //Arrange

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>();

            mockShiftApiClient.Verify(_ => _.GetShifts());
        }
    }
}
