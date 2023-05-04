using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.AutoMapper;
using AW.Services.HumanResources.Core.Exceptions;
using AW.Services.HumanResources.Core.Handlers.UpdateDepartment;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;

namespace AW.Services.HumanResources.Core.UnitTests.Handlers
{
    public class UpdateDepartmentCommandUnitTests
    {
        [Theory]
        [AutoMapperData(typeof(MappingProfile))]
        public async Task ReturnUpdatedDepartmentGivenDepartmentExists(
            [Frozen] Mock<IRepository<Entities.Department>> departmentRepoMock,
            Entities.Department department,
            UpdateDepartmentCommandHandler sut,
            UpdateDepartmentCommand command
        )
        {
            //Arrange
            departmentRepoMock.Setup(_ => _.SingleOrDefaultAsync(
                    It.IsAny<GetDepartmentSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            ).
            ReturnsAsync(department);

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            departmentRepoMock.Verify(x => x.SingleOrDefaultAsync(
                It.IsAny<GetDepartmentSpecification>(),
                It.IsAny<CancellationToken>()
            ));
            departmentRepoMock.Verify(x => x.UpdateAsync(
                It.IsAny<Entities.Department>(),
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory]
        [AutoMoqData]
        public async Task ThrowDepartmentNotFoundExceptionGivenDepartmentDoesNotExist(
            [Frozen] Mock<IRepository<Entities.Department>> departmentRepoMock,
            UpdateDepartmentCommandHandler sut,
            UpdateDepartmentCommand command
        )
        {
            // Arrange
            departmentRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetDepartmentSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.Department?)null);

            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<DepartmentNotFoundException>()
                .WithMessage($"Department '{command.Key}' not found");
        }
    }
}
