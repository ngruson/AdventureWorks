using AutoFixture.Xunit2;
using AW.UI.Web.SharedKernel.Department.Handlers.CreateDepartment;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using Moq;

namespace AW.UI.Web.SharedKernel.UnitTests.Department.Handlers
{
    public class CreateDepartmentCommandUnitTests
    {
        [Theory, AutoMoqData]
        public async Task create_department_given_command_is_valid(
            [Frozen] Mock<IDepartmentApiClient> mockDepartmentApiClient,
            CreateDepartmentCommandHandler sut,
            CreateDepartmentCommand command
        )
        {
            //Arrange

            //Act
            await sut.Handle(command, CancellationToken.None);

            //Assert
            mockDepartmentApiClient.Verify(_ => _.CreateDepartment(
                command.Department
            ));
        }
    }
}
