using AutoFixture.Xunit2;
using AW.UI.Web.SharedKernel.Employee.Handlers.UpdateEmployee;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using FluentAssertions;
using Moq;

namespace AW.UI.Web.SharedKernel.UnitTests.Employee.Handlers
{
    public class UpdateEmployeeCommandUnitTests
    {
        [Theory, AutoMoqData]
        public async Task UpdateProductGivenCommandIsValid(
            [Frozen] Mock<IEmployeeApiClient> mockEmployeeApiClient,
            UpdateEmployeeCommandHandler sut,
            UpdateEmployeeCommand command
        )
        {
            //Arrange

            //Act
            await sut.Handle(command, CancellationToken.None);

            //Assert
            mockEmployeeApiClient.Verify(_ => _.UpdateEmployee(
                It.IsAny<string>(),
                It.IsAny<SharedKernel.Employee.Handlers.UpdateEmployee.Employee>()
            ));
        }

        [Theory, AutoMoqData]
        public async Task ThrowArgumentNullExceptionGivenCommandIsInvalid(
            [Frozen] Mock<IEmployeeApiClient> mockEmployeeApiClient,
            UpdateEmployeeCommandHandler sut
        )
        {
            //Arrange

            //Act
            Func<Task> func = async () => await sut.Handle(
                new UpdateEmployeeCommand(null!, null!), CancellationToken.None
            );

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>();

            mockEmployeeApiClient.Verify(_ => _.UpdateEmployee(
                    It.IsAny<string>(),
                    It.IsAny<SharedKernel.Employee.Handlers.UpdateEmployee.Employee>()
                )
                , Times.Never
            );
        }
    }
}
