using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.AutoMapper;
using AW.Services.HumanResources.Core.Exceptions;
using AW.Services.HumanResources.Core.Handlers.CreateDepartment;
using AW.Services.HumanResources.Core.Handlers.DeleteDepartment;
using AW.Services.HumanResources.Core.Handlers.GetDepartment;
using AW.Services.HumanResources.Core.Handlers.GetDepartments;
using AW.Services.HumanResources.Core.Handlers.UpdateDepartment;
using AW.Services.HumanResources.Department.REST.API.Controllers;
using AW.Services.Infrastructure.ActionResults;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
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

        public class CreateDepartment
        {
            [Theory, AutoMoqData]
            public async Task return_ok_given_command_is_valid(
                [Frozen] Mock<IMediator> mockMediator,
                [Frozen] Mock<IValidator<CreateDepartmentCommand>> validator,
                [Greedy] DepartmentController sut,
                Core.Handlers.CreateDepartment.Department department
            )
            {
                //Arrange
                validator.Setup(_ => _.ValidateAsync(
                        It.IsAny<CreateDepartmentCommand>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(new ValidationResult());

                mockMediator.Setup(x => x.Send(
                    It.IsAny<CreateDepartmentCommand>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(department);

                //Act
                var actionResult = await sut.CreateDepartment(department);

                //Assert
                var okObjectResult = actionResult as OkObjectResult;
                okObjectResult.Should().NotBeNull();

                var response = okObjectResult?.Value as Core.Handlers.CreateDepartment.Department;
                response?.Should().Be(department);
            }

            [Theory, AutoMoqData]
            public async Task return_badrequest_given_command_is_invalid(
                [Frozen] Mock<IValidator<CreateDepartmentCommand>> validator,
                [Greedy] DepartmentController sut,
                Core.Handlers.CreateDepartment.Department department,
                List<ValidationFailure> validationFailures
            )
            {
                //Arrange
                validator.Setup(_ => _.ValidateAsync(
                        It.IsAny<CreateDepartmentCommand>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(new ValidationResult(validationFailures));

                //Act
                var actionResult = await sut.CreateDepartment(department);

                //Assert
                var result = actionResult as BadRequestObjectResult;
                result!.Value.Should().BeOfType<ProblemHttpResult>();
            }

            [Theory, AutoMoqData]
            public async Task return_internalservererror_given_exception_occurs(
                [Frozen] Mock<IValidator<CreateDepartmentCommand>> validator,
                [Greedy] DepartmentController sut,
                Core.Handlers.CreateDepartment.Department department
            )
            {
                //Arrange
                validator.Setup(_ => _.ValidateAsync(
                        It.IsAny<CreateDepartmentCommand>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ThrowsAsync(new Exception());

                //Act
                var actionResult = await sut.CreateDepartment(department);

                //Assert
                actionResult.Should().BeOfType<InternalServerErrorObjectResult>();
            }
        }

        public class UpdateDepartment
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task ReturnDepartmentWhenDepartmentExists(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] DepartmentController sut,
                UpdateDepartmentCommand command,
                Core.Handlers.UpdateDepartment.Department department
            )
            {
                //Arrange
                mockMediator.Setup(x => x.Send(
                    It.IsAny<UpdateDepartmentCommand>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(department);

                //Act
                var actionResult = await sut.UpdateDepartment(command);

                //Assert
                var okObjectResult = actionResult as OkObjectResult;
                okObjectResult.Should().NotBeNull();

                var response = okObjectResult?.Value as Core.Handlers.UpdateDepartment.Department;
                response?.Should().Be(department);
            }

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task ReturnNotFoundWhenDepartmentDoesNotExist(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] DepartmentController sut,
                UpdateDepartmentCommand command
            )
            {
                //Arrange
                mockMediator.Setup(x => x.Send(
                        It.IsAny<UpdateDepartmentCommand>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ThrowsAsync(new DepartmentNotFoundException(command.Department!.Name));

                //Act
                var actionResult = await sut.UpdateDepartment(command);

                //Assert
                actionResult.Should().BeOfType<NotFoundResult>();
            }
        }

        public class DeleteDepartment
        {
            [Theory, AutoMoqData]
            public async Task return_ok_given_command_is_valid(
                [Frozen] Mock<IMediator> mockMediator,
                [Frozen] Mock<IValidator<DeleteDepartmentCommand>> validator,
                [Greedy] DepartmentController sut,
                DeleteDepartmentCommand command
            )
            {
                //Arrange
                validator.Setup(_ => _.ValidateAsync(
                        command,
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(new ValidationResult());

                //Act
                var actionResult = await sut.DeleteDepartment(command);

                //Assert
                actionResult.Should().BeOfType<OkResult>();

                mockMediator.Verify(_ => _.Send(
                        command,
                        It.IsAny<CancellationToken>()
                    )
                );
            }

            [Theory, AutoMoqData]
            public async Task return_badrequest_given_command_is_invalid(
                [Frozen] Mock<IValidator<DeleteDepartmentCommand>> validator,
                [Greedy] DepartmentController sut,
                DeleteDepartmentCommand command,
                List<ValidationFailure> validationFailures
            )
            {
                //Arrange
                validator.Setup(_ => _.ValidateAsync(
                        command,
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(new ValidationResult(validationFailures));

                //Act
                var actionResult = await sut.DeleteDepartment(command);

                //Assert
                var result = actionResult as BadRequestObjectResult;
                result!.Value.Should().BeOfType<ProblemHttpResult>();
            }

            [Theory, AutoMoqData]
            public async Task return_internalservererror_given_exception_occurs(
                [Frozen] Mock<IValidator<DeleteDepartmentCommand>> validator,
                [Greedy] DepartmentController sut,
                DeleteDepartmentCommand command
            )
            {
                //Arrange
                validator.Setup(_ => _.ValidateAsync(
                        command,
                        It.IsAny<CancellationToken>()
                    )
                )
                .ThrowsAsync(new Exception());

                //Act
                var actionResult = await sut.DeleteDepartment(command);

                //Assert
                actionResult.Should().BeOfType<InternalServerErrorObjectResult>();
            }
        }
    }
}
