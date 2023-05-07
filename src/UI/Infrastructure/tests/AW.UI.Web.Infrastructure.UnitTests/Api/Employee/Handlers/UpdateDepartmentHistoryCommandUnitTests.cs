using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.Employee.Handlers.UpdateDepartmentHistory;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using Moq;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.Employee.Handlers
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
