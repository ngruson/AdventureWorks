using AutoFixture.Xunit2;
using AW.UI.Web.SharedKernel.Employee.Handlers.DeleteDepartmentHistory;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using Moq;

namespace AW.UI.Web.SharedKernel.UnitTests.Employee.Handlers
{
    public class DeleteDepartmentHistoryCommandUnitTests
    {
        [Theory, AutoMoqData]
        public async Task SuccessGivenCommandIsValid(
            [Frozen] Mock<IEmployeeApiClient> mockEmployeeApiClient,
            DeleteDepartmentHistoryCommandHandler sut,
            DeleteDepartmentHistoryCommand command
        )
        {
            //Arrange

            //Act
            await sut.Handle(command, CancellationToken.None);

            //Assert
            mockEmployeeApiClient.Verify(_ => _.DeleteDepartmentHistory(
                It.IsAny<DeleteDepartmentHistoryCommand>()
            ));
        }
    }
}
