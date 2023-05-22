using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.Employee.Handlers.UpdateEmployee;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using Moq;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.Employee.Handlers
{
    public class UpdateEmployeeCommandUnitTests
    {
        public class UpdateDepartmentCommandUnitTests
        {
            [Theory, AutoMoqData]
            public async Task update_department(
                [Frozen] Mock<IEmployeeApiClient> employeeApiClient,
                UpdateEmployeeCommandHandler sut,
                UpdateEmployeeCommand command
            )
            {
                //Arrange

                //Act
                await sut.Handle(command, CancellationToken.None);

                //Assert
                employeeApiClient.Verify(_ => _.UpdateEmployee(
                    It.IsAny<Infrastructure.Api.Employee.Handlers.UpdateEmployee.Employee>()
                ));
            }
        }
    }
}
