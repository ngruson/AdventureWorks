using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.Department.Handlers.UpdateDepartment;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using Moq;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.Department.Handlers
{
    public class UpdateDepartmentCommandUnitTests
    {
        [Theory, AutoMoqData]
        public async Task update_department(
            [Frozen] Mock<IDepartmentApiClient> mockDepartmentApiClient,
            UpdateDepartmentCommandHandler sut,
            UpdateDepartmentCommand command
        )
        {
            //Arrange

            //Act
            await sut.Handle(command, CancellationToken.None);

            //Assert
            mockDepartmentApiClient.Verify(_ => _.UpdateDepartment(
                It.IsAny<AW.UI.Web.Infrastructure.Api.Department.Handlers.UpdateDepartment.Department>()
            ));
        }
    }
}
