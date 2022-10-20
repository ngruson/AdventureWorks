using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.Exceptions;
using AW.Services.HumanResources.Core.Handlers.GetShift;
using AW.Services.HumanResources.Core.Handlers.GetShifts;
using AW.Services.HumanResources.Shift.REST.API.Controllers;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using MediatR;
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
                .ThrowsAsync(new ShiftNotFoundException(query.Name));

                //Act
                var actionResult = await sut.GetShift(query);

                //Assert
                var notFoundResult = actionResult as NotFoundResult;
                notFoundResult.Should().NotBeNull();
            }
        }
    }
}