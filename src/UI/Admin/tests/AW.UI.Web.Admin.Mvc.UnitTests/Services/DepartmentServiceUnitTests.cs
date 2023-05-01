using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Admin.Mvc.Services;
using AW.UI.Web.Admin.Mvc.ViewModels.Department;
using AW.UI.Web.Admin.Mvc.ViewModels.Product;
using AW.UI.Web.SharedKernel.Department.Handlers.DeleteDepartment;
using AW.UI.Web.SharedKernel.Department.Handlers.GetDepartment;
using AW.UI.Web.SharedKernel.Department.Handlers.GetDepartments;
using AW.UI.Web.SharedKernel.Department.Handlers.UpdateDepartment;
using FluentAssertions;
using MediatR;
using Moq;
using Xunit;

namespace AW.UI.Web.Admin.Mvc.UnitTests.Services
{
    public class DepartmentServiceUnitTests
    {
        public class GetDepartments
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task ReturnsViewModel(
                [Frozen] Mock<IMediator> mockMediator,
                DepartmentService sut,
                List<SharedKernel.Department.Handlers.GetDepartments.Department> departments
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
                SharedKernel.Department.Handlers.GetDepartment.Department department,
                string name
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
                var actual = await sut.GetDetail(name);

                //Assert
                actual.Department.Should().BeEquivalentTo(department);

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
                EditDepartmentViewModel viewModel,
                SharedKernel.Department.Handlers.GetDepartment.Department department
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
                EditDepartmentViewModel viewModel
            )
            {
                //Arrange
                mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetDepartmentQuery>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync((SharedKernel.Department.Handlers.GetDepartment.Department?)null);

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
                string name
            )
            {
                //Arrange

                //Act
                await sut.DeleteDepartment(name);

                //Assert
                mediator.Verify(_ => _.Send(
                        It.IsAny<DeleteDepartmentCommand>(),
                        It.IsAny<CancellationToken>()
                    )
                );
            }
        }
    }
}
