using Ardalis.Result;
using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.Handlers.DeleteEmployee;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace AW.Services.HumanResources.Core.UnitTests.Handlers
{
    public class DeleteEmployeeCommandUnitTests
    {
        [Theory, AutoMoqData]
        public async Task return_success_given_employee_was_deleted(
            [Frozen] Mock<IValidator<DeleteEmployeeCommand>> validator,
            [Frozen] Mock<IRepository<Entities.Employee>> employeeRepo,
            DeleteEmployeeCommandHandler sut,
            DeleteEmployeeCommand command,
            Entities.Employee employee
        )
        {
            //Arrange
            validator.Setup(_ => _.ValidateAsync(
                    command,
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(new ValidationResult());

            employeeRepo.Setup(_ => _.SingleOrDefaultAsync(
                    It.IsAny<GetEmployeeSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(employee);

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();

            employeeRepo.Verify(x => x.SingleOrDefaultAsync(
                    It.IsAny<GetEmployeeSpecification>(),
                    It.IsAny<CancellationToken>()
                ) 
            );

            employeeRepo.Verify(x => x.DeleteAsync(
                employee,
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory, AutoMoqData]
        public async Task return_invalid_given_command_was_invalid(
            [Frozen] Mock<IRepository<Entities.Employee>> employeeRepo,
            [Frozen] Mock<IValidator<DeleteEmployeeCommand>> validator,
            DeleteEmployeeCommandHandler sut,
            DeleteEmployeeCommand command,
            List<ValidationFailure> failures
        )
        {
            //Arrange
            validator.Setup(_ => _.ValidateAsync(
                    command,
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(new ValidationResult(failures));

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Status.Should().Be(ResultStatus.Invalid);

            employeeRepo.Verify(x => x.SingleOrDefaultAsync(
                    It.IsAny<GetEmployeeSpecification>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );

            employeeRepo.Verify(x => x.DeleteAsync(
                    It.IsAny<Entities.Employee>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }

        [Theory, AutoMoqData]
        public async Task return_notfound_given_employee_does_not_exist(
            [Frozen] Mock<IValidator<DeleteEmployeeCommand>> validator,
            [Frozen] Mock<IRepository<Entities.Employee>> employeeRepo,
            DeleteEmployeeCommandHandler sut,
            DeleteEmployeeCommand command
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
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Status.Should().Be(ResultStatus.NotFound);

            employeeRepo.Verify(x => x.SingleOrDefaultAsync(
                    It.IsAny<GetEmployeeSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            );

            employeeRepo.Verify(x => x.DeleteAsync(
                    It.IsAny<Entities.Employee>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }

        [Theory, AutoMoqData]
        public async Task return_error_given_exception_was_thrown(
            DeleteEmployeeCommandHandler sut,
            DeleteEmployeeCommand command
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
