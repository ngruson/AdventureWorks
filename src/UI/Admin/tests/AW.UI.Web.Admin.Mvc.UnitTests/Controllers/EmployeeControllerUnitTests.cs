using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Admin.Mvc.Controllers;
using AW.UI.Web.Admin.Mvc.Services;
using AW.UI.Web.Admin.Mvc.ViewModels.Employee;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Moq;
using Xunit;

namespace AW.UI.Web.Admin.Mvc.UnitTests.Controllers
{
    public class EmployeeControllerUnitTests
    {
        public class Index
        {
            [Theory, AutoMoqData]
            public async Task Index_ReturnsViewModel(
                [Frozen] Mock<IEmployeeService> employeeService,
                List<EmployeeViewModel> viewModel,
                [Greedy] EmployeeController sut
            )
            {
                //Arrange
                employeeService.Setup(x => x.GetEmployees())
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
                [Frozen] Mock<IEmployeeService> employeeService,
                [Greedy] EmployeeController sut,
                EmployeeDetailViewModel viewModel,
                string loginID
            )
            {
                //Arrange
                employeeService.Setup(_ => _.GetDetail(loginID)) 
                    .ReturnsAsync(viewModel);

                //Act
                var actionResult = await sut.Detail(loginID);

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().Be(viewModel);

                employeeService.Verify(_ => _.GetDetail(loginID));
                employeeService.Verify(_ => _.GetDepartments());
                employeeService.Verify(_ => _.GetJobTitles());
                employeeService.Verify(_ => _.GetShifts());
            }
        }

        public class UpdateEmployee
        {
            [Theory, AutoMoqData]
            public async Task ReturnsViewModel(
                [Frozen] Mock<IEmployeeService> employeeService,
                [Greedy] EmployeeController sut,
                EditEmployeeViewModel viewModel
            )
            {
                //Arrange

                //Act
                var actionResult = await sut.UpdateEmployee(viewModel);

                //Assert
                var redirectResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                redirectResult.ActionName.Should().Be(nameof(EmployeeController.Detail));
                redirectResult.RouteValues.Should().BeEquivalentTo(
                    new RouteValueDictionary
                    {
                        { "loginID", viewModel.Employee!.LoginID }
                    }
                );

                employeeService.Verify(_ => _.UpdateEmployee(viewModel));
            }
        }

        public class AddDepartmentHistory
        {
            [Theory, AutoMoqData]
            public async Task ReturnsViewModel(
                [Frozen] Mock<IEmployeeService> employeeService,
                [Greedy] EmployeeController sut,
                AddDepartmentHistoryViewModel viewModel
            )
            {
                //Arrange

                //Act
                var actionResult = await sut.AddDepartmentHistory(viewModel);

                //Assert
                var redirectResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                redirectResult.ActionName.Should().Be(nameof(EmployeeController.Detail));
                redirectResult.RouteValues.Should().BeEquivalentTo(
                    new RouteValueDictionary
                    {
                        { "loginID", viewModel.LoginID }
                    }
                );

                employeeService.Verify(_ => _.AddDepartmentHistory(viewModel));
            }
        }

        public class UpdateDepartmentHistory
        {
            [Theory, AutoMoqData]
            public async Task ReturnsViewModel(
                [Frozen] Mock<IEmployeeService> employeeService,
                [Greedy] EmployeeController sut,
                UpdateDepartmentHistoryViewModel viewModel
            )
            {
                //Arrange

                //Act
                var actionResult = await sut.UpdateDepartmentHistory(viewModel);

                //Assert
                var redirectResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                redirectResult.ActionName.Should().Be(nameof(EmployeeController.Detail));
                redirectResult.RouteValues.Should().BeEquivalentTo(
                    new RouteValueDictionary
                    {
                        { "loginID", viewModel.LoginID }
                    }
                );

                employeeService.Verify(_ => _.UpdateDepartmentHistory(viewModel));
            }
        }

        public class DeleteDepartmentHistory
        {
            [Theory, AutoMoqData]
            public async Task ReturnsViewModel(
                [Frozen] Mock<IEmployeeService> employeeService,
                [Greedy] EmployeeController sut,
                string loginID,
                string departmentName,
                string shiftName,
                DateTime startDate
            )
            {
                //Arrange
                
                //Act
                var actionResult = await sut.DeleteDepartmentHistory(
                    loginID,
                    departmentName,
                    shiftName,
                    startDate
                );

                //Assert
                var redirectResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                redirectResult.ActionName.Should().Be(nameof(EmployeeController.Detail));
                redirectResult.RouteValues.Should().BeEquivalentTo(
                    new RouteValueDictionary
                    {
                        { "loginID", loginID }
                    }
                );

                employeeService.Verify(_ => _.DeleteDepartmentHistory(
                    loginID,
                    departmentName,
                    shiftName,
                    startDate
                ));
            }
        }
    }
}
