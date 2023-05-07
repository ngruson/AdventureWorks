using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.Department.Handlers.CreateDepartment;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using Moq;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.Department.Handlers
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
