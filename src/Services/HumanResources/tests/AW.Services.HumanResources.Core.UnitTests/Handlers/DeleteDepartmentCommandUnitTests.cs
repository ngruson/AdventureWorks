using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.Exceptions;
using AW.Services.HumanResources.Core.Handlers.DeleteDepartment;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;

namespace AW.Services.HumanResources.Core.UnitTests.Handlers
{
    public class DeleteDepartmentCommandUnitTests
    {
        [Theory, AutoMoqData]
        public async Task delete_department_given_department_exists(
            [Frozen] Mock<IRepository<Entities.Department>> departmentRepoMock,
            DeleteDepartmentCommandHandler sut,
            DeleteDepartmentCommand command,
            Entities.Department department
        )
        {
            //Arrange
            departmentRepoMock.Setup(_ => _.SingleOrDefaultAsync(
                    It.IsAny<GetDepartmentSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(department);

            //Act
            await sut.Handle(command, CancellationToken.None);

            //Assert
            departmentRepoMock.Verify(x => x.SingleOrDefaultAsync(
                    It.IsAny<GetDepartmentSpecification>(),
                    It.IsAny<CancellationToken>()
                ) 
            );

            departmentRepoMock.Verify(x => x.DeleteAsync(
                department,
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory, AutoMoqData]
        public async Task throw_departmentnotfoundexception_given_department_does_not_exist(
            [Frozen] Mock<IRepository<Entities.Department>> departmentRepoMock,
            DeleteDepartmentCommandHandler sut,
            DeleteDepartmentCommand command,
            Entities.Department department
        )
        {
            //Arrange

            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<DepartmentNotFoundException>();

            departmentRepoMock.Verify(x => x.SingleOrDefaultAsync(
                    It.IsAny<GetDepartmentSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            );

            departmentRepoMock.Verify(x => x.DeleteAsync(
                    department,
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }
    }
}
