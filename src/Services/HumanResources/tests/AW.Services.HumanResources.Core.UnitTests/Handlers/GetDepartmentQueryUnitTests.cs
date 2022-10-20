using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.AutoMapper;
using AW.Services.HumanResources.Core.Exceptions;
using AW.Services.HumanResources.Core.Handlers.GetDepartment;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;

namespace AW.Services.HumanResources.Core.UnitTests.Handlers
{
    public class GetDepartmentQueryUnitTests
    {
        [Theory]
        [AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_DepartmentExists_ReturnDepartment(
            [Frozen] Mock<IRepository<Entities.Department>> departmentRepoMock,
            GetDepartmentQueryHandler sut,
            GetDepartmentQuery query,
            Entities.Department department
        )
        {
            //Arrange
            departmentRepoMock.Setup(_ => _.SingleOrDefaultAsync(
                It.IsAny<GetDepartmentSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(department);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            departmentRepoMock.Verify(x => x.SingleOrDefaultAsync(
                It.IsAny<GetDepartmentSpecification>(),
                It.IsAny<CancellationToken>()
            ));

            result.Should().BeEquivalentTo(department, opt => opt
                .Excluding(_ => _.Path.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase))
            );
        }

        [Theory]
        [AutoMoqData]
        public async Task Handle_DepartmentNotFound_ThrowArgumentNullException(
            [Frozen] Mock<IRepository<Entities.Department>> departmentRepoMock,
            GetDepartmentQueryHandler sut,
            GetDepartmentQuery query
        )
        {
            // Arrange
            departmentRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetDepartmentSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.Department?)null);

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<DepartmentNotFoundException>()
                .WithMessage($"Department '{query.Name}' not found");
        }
    }
}