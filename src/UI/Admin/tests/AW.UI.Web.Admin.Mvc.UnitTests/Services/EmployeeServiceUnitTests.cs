using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Admin.Mvc.Services;
using AW.UI.Web.SharedKernel.Employee.Handlers.GetEmployees;
using FluentAssertions;
using MediatR;
using Moq;
using Xunit;

namespace AW.UI.Web.Admin.Mvc.UnitTests.Services
{
    public class EmployeeServiceUnitTests
    {
        public class GetEmployees
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task ReturnsViewModel(
                [Frozen] Mock<IMediator> mockMediator,
                EmployeeService sut,
                List<Employee> employees
            )
            {
                //Arrange
                mockMediator.Setup(_ => _.Send(
                        It.IsAny<GetEmployeesQuery>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(employees);

                //Act
                var viewModel = await sut.GetEmployees();

                //Assert
                viewModel.Should().BeEquivalentTo(employees);
            }
        }
    }
}
