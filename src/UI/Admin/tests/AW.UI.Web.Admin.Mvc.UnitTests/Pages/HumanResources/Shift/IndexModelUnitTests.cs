using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Admin.Mvc.Pages.HumanResources.Shift;
using AW.UI.Web.Admin.Mvc.Services;
using AW.UI.Web.Admin.Mvc.ViewModels.Shift;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.UI.Web.Admin.Mvc.UnitTests.Pages.HumanResources.Shift;

public class IndexModelUnitTests
{
    public class Get
    {
        [Theory, AutoMoqData]
        public async Task get_shifts(
            [Frozen] Mock<IShiftService> shiftService,
            [Greedy] IndexModel sut,
            List<ShiftViewModel> shifts
        )
        {
            //Arrange
            shiftService.Setup(_ => _.GetShifts())
                .ReturnsAsync(shifts);

            //Act
            await sut.OnGetAsync();

            //Assert
            sut.Shifts.Should().BeEquivalentTo(shifts);

            shiftService.Verify(_ => _.GetShifts());
        }
    }
}
