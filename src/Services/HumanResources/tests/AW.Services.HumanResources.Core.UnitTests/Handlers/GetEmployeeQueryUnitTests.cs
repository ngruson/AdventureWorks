using Ardalis.Result;
using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.AutoMapper;
using AW.Services.HumanResources.Core.Handlers.GetEmployee;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;

namespace AW.Services.HumanResources.Core.UnitTests.Handlers
{
    public class GetEmployeeQueryUnitTests
    {
        [Theory]
        [AutoMapperData(typeof(MappingProfile))]
        public async Task return_success_given_employee_exists(
            [Frozen] Mock<IRepository<Entities.Employee>> employeeRepo,
            GetEmployeeQueryHandler sut,
            GetEmployeeQuery query,
            Entities.Employee employee
        )
        {
            //Arrange
            employeeRepo.Setup(_ => _.SingleOrDefaultAsync(
                It.IsAny<GetEmployeeSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(employee);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(employee, opt => opt
                .Excluding(_ => _.Path.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase))
            );

            employeeRepo.Verify(x => x.SingleOrDefaultAsync(
                It.IsAny<GetEmployeeSpecification>(),
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory]
        [AutoMoqData]
        public async Task return_notfound_given_employee_does_not_exist(
            [Frozen] Mock<IRepository<Entities.Employee>> employeeRepo,
            GetEmployeeQueryHandler sut,
            GetEmployeeQuery query
        )
        {
            // Arrange
            employeeRepo.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetEmployeeSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.Employee?)null);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Status.Should().Be(ResultStatus.NotFound);
        }

        [Theory]
        [AutoMoqData]
        public async Task return_error_given_exception_was_thrown(
            [Frozen] Mock<IRepository<Entities.Employee>> employeeRepo,
            GetEmployeeQueryHandler sut,
            GetEmployeeQuery query
        )
        {
            // Arrange
            employeeRepo.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetEmployeeSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ThrowsAsync(new Exception());

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Status.Should().Be(ResultStatus.Error);
        }
    }
}
