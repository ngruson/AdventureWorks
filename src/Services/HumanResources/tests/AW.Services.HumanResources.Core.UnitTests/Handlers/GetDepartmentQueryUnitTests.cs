using Ardalis.Result;
using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.AutoMapper;
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
        public async Task return_success_given_department_exists(
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
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(department, opt => opt
                .Excluding(_ => _.Path.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase))
            );

            departmentRepoMock.Verify(x => x.SingleOrDefaultAsync(
                It.IsAny<GetDepartmentSpecification>(),
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory]
        [AutoMoqData]
        public async Task return_notfound_given_department_does_not_exist(
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
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Status.Should().Be(ResultStatus.NotFound);
        }
    }
}
