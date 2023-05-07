using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.Employee.Handlers.UpdateEmployee;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.Employee.Handlers
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
                It.IsAny<Infrastructure.Api.Employee.Handlers.UpdateEmployee.Employee>()
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
                    It.IsAny<Infrastructure.Api.Employee.Handlers.UpdateEmployee.Employee>()
                )
                , Times.Never
            );
        }
    }
}
