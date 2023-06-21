using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Admin.Mvc.Pages.HumanResources.Department;
using AW.UI.Web.Admin.Mvc.Services;
using AW.UI.Web.Admin.Mvc.ViewModels.Department;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.UI.Web.Admin.Mvc.UnitTests.Pages.HumanResources.Department;

public class IndexModelUnitTests
{
    public class Get
    {
        [Theory, AutoMoqData]
        public async Task get_departments(
            [Frozen] Mock<IDepartmentService> departmentService,
            [Greedy] IndexModel sut,
            List<DepartmentViewModel> departments
        )
        {
            //Arrange
            departmentService.Setup(_ => _.GetDepartments())
                .ReturnsAsync(departments);

            //Act
            await sut.OnGetAsync();

            //Assert
            sut.Departments.Should().BeEquivalentTo(departments);

            departmentService.Verify(_ => _.GetDepartments());
        }
    }
}
