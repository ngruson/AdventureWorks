using AutoFixture.Xunit2;
using AW.UI.Web.SharedKernel.Employee.Handlers.UpdateDepartmentHistory;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using Moq;

namespace AW.UI.Web.SharedKernel.UnitTests.Employee.Handlers
{
    public class UpdateDepartmentHistoryCommandUnitTests
    {
        [Theory, AutoMoqData]
        public async Task UpdateProductGivenCommandIsValid(
            [Frozen] Mock<IEmployeeApiClient> mockEmployeeApiClient,
            UpdateDepartmentHistoryCommandHandler sut,
            UpdateDepartmentHistoryCommand command
        )
        {
            //Arrange

            //Act
            await sut.Handle(command, CancellationToken.None);

            //Assert
            mockEmployeeApiClient.Verify(_ => _.UpdateDepartmentHistory(command));
        }
    }
}
