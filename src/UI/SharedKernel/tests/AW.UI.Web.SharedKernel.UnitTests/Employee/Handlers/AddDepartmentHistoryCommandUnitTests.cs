using AutoFixture.Xunit2;
using AW.UI.Web.SharedKernel.Employee.Handlers.AddDepartmentHistory;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using Moq;

namespace AW.UI.Web.SharedKernel.UnitTests.Employee.Handlers
{
    public class AddDepartmentHistoryCommandUnitTests
    {
        [Theory, AutoMoqData]
        public async Task UpdateProductGivenCommandIsValid(
            [Frozen] Mock<IEmployeeApiClient> mockEmployeeApiClient,
            AddDepartmentHistoryCommandHandler sut,
            AddDepartmentHistoryCommand command
        )
        {
            //Arrange

            //Act
            await sut.Handle(command, CancellationToken.None);

            //Assert
            mockEmployeeApiClient.Verify(_ => _.AddDepartmentHistory(
                It.IsAny<AddDepartmentHistoryCommand>()
            ));
        }
    }
}
