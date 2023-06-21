using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Admin.Mvc.Services;
using AW.UI.Web.Admin.Mvc.ViewModels.Department;
using AW.UI.Web.Infrastructure.Api.Department.Handlers.DeleteDepartment;
using AW.UI.Web.Infrastructure.Api.Department.Handlers.GetDepartment;
using AW.UI.Web.Infrastructure.Api.Department.Handlers.GetDepartments;
using AW.UI.Web.Infrastructure.Api.Department.Handlers.UpdateDepartment;
using FluentAssertions;
using MediatR;
using Moq;
using Xunit;

namespace AW.UI.Web.Admin.Mvc.UnitTests.Services;

public class DepartmentServiceUnitTests
{
    public class GetDepartments
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task ReturnsViewModel(
            [Frozen] Mock<IMediator> mockMediator,
            DepartmentService sut,
            List<Infrastructure.Api.Department.Handlers.GetDepartments.Department> departments
        )
        {
            //Arrange
            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetDepartmentsQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(departments);

            //Act
            var viewModel = await sut.GetDepartments();

            //Assert
            viewModel.Should().BeEquivalentTo(departments);
        }
    }

    public class GetDetail
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task ReturnsDetailViewModel(
            [Frozen] Mock<IMediator> mockMediator,
            DepartmentService sut,
            Infrastructure.Api.Department.Handlers.GetDepartment.Department department,
            Guid objectId
        )
        {
            //Arrange
            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetDepartmentQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(department);

            //Act
            var viewModel = await sut.GetDetail(objectId);

            //Assert
            viewModel.Should().BeEquivalentTo(department);

            mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetDepartmentQuery>(),
                    It.IsAny<CancellationToken>()
                )
            );
        }
    }

    public class UpdateDepartment
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task UpdateDepartmentGivenDepartmentExists(
            [Frozen] Mock<IMediator> mockMediator,
            DepartmentService sut,
            DepartmentViewModel viewModel,
            Infrastructure.Api.Department.Handlers.GetDepartment.Department department
        )
        {
            //Arrange
            mockMediator.Setup(_ => _.Send(
                It.IsAny<GetDepartmentQuery>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(department);

            //Act
            await sut.UpdateDepartment(viewModel);

            //Assert
            mockMediator.Verify(_ => _.Send(
                It.IsAny<GetDepartmentQuery>(),
                It.IsAny<CancellationToken>()
            ));

            mockMediator.Verify(_ => _.Send(
                It.IsAny<UpdateDepartmentCommand>(),
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task ThrowArgumentNullExceptionGivenDepartmentDoesNotExist(
            [Frozen] Mock<IMediator> mockMediator,
            DepartmentService sut,
            DepartmentViewModel viewModel
        )
        {
            //Arrange
            mockMediator.Setup(_ => _.Send(
                It.IsAny<GetDepartmentQuery>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Infrastructure.Api.Department.Handlers.GetDepartment.Department?)null);

            //Act
            Func<Task> func = async () => await sut.UpdateDepartment(viewModel);

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>();

            mockMediator.Verify(_ => _.Send(
                It.IsAny<GetDepartmentQuery>(),
                It.IsAny<CancellationToken>()
            ));

            mockMediator.Verify(_ => _.Send(
                It.IsAny<UpdateDepartmentCommand>(),
                It.IsAny<CancellationToken>()
            ), Times.Never);
        }
    }

    public class DeleteDepartment
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task delete_department_given_name(
            [Frozen] Mock<IMediator> mediator,
            DepartmentService sut,
            Guid objectId
        )
        {
            //Arrange

            //Act
            await sut.DeleteDepartment(objectId);

            //Assert
            mediator.Verify(_ => _.Send(
                    It.IsAny<DeleteDepartmentCommand>(),
                    It.IsAny<CancellationToken>()
                )
            );
        }
    }
}
