using Ardalis.Result;
using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.AutoMapper;
using AW.Services.HumanResources.Core.Handlers.CreateEmployee;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace AW.Services.HumanResources.Core.UnitTests.Handlers
{
    public class CreateEmployeeCommandUnitTests
    {
        [Theory]
        [AutoMapperData(typeof(MappingProfile))]
        public async Task return_success_given_employee_was_created(
            [Frozen] Mock<IValidator<CreateEmployeeCommand>> validator,
            [Frozen] Mock<IRepository<Entities.Employee>> employeeRepo,
            CreateEmployeeCommandHandler sut,
            CreateEmployeeCommand command,
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

            command.Employee.MaritalStatus = nameof(Entities.MaritalStatus.Married);
            command.Employee.Gender = nameof(Entities.Gender.Male);

            employeeRepo.Setup(_ => _.AddAsync(
                    It.IsAny<Entities.Employee>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(employee);

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();

            result.Value.Should().BeEquivalentTo(employee, opt => opt
                .Excluding(_ => _.Id)
            );

            employeeRepo.Verify(x => x.AddAsync(
                It.IsAny<Entities.Employee>(),
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory, AutoMoqData]
        public async Task return_invalid_given_command_was_invalid(
            [Frozen] Mock<IRepository<Entities.Employee>> employeeRepo,
            [Frozen] Mock<IValidator<CreateEmployeeCommand>> validator,
            CreateEmployeeCommandHandler sut,
            CreateEmployeeCommand command,
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

            employeeRepo.Verify(x => x.AddAsync(
                    It.IsAny<Entities.Employee>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }

        [Theory, AutoMoqData]
        public async Task return_error_given_exception_was_thrown(
            [Frozen] Mock<IRepository<Entities.Employee>> employeeRepo,
            CreateEmployeeCommandHandler sut,
            CreateEmployeeCommand command
        )
        {
            //Arrange

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Status.Should().Be(ResultStatus.Error);

            employeeRepo.Verify(x => x.AddAsync(
                    It.IsAny<Entities.Employee>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }
    }
}
