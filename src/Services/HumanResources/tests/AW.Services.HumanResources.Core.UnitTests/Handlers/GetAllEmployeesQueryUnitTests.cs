using Ardalis.Specification;
using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.AutoMapper;
using AW.Services.HumanResources.Core.Handlers.GetAllEmployees;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using AW.SharedKernel.ValueTypes;
using FluentAssertions;
using Moq;

namespace AW.Services.HumanResources.Core.UnitTests.Handlers
{
    public class GetAllEmployeesQueryUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_EmployeesExists_ReturnEmployees(
            [Frozen] Mock<IRepository<Entities.Employee>> employeeRepoMock,
            GetAllEmployeesQueryHandler sut,
            List<NameFactory> names,
            GetAllEmployeesQuery query
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
                It.IsAny<GetAllEmployeesSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(employees);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert            
            result.Should().BeEquivalentTo(employees, opt => opt
                .Excluding(_ => _.Id)
                .Excluding(_ => _.Title)
                .Excluding(_ => _.Suffix)
                .Excluding(_ => _.Gender)
                .Excluding(_ => _.MaritalStatus)
            );
            employeeRepoMock.Verify(x => x.ListAsync(
                It.IsAny<ISpecification<Entities.Employee>>(),
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory]
        [AutoMoqData]
        public async Task Handle_NoEmployeesExists_ThrowArgumentNullException(
            [Frozen] Mock<IRepository<Entities.Employee>> employeeRepoMock,
            GetAllEmployeesQueryHandler sut,
            GetAllEmployeesQuery query
        )
        {
            // Arrange            
            employeeRepoMock.Setup(x => x.ListAsync(
                It.IsAny<GetAllEmployeesSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((List<Entities.Employee>?)null);

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'employees')");
        }
    }
}