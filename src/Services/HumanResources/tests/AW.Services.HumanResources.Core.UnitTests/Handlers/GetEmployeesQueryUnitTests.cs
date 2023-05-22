using Ardalis.Result;
using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.AutoMapper;
using AW.Services.HumanResources.Core.Handlers.GetEmployees;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;

namespace AW.Services.HumanResources.Core.UnitTests.Handlers
{
    public class GetEmployeesQueryUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task return_success_given_employees_exist(
            [Frozen] Mock<IRepository<Entities.Employee>> employeeRepo,
            GetEmployeesQueryHandler sut,
            GetEmployeesQuery query,
            List<Entities.Employee> employees
        )
        {
            // Arrange
            employeeRepo.Setup(x => x.ListAsync(
                It.IsAny<GetEmployeesSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(employees);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(employees, opt => opt
                .Excluding(_ => _.Id)
            );

            employeeRepo.Verify(x => x.ListAsync(
                It.IsAny<GetEmployeesSpecification>(),
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory]
        [AutoMoqData]
        public async Task return_notfound_given_employees_do_not_exist(
            [Frozen] Mock<IRepository<Entities.Employee>> employeeRepo,
            GetEmployeesQueryHandler sut,
            GetEmployeesQuery query
        )
        {
            // Arrange            
            #pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            employeeRepo.Setup(x => x.ListAsync(
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((List<Entities.Employee>?)null);
            #pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Status.Should().Be(ResultStatus.NotFound);
        }

        [Theory]
        [AutoMoqData]
        public async Task return_error_given_exception_was_thrown(
            [Frozen] Mock<IRepository<Entities.Employee>> employeeRepo,
            GetEmployeesQueryHandler sut,
            GetEmployeesQuery query
        )
        {
            // Arrange
            employeeRepo.Setup(x => x.ListAsync(
                It.IsAny<GetEmployeesSpecification>(),
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
