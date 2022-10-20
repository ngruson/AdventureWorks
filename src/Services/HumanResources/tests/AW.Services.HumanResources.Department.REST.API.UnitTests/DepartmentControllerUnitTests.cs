using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.Exceptions;
using AW.Services.HumanResources.Core.Handlers.GetDepartment;
using AW.Services.HumanResources.Core.Handlers.GetDepartments;
using AW.Services.HumanResources.Department.REST.API.Controllers;
using AW.SharedKernel.UnitTesting;
using AW.SharedKernel.ValueTypes;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AW.Services.HumanResources.Department.REST.API.UnitTests
{
    public class DepartmentControllerUnitTests
    {
        public class GetDepartments
        {
            [Theory, AutoMoqData]
            public async Task GetDepartments_DepartmentsExists_ShouldReturnDepartments(

                [Frozen] Mock<IMediator> mockMediator,
                List<Core.Handlers.GetDepartments.Department> departments,
                [Greedy] DepartmentController sut,
                GetDepartmentsQuery query
            )
            {
                //Arrange
                mockMediator.Setup(x => x.Send(
                        It.IsAny<GetDepartmentsQuery>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(departments);

                //Act
                var actionResult = await sut.GetDepartments(query);

                //Assert
                var okObjectResult = actionResult as OkObjectResult;
                okObjectResult.Should().NotBeNull();

                var response = okObjectResult?.Value as List<Core.Handlers.GetDepartments.Department>;
                response?.Should().BeEquivalentTo(departments);
            }

            [Theory]
            [AutoMoqData]
            public async Task GetDepartments_NoDepartments_ShouldReturnNotFound(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] DepartmentController sut,
                GetDepartmentsQuery query
            )
            {
                //Arrange                
                mockMediator.Setup(x => x.Send(
                    It.IsAny<GetDepartmentsQuery>(),
                    It.IsAny<CancellationToken>()
                ))
                .Throws<DepartmentsNotFoundException>();

                //Act
                var actionResult = await sut.GetDepartments(query);

                //Assert
                var notFoundResult = actionResult as NotFoundResult;
                notFoundResult.Should().NotBeNull();
            }
        }

        public class GetDepartment
        {
            [Theory]
            [AutoMoqData]
            public async Task GetDepartment_DepartmentExists_ShouldReturnDepartment(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] DepartmentController sut,
                GetDepartmentQuery query,
                Core.Handlers.GetDepartment.Department department
            )
            {
                //Arrange
                mockMediator.Setup(x => x.Send(
                    It.IsAny<GetDepartmentQuery>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(department);

                //Act
                var actionResult = await sut.GetDepartment(query);

                //Assert
                var okObjectResult = actionResult as OkObjectResult;
                okObjectResult.Should().NotBeNull();

                var result = okObjectResult?.Value as Core.Handlers.GetDepartment.Department;
                result.Should().NotBeNull();
            }

            [Theory]
            [AutoMoqData]
            public async Task GetDepartment_DepartmentDoesNotExists_ShouldReturnNotFound(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] DepartmentController sut,
                GetDepartmentQuery query
            )
            {
                //Arrange
                mockMediator.Setup(x => x.Send(
                    It.IsAny<GetDepartmentQuery>(),
                    It.IsAny<CancellationToken>()
                ))
                .ThrowsAsync(new DepartmentNotFoundException(query.Name));

                //Act
                var actionResult = await sut.GetDepartment(query);

                //Assert
                var notFoundResult = actionResult as NotFoundResult;
                notFoundResult.Should().NotBeNull();
            }
        }
    }
}