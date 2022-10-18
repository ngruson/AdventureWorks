using Ardalis.Specification;
using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.AutoMapper;
using AW.Services.HumanResources.Core.Exceptions;
using AW.Services.HumanResources.Core.Handlers.GetEmployees;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using AW.SharedKernel.ValueTypes;
using FluentAssertions;
using Moq;

namespace AW.Services.HumanResources.Core.UnitTests.Handlers
{
    public class GetEmployeesQueryUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_EmployeesExists_ReturnEmployees(
            [Frozen] Mock<IRepository<Entities.Employee>> employeeRepoMock,
            GetEmployeesQueryHandler sut,
            List<NameFactory> names,
            GetEmployeesQuery query
        )
        {
            // Arrange

            var employees = names.Select(_ =>
            {
                var emp = new Entities.Employee();
                emp.SetName(_);
                return emp;
            }
                
            ).ToList();

            employeeRepoMock.Setup(x => x.ListAsync(
                It.IsAny<GetEmployeesPaginatedSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(employees);

            employeeRepoMock.Setup(x => x.CountAsync(
                It.IsAny<CountEmployeesSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(employees.Count);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert            
            result.Employees.Should().BeEquivalentTo(employees, opt => opt
                .Excluding(_ => _.Id)
                .Excluding(_ => _.Title)
                .Excluding(_ => _.Suffix)
                .Excluding(_ => _.Gender)
                .Excluding(_ => _.MaritalStatus)
            );
            result.TotalEmployees.Should().Be(employees.Count);
            employeeRepoMock.Verify(x => x.ListAsync(
                It.IsAny<ISpecification<Entities.Employee>>(),
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory]
        [AutoMoqData]
        public async Task Handle_NoEmployeesExists_ThrowArgumentNullException(
            [Frozen] Mock<IRepository<Entities.Employee>> employeeRepoMock,
            GetEmployeesQueryHandler sut,
            GetEmployeesQuery query
        )
        {
            // Arrange            
            employeeRepoMock.Setup(x => x.ListAsync(
                It.IsAny<GetEmployeesPaginatedSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((List<Entities.Employee>?)null);

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<EmployeesNotFoundException>()
                .WithMessage("Employees not found");
        }
    }
}