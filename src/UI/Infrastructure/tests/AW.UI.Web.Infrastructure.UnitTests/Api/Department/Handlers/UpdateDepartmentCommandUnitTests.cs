using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.Department.Handlers.UpdateDepartment;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.Department.Handlers
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
