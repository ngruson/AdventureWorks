using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.AutoMapper;
using AW.Services.HumanResources.Core.Exceptions;
using AW.Services.HumanResources.Core.Handlers.GetShift;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;

namespace AW.Services.HumanResources.Core.UnitTests.Handlers
{
    public class GetShiftQueryUnitTests
    {
        [Theory]
        [AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_ShiftExists_ReturnShift(
            [Frozen] Mock<IRepository<Entities.Shift>> shiftRepoMock,
            GetShiftQueryHandler sut,
            GetShiftQuery query,
            Entities.Shift shift
        )
        {
            //Arrange
            shiftRepoMock.Setup(_ => _.SingleOrDefaultAsync(
                It.IsAny<GetShiftSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(shift);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            shiftRepoMock.Verify(x => x.SingleOrDefaultAsync(
                It.IsAny<GetShiftSpecification>(),
                It.IsAny<CancellationToken>()
            ));

            result.Should().BeEquivalentTo(shift, opt => opt
                .Excluding(_ => _.Path.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase))
            );
        }

        [Theory]
        [AutoMoqData]
        public async Task Handle_ShiftNotFound_ThrowArgumentNullException(
            [Frozen] Mock<IRepository<Entities.Shift>> shiftRepoMock,
            GetShiftQueryHandler sut,
            GetShiftQuery query
        )
        {
            // Arrange
            shiftRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetShiftSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.Shift?)null);

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ShiftNotFoundException>()
                .WithMessage($"Shift '{query.Name}' not found");
        }
    }
}