using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.Employee.Handlers.DeleteDepartmentHistory;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using Moq;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.Employee.Handlers
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
