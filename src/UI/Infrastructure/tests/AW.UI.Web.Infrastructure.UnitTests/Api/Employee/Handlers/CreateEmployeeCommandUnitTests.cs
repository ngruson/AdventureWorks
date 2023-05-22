using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.Employee.Handlers.CreateEmployee;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using Moq;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.Employee.Handlers
{
    public class CreateEmployeeCommandUnitTests
    {
        [Theory, AutoMoqData]
        public async Task create_employee_given_command_is_valid(
            [Frozen] Mock<IEmployeeApiClient> employeeApiClient,
            CreateEmployeeCommandHandler sut,
            CreateEmployeeCommand command
        )
        {
            //Arrange

            //Act
            await sut.Handle(command, CancellationToken.None);

            //Assert
            employeeApiClient.Verify(_ => _.CreateEmployee(
                command.Employee
            ));
        }
    }
}
