using Ardalis.Result;
using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.AutoMapper;
using AW.Services.HumanResources.Core.Handlers.GetDepartments;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;

namespace AW.Services.HumanResources.Core.UnitTests.Handlers
{
    public class GetDepartmentsQueryUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task return_success_given_departments_exist(
            [Frozen] Mock<IRepository<Entities.Department>> departmentRepoMock,
            GetDepartmentsQueryHandler sut,
            GetDepartmentsQuery query,
            List<Entities.Department> departments
        )
        {
            // Arrange
            departmentRepoMock.Setup(x => x.ListAsync(
                It.IsAny<GetDepartmentsSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(departments);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(departments, opt => opt
                .Excluding(_ => _.Id)
            );
            
            departmentRepoMock.Verify(x => x.ListAsync(
                It.IsAny<GetDepartmentsSpecification>(),
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory]
        [AutoMoqData]
        public async Task return_notfound_given_departments_do_not_exist(
            [Frozen] Mock<IRepository<Entities.Department>> departmentRepoMock,
            GetDepartmentsQueryHandler sut,
            GetDepartmentsQuery query
        )
        {
            // Arrange            
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            departmentRepoMock.Setup(x => x.ListAsync(
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((List<Entities.Department>?)null);
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Status.Should().Be(ResultStatus.NotFound);
        }
    }
}
