using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.AutoMapper;
using AW.Services.HumanResources.Core.Exceptions;
using AW.Services.HumanResources.Core.Handlers.GetDepartments;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;

namespace AW.Services.HumanResources.Core.UnitTests.Handlers
{
    public class GetDepartmentsQueryUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_DepartmentsExists_ReturnDepartments(
            [Frozen] Mock<IRepository<Entities.Department>> departmentRepoMock,
            GetDepartmentsQueryHandler sut,
            GetDepartmentsQuery query,
            List<Entities.Department> departments
        )
        {
            // Arrange
            departmentRepoMock.Setup(x => x.ListAsync(
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(departments);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert            
            result.Should().BeEquivalentTo(departments, opt => opt
                .Excluding(_ => _.Id)
            );
            departmentRepoMock.Verify(x => x.ListAsync(
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory]
        [AutoMoqData]
        public async Task Handle_NoDepartmentsExists_ThrowArgumentNullException(
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
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<DepartmentsNotFoundException>()
                .WithMessage("No departments found");
        }
    }
}