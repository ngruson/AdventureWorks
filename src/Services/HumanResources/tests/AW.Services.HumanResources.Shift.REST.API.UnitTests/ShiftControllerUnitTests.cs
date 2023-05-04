using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.AutoMapper;
using AW.Services.HumanResources.Core.Exceptions;
using AW.Services.HumanResources.Core.Handlers.CreateShift;
using AW.Services.HumanResources.Core.Handlers.DeleteShift;
using AW.Services.HumanResources.Core.Handlers.GetShift;
using AW.Services.HumanResources.Core.Handlers.GetShifts;
using AW.Services.HumanResources.Core.Handlers.UpdateShift;
using AW.Services.HumanResources.Shift.REST.API.Controllers;
using AW.Services.Infrastructure.ActionResults;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AW.Services.HumanResources.Shift.REST.API.UnitTests
{
    public class ShiftControllerUnitTests
    {
        public class GetShifts
        {
            [Theory, AutoMoqData]
            public async Task GetShifts_ShiftsExists_ShouldReturnShifts(

                [Frozen] Mock<IMediator> mockMediator,
                List<Core.Handlers.GetShifts.Shift> shifts,
                [Greedy] ShiftController sut,
                GetShiftsQuery query
            )
            {
                //Arrange
                mockMediator.Setup(x => x.Send(
                        It.IsAny<GetShiftsQuery>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(shifts);

                //Act
                var actionResult = await sut.GetShifts(query);

                //Assert
                var okObjectResult = actionResult as OkObjectResult;
                okObjectResult.Should().NotBeNull();

                var response = okObjectResult?.Value as List<Core.Handlers.GetShifts.Shift>;
                response?.Should().BeEquivalentTo(shifts);
            }

            [Theory]
            [AutoMoqData]
            public async Task GetShifts_NoShifts_ShouldReturnNotFound(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] ShiftController sut,
                GetShiftsQuery query
            )
            {
                //Arrange                
                mockMediator.Setup(x => x.Send(
                    It.IsAny<GetShiftsQuery>(),
                    It.IsAny<CancellationToken>()
                ))
                .Throws<ShiftsNotFoundException>();

                //Act
                var actionResult = await sut.GetShifts(query);

                //Assert
                var notFoundResult = actionResult as NotFoundResult;
                notFoundResult.Should().NotBeNull();
            }
        }

        public class GetShift
        {
            [Theory]
            [AutoMoqData]
            public async Task GetShift_ShiftExists_ShouldReturnShift(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] ShiftController sut,
                GetShiftQuery query,
                Core.Handlers.GetShift.Shift shift
            )
            {
                //Arrange
                mockMediator.Setup(x => x.Send(
                    It.IsAny<GetShiftQuery>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(shift);

                //Act
                var actionResult = await sut.GetShift(query);

                //Assert
                var okObjectResult = actionResult as OkObjectResult;
                okObjectResult.Should().NotBeNull();

                var result = okObjectResult?.Value as Core.Handlers.GetShift.Shift;
                result.Should().NotBeNull();
            }

            [Theory]
            [AutoMoqData]
            public async Task GetShift_ShiftDoesNotExists_ShouldReturnNotFound(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] ShiftController sut,
                GetShiftQuery query
            )
            {
                //Arrange
                mockMediator.Setup(x => x.Send(
                    It.IsAny<GetShiftQuery>(),
                    It.IsAny<CancellationToken>()
                ))
                .ThrowsAsync(new ShiftNotFoundException(query.Name!));

                //Act
                var actionResult = await sut.GetShift(query);

                //Assert
                var notFoundResult = actionResult as NotFoundResult;
                notFoundResult.Should().NotBeNull();
            }
        }

        public class CreateShift
        {
            [Theory, AutoMoqData]
            public async Task return_ok_given_shift_is_created(
                [Frozen] Mock<IMediator> mockMediator,
                [Frozen] Mock<IValidator<CreateShiftCommand>> validator,
                [Greedy] ShiftController sut,
                Core.Handlers.CreateShift.Shift shift
            )
            {
                //Arrange
                validator.Setup(_ => _.ValidateAsync(
                        It.IsAny<CreateShiftCommand>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(new ValidationResult());

                mockMediator.Setup(x => x.Send(
                    It.IsAny<CreateShiftCommand>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(shift);

                //Act
                var actionResult = await sut.CreateShift(shift);

                //Assert
                var okObjectResult = actionResult as OkObjectResult;
                okObjectResult.Should().NotBeNull();

                var response = okObjectResult?.Value;
                response?.Should().Be(shift);
            }

            [Theory, AutoMoqData]
            public async Task return_badrequest_given_command_is_invalid(
                [Frozen] Mock<IValidator<CreateShiftCommand>> validator,
                [Greedy] ShiftController sut,
                Core.Handlers.CreateShift.Shift shift,
                List<ValidationFailure> validationFailures
            )
            {
                //Arrange
                validator.Setup(_ => _.ValidateAsync(
                        It.IsAny<CreateShiftCommand>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(new ValidationResult(validationFailures));

                //Act
                var actionResult = await sut.CreateShift(shift);

                //Assert
                var result = actionResult as BadRequestObjectResult;
                result!.Value.Should().BeOfType<ProblemHttpResult>();
            }

            [Theory, AutoMoqData]
            public async Task return_internalservererror_given_exception_occurs(
                [Frozen] Mock<IValidator<CreateShiftCommand>> validator,
                [Greedy] ShiftController sut,
                Core.Handlers.CreateShift.Shift shift
            )
            {
                //Arrange
                validator.Setup(_ => _.ValidateAsync(
                        It.IsAny<CreateShiftCommand>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ThrowsAsync(new Exception());

                //Act
                var actionResult = await sut.CreateShift(shift);

                //Assert
                actionResult.Should().BeOfType<InternalServerErrorObjectResult>();
            }
        }

        public class UpdateShift
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task return_ok_given_shift_is_updated(
                [Frozen] Mock<IMediator> mockMediator,
                [Frozen] Mock<IValidator<UpdateShiftCommand>> validator,
                [Greedy] ShiftController sut,
                UpdateShiftCommand command,
                Core.Handlers.UpdateShift.Shift shift
            )
            {
                //Arrange
                validator.Setup(_ => _.ValidateAsync(
                        command,
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(new ValidationResult());

                mockMediator.Setup(x => x.Send(
                    command,
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(shift);

                //Act
                var actionResult = await sut.UpdateShift(command);

                //Assert
                var okObjectResult = actionResult as OkObjectResult;
                okObjectResult.Should().NotBeNull();

                var response = okObjectResult?.Value;
                response?.Should().Be(shift);
            }

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task return_notfound_given_shift_does_not_exist(
                [Frozen] Mock<IMediator> mockMediator,
                [Frozen] Mock<IValidator<UpdateShiftCommand>> validator,
                [Greedy] ShiftController sut,
                UpdateShiftCommand command
            )
            {
                //Arrange
                validator.Setup(_ => _.ValidateAsync(
                        command,
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(new ValidationResult());

                mockMediator.Setup(x => x.Send(
                        It.IsAny<UpdateShiftCommand>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ThrowsAsync(new ShiftNotFoundException(command.Shift!.Name!));

                //Act
                var actionResult = await sut.UpdateShift(command);

                //Assert
                actionResult.Should().BeOfType<NotFoundResult>();
            }

            [Theory, AutoMoqData]
            public async Task return_badrequest_given_command_is_invalid(
                [Frozen] Mock<IValidator<UpdateShiftCommand>> validator,
                [Greedy] ShiftController sut,
                UpdateShiftCommand command,
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
                var actionResult = await sut.UpdateShift(command);

                //Assert
                var result = actionResult as BadRequestObjectResult;
                result!.Value.Should().BeOfType<ProblemHttpResult>();
            }

            [Theory, AutoMoqData]
            public async Task return_internalservererror_given_exception_occurs(
                [Frozen] Mock<IValidator<UpdateShiftCommand>> validator,
                [Greedy] ShiftController sut,
                UpdateShiftCommand command
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
                var actionResult = await sut.UpdateShift(command);

                //Assert
                actionResult.Should().BeOfType<InternalServerErrorObjectResult>();
            }
        }

        public class DeleteShift
        {
            [Theory, AutoMoqData]
            public async Task return_ok_given_command_is_valid(
                [Frozen] Mock<IMediator> mockMediator,
                [Frozen] Mock<IValidator<DeleteShiftCommand>> validator,
                [Greedy] ShiftController sut,
                DeleteShiftCommand command
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
                var actionResult = await sut.DeleteShift(command);

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
                [Frozen] Mock<IValidator<DeleteShiftCommand>> validator,
                [Greedy] ShiftController sut,
                DeleteShiftCommand command,
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
                var actionResult = await sut.DeleteShift(command);

                //Assert
                var result = actionResult as BadRequestObjectResult;
                result!.Value.Should().BeOfType<ProblemHttpResult>();
            }

            [Theory, AutoMoqData]
            public async Task return_internalservererror_given_exception_occurs(
                [Frozen] Mock<IValidator<DeleteShiftCommand>> validator,
                [Greedy] ShiftController sut,
                DeleteShiftCommand command
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
                var actionResult = await sut.DeleteShift(command);

                //Assert
                actionResult.Should().BeOfType<InternalServerErrorObjectResult>();
            }
        }
    }
}
