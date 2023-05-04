using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Admin.Mvc.Pages.HumanResources.Shift;
using AW.UI.Web.Admin.Mvc.Services;
using Moq;
using Xunit;

namespace AW.UI.Web.Admin.Mvc.UnitTests.Pages.HumanResources.Shift
{
    public class DeleteModelUnitTests
    {
        public class Get
        {
            [Theory, AutoMoqData]
            public async Task delete_shifts(
                [Frozen] Mock<IShiftService> shiftService,
                [Greedy] DeleteModel sut,
                string[] shifts
            )
            {
                //Arrange

                //Act
                await sut.OnPostAsync(shifts);

                //Assert

                shiftService.Verify(_ => _.DeleteShift(
                        It.IsAny<string>()
                    ), 
                    Times.Exactly(shifts.Length)
                );
            }
        }
    }
}
