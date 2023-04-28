using AutoFixture.Xunit2;
using AW.UI.Web.SharedKernel.Department.Handlers.UpdateDepartment;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using FluentAssertions;
using Moq;

namespace AW.UI.Web.SharedKernel.UnitTests.Department.Handlers
{
    public class UpdateDepartmentCommandUnitTests
    {
        [Theory, AutoMoqData]
        public async Task UpdateDepartmentGivenCommandIsValid(
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
                It.IsAny<UpdateDepartmentCommand>()
            ));
        }

        [Theory, AutoMoqData]
        public async Task ThrowArgumentNullExceptionGivenCommandIsInvalid(
            [Frozen] Mock<IDepartmentApiClient> mockDepartmentApiClient,
            UpdateDepartmentCommandHandler sut
        )
        {
            //Arrange

            //Act
            Func<Task> func = async () => await sut.Handle(
                new UpdateDepartmentCommand(null!, null!), CancellationToken.None
            );

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>();

            mockDepartmentApiClient.Verify(_ => _.UpdateDepartment(
                    It.IsAny<UpdateDepartmentCommand>()
                )
                , Times.Never
            );
        }
    }
}
