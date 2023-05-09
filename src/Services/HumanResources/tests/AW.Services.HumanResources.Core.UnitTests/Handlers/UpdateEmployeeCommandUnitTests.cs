using Ardalis.Result;
using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.AutoMapper;
using AW.Services.HumanResources.Core.Handlers.UpdateDepartment;
using AW.Services.HumanResources.Core.Handlers.UpdateEmployee;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace AW.Services.HumanResources.Core.UnitTests.Handlers
{
    public class UpdateEmployeeCommandUnitTests
    {
        [Theory]
        [AutoMapperData(typeof(MappingProfile))]
        public async Task return_success_given_employee_was_updated(
            [Frozen] Mock<IValidator<UpdateEmployeeCommand>> validatorMock,
            [Frozen] Mock<IRepository<Entities.Employee>> employeeRepoMock,
            Entities.Employee employee,
            UpdateEmployeeCommandHandler sut,
            UpdateEmployeeCommand command
        )
        {
            //Arrange
            command.Employee!.MaritalStatus = Entities.MaritalStatus.Married.Name;
            command.Employee!.Gender = Entities.Gender.Male.Name;

            validatorMock.Setup(_ => _.ValidateAsync(
                    command,
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(new ValidationResult());

            employeeRepoMock.Setup(_ => _.SingleOrDefaultAsync(
                    It.IsAny<GetEmployeeSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            ).
            ReturnsAsync(employee);

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();

            employeeRepoMock.Verify(x => x.SingleOrDefaultAsync(
                It.IsAny<GetEmployeeSpecification>(),
                It.IsAny<CancellationToken>()
            ));
            employeeRepoMock.Verify(x => x.UpdateAsync(
                It.IsAny<Entities.Employee>(),
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory, AutoMoqData]
        public async Task return_invalid_given_command_was_invalid(
            [Frozen] Mock<IValidator<UpdateEmployeeCommand>> validatorMock,
            [Frozen] Mock<IRepository<Entities.Employee>> employeeRepoMock,
            UpdateEmployeeCommandHandler sut,
            UpdateEmployeeCommand command,
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

            employeeRepoMock.Verify(x => x.SingleOrDefaultAsync(
                    It.IsAny<GetEmployeeSpecification>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );

            employeeRepoMock.Verify(x => x.UpdateAsync(
                    It.IsAny<Entities.Employee>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }

        [Theory]
        [AutoMoqData]
        public async Task return_notfound_given_employee_does_not_exist(
            [Frozen] Mock<IValidator<UpdateEmployeeCommand>> validatorMock,
            [Frozen] Mock<IRepository<Entities.Employee>> employeeRepoMock,
            UpdateEmployeeCommandHandler sut,
            UpdateEmployeeCommand command
        )
        {
            // Arrange
            validatorMock.Setup(_ => _.ValidateAsync(
                    command,
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(new ValidationResult());

            employeeRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetEmployeeSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.Employee?)null);

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Status.Should().Be(ResultStatus.NotFound);
            result.Errors.Should().Contain($"Employee {command.Key} not found");

            employeeRepoMock.Verify(x => x.SingleOrDefaultAsync(
                It.IsAny<GetEmployeeSpecification>(),
                It.IsAny<CancellationToken>()
            ));

            employeeRepoMock.Verify(x => x.UpdateAsync(
                    It.IsAny<Entities.Employee>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }
    }
}
