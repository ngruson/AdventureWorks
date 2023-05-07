using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.Department.Handlers.DeleteDepartment;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.Department.Handlers
{
    public class DeleteDepartmentCommandUnitTests
    {
        [Theory, AutoMoqData]
        public async Task delete_department_given_name(
            [Frozen] Mock<IDepartmentApiClient> mockDepartmentApiClient,
            DeleteDepartmentCommandHandler sut,
            DeleteDepartmentCommand command
        )
        {
            //Arrange

            //Act
            await sut.Handle(command, CancellationToken.None);

            //Assert
            mockDepartmentApiClient.Verify(_ => _.DeleteDepartment(
                It.IsAny<DeleteDepartmentCommand>()
            ));
        }

        [Theory, AutoMoqData]
        public async Task throw_argumentnullexception_given_empty_name(
            [Frozen] Mock<IDepartmentApiClient> mockDepartmentApiClient,
            DeleteDepartmentCommandHandler sut
        )
        {
            //Arrange

            //Act
            Func<Task> func = async () => await sut.Handle(
                new DeleteDepartmentCommand(null!),
                CancellationToken.None
            );

            //Assert
            await func.Should().ThrowAsync<ArgumentException>();

            mockDepartmentApiClient.Verify(_ => _.DeleteDepartment(
                    It.IsAny<DeleteDepartmentCommand>()
                )
                , Times.Never
            );
        }
    }
}
