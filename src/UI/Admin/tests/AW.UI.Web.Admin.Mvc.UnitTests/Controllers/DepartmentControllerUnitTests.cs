using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Admin.Mvc.Controllers;
using AW.UI.Web.Admin.Mvc.Services;
using AW.UI.Web.Admin.Mvc.ViewModels.Department;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Moq;
using Xunit;

namespace AW.UI.Web.Admin.Mvc.UnitTests.Controllers
{
    public class DepartmentControllerUnitTests
    {
        public class Index
        {
            [Theory, AutoMoqData]
            public async Task Index_ReturnsViewModel(
                [Frozen] Mock<IDepartmentService> departmentService,
                List<DepartmentViewModel> viewModel,
                [Greedy] DepartmentController sut
            )
            {
                //Arrange
                departmentService.Setup(x => x.GetDepartments())
                    .ReturnsAsync(viewModel);

                //Act
                var actionResult = await sut.Index();

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().Be(viewModel);
            }
        }

        public class Detail
        {
            [Theory, AutoMoqData]
            public async Task ReturnsViewModel(
                [Frozen] Mock<IDepartmentService> departmentService,
                [Greedy] DepartmentController sut,
                DepartmentDetailViewModel viewModel,
                string name
            )
            {
                //Arrange
                departmentService.Setup(_ => _.GetDetail(name))
                    .ReturnsAsync(viewModel);

                //Act
                var actionResult = await sut.Detail(name);

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().Be(viewModel);

                departmentService.Verify(_ => _.GetDetail(name));
            }
        }

        public class UpdateDepartment
        {
            [Theory, AutoMoqData]
            public async Task ReturnsViewModel(
                [Frozen] Mock<IDepartmentService> departmentService,
                [Greedy] DepartmentController sut,
                EditDepartmentViewModel viewModel
            )
            {
                //Arrange

                //Act
                var actionResult = await sut.UpdateDepartment(viewModel);

                //Assert
                var redirectResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                redirectResult.ActionName.Should().Be(nameof(DepartmentController.Detail));
                redirectResult.RouteValues.Should().BeEquivalentTo(
                    new RouteValueDictionary
                    {
                        { "name", viewModel.Department!.Name }
                    }
                );

                departmentService.Verify(_ => _.UpdateDepartment(viewModel));
            }
        }

        public class DeleteDepartments
        {
            [Theory, AutoMoqData]
            public async Task delete_departments_given_department_names(
                string[] departments,
                [Greedy] DepartmentController sut
            )
            {
                //Arrange

                //Act
                var actionResult = await sut.DeleteDepartments(departments);

                //Assert
                actionResult.Should().BeOfType<OkResult>();
            }
        }

        public class DeleteDepartment
        {
            [Theory, AutoMoqData]
            public async Task delete_department_given_department_exists(
                string name,
                [Greedy] DepartmentController sut
            )
            {
                //Arrange

                //Act
                var actionResult = await sut.DeleteDepartment(name);

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                viewResult.ActionName.Should().Be(nameof(DepartmentController.Index));
            }
        }
    }
}
