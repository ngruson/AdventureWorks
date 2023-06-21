using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Admin.Mvc.Pages.HumanResources.Shift;
using AW.UI.Web.Admin.Mvc.Services;
using AW.UI.Web.Admin.Mvc.ViewModels.Shift;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using Xunit;

namespace AW.UI.Web.Admin.Mvc.UnitTests.Pages.HumanResources.Shift;

public class DetailModelUnitTests
{
    public class Get
    {
        [Theory, AutoMoqData]
        public async Task get_shift(
            [Frozen] Mock<IShiftService> shiftService,
            [Greedy] DetailModel sut,
            ShiftViewModel shift
        )
        {
            //Arrange
            shiftService.Setup(_ => _.GetDetail(shift.ObjectId!))
                .ReturnsAsync(shift);

            //Act
            await sut.OnGetAsync(shift.ObjectId!);

            //Assert
            sut.Shift.Should().BeEquivalentTo(shift);

            shiftService.Verify(_ => _.GetDetail(shift.ObjectId!));
        }
    }

    public class Post
    {
        [Theory, AutoMoqData]
        public async Task return_page_given_shift_is_updated(
            [Frozen] Mock<IShiftService> shiftService,
            [Greedy] DetailModel sut,
            ShiftViewModel shift
        )
        {
            //Arrange
            sut.Shift = shift;

            //Act
            var actual = await sut.OnPostAsync();

            //Assert
            actual.Should().BeOfType<PageResult>();

            shiftService.Verify(_ => _.UpdateShift(shift));
        }

        [Theory, AutoMoqData]
        public async Task return_page_given_modelstate_is_invalid(
            [Frozen] Mock<IShiftService> shiftService,
            [Greedy] DetailModel sut,
            ShiftViewModel shift,
            string key,
            string errorMessage
        )
        {
            //Arrange
            sut.ModelState.AddModelError(key, errorMessage);

            //Act
            var actual = await sut.OnPostAsync();

            //Assert
            actual.Should().BeOfType<PageResult>();

            shiftService.Verify(_ => _.UpdateShift(shift), Times.Never);
        }
    }

    public class Delete
    {
        [Theory, AutoMoqData]
        public async Task return_page_given_shift_is_deleted(
            [Frozen] Mock<IShiftService> shiftService,
            [Greedy] DetailModel sut,
            ShiftViewModel shift
        )
        {
            //Arrange

            //Act
            var actual = await sut.OnGetDeleteAsync(shift.ObjectId!);

            //Assert
            var redirectToPageResult = actual.Should().BeOfType<RedirectToPageResult>().Subject;
            redirectToPageResult.PageName.Should().Be("Index");
            shiftService.Verify(_ => _.DeleteShift(shift.ObjectId!));
        }
    }
}
