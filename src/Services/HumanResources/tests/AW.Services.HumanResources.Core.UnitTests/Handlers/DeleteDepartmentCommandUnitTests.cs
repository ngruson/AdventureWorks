using Ardalis.Result;
using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.Handlers.CreateShift;
using AW.Services.HumanResources.Core.Handlers.DeleteDepartment;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace AW.Services.HumanResources.Core.UnitTests.Handlers
{
    public class DeleteDepartmentCommandUnitTests
    {
        [Theory, AutoMoqData]
        public async Task return_success_given_department_was_deleted(
            [Frozen] Mock<IValidator<DeleteDepartmentCommand>> validatorMock,
            [Frozen] Mock<IRepository<Entities.Department>> departmentRepoMock,
            DeleteDepartmentCommandHandler sut,
            DeleteDepartmentCommand command,
            Entities.Department department
        )
        {
            //Arrange
            validatorMock.Setup(_ => _.ValidateAsync(
                    command,
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(new ValidationResult());

            departmentRepoMock.Setup(_ => _.SingleOrDefaultAsync(
                    It.IsAny<GetDepartmentSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(department);

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();

            departmentRepoMock.Verify(x => x.SingleOrDefaultAsync(
                    It.IsAny<GetDepartmentSpecification>(),
                    It.IsAny<CancellationToken>()
                ) 
            );

            departmentRepoMock.Verify(x => x.DeleteAsync(
                department,
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory, AutoMoqData]
        public async Task return_invalid_given_command_was_invalid(
            [Frozen] Mock<IRepository<Entities.Department>> departmentRepoMock,
            [Frozen] Mock<IValidator<DeleteDepartmentCommand>> validatorMock,
            DeleteDepartmentCommandHandler sut,
            DeleteDepartmentCommand command,
            List<ValidationFailure> failures
        )
        {
            //Arrange
            validatorMock.Setup(_ => _.ValidateAsync(
                    command,
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(new ValidationResult(failures));

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Status.Should().Be(ResultStatus.Invalid);

            departmentRepoMock.Verify(x => x.SingleOrDefaultAsync(
                    It.IsAny<GetDepartmentSpecification>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );

            departmentRepoMock.Verify(x => x.DeleteAsync(
                    It.IsAny<Entities.Department>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }

        [Theory, AutoMoqData]
        public async Task return_notfound_given_department_does_not_exist(
            [Frozen] Mock<IValidator<DeleteDepartmentCommand>> validatorMock,
            [Frozen] Mock<IRepository<Entities.Department>> departmentRepoMock,
            DeleteDepartmentCommandHandler sut,
            DeleteDepartmentCommand command
        )
        {
            //Arrange
            validatorMock.Setup(_ => _.ValidateAsync(
                    command,
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(new ValidationResult());

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Status.Should().Be(ResultStatus.NotFound);

            departmentRepoMock.Verify(x => x.SingleOrDefaultAsync(
                    It.IsAny<GetDepartmentSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            );

            departmentRepoMock.Verify(x => x.DeleteAsync(
                    It.IsAny<Entities.Department>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }

        [Theory, AutoMoqData]
        public async Task return_error_given_exception_was_thrown(
            DeleteDepartmentCommandHandler sut,
            DeleteDepartmentCommand command
        )
        {
            //Arrange

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Status.Should().Be(ResultStatus.Error);
        }
    }
}
