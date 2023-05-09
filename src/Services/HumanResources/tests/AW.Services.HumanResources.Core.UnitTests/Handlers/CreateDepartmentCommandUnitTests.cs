using Ardalis.Result;
using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.AutoMapper;
using AW.Services.HumanResources.Core.Handlers.CreateDepartment;
using AW.Services.HumanResources.Core.Handlers.CreateShift;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace AW.Services.HumanResources.Core.UnitTests.Handlers
{
    public class CreateDepartmentCommandUnitTests
    {
        [Theory]
        [AutoMapperData(typeof(MappingProfile))]
        public async Task return_success_given_department_was_created(
            [Frozen] Mock<IValidator<CreateDepartmentCommand>> validatorMock,
            [Frozen] Mock<IRepository<Entities.Department>> departmentRepoMock,
            CreateDepartmentCommandHandler sut,
            CreateDepartmentCommand command,
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

            departmentRepoMock.Setup(_ => _.AddAsync(
                    It.IsAny<Entities.Department>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(department);

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();

            result.Value.Should().BeEquivalentTo(department, opt => opt
                .Excluding(_ => _.Id)
            );

            departmentRepoMock.Verify(x => x.AddAsync(
                It.IsAny<Entities.Department>(),
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory, AutoMoqData]
        public async Task return_invalid_given_command_was_invalid(
            [Frozen] Mock<IRepository<Entities.Department>> departmentRepoMock,
            [Frozen] Mock<IValidator<CreateDepartmentCommand>> validatorMock,
            CreateDepartmentCommandHandler sut,
            CreateDepartmentCommand command,
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

            departmentRepoMock.Verify(x => x.AddAsync(
                    It.IsAny<Entities.Department>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }

        [Theory, AutoMoqData]
        public async Task return_error_given_exception_was_thrown(
            [Frozen] Mock<IRepository<Entities.Department>> departmentRepoMock,
            CreateShiftCommandHandler sut,
            CreateShiftCommand command
        )
        {
            //Arrange

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Status.Should().Be(ResultStatus.Error);

            departmentRepoMock.Verify(x => x.AddAsync(
                    It.IsAny<Entities.Department>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }
    }
}
