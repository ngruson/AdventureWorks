using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.AutoMapper;
using AW.Services.HumanResources.Core.Handlers.CreateDepartment;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;

namespace AW.Services.HumanResources.Core.UnitTests.Handlers
{
    public class CreateDepartmentCommandUnitTests
    {
        [Theory]
        [AutoMapperData(typeof(MappingProfile))]
        public async Task return_department_given_department_was_created(
            [Frozen] Mock<IRepository<Entities.Department>> departmentRepoMock,
            CreateDepartmentCommandHandler sut,
            CreateDepartmentCommand command,
            Entities.Department department
        )
        {
            //Arrange
            departmentRepoMock.Setup(_ => _.AddAsync(
                    It.IsAny<Entities.Department>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(department);

            //Act
            var actual = await sut.Handle(command, CancellationToken.None);

            //Assert
            actual.Should().BeEquivalentTo(department, opt => opt
                .Excluding(_ => _.Id)
            );

            departmentRepoMock.Verify(x => x.AddAsync(
                It.IsAny<Entities.Department>(),
                It.IsAny<CancellationToken>()
            ));
        }
    }
}
