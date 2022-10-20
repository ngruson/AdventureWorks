using Ardalis.Specification;
using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.AutoMapper;
using AW.Services.HumanResources.Core.Exceptions;
using AW.Services.HumanResources.Core.Handlers.GetShifts;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;

namespace AW.Services.HumanResources.Core.UnitTests.Handlers
{
    public class GetShiftsQueryUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_ShiftsExists_ReturnShifts(
            [Frozen] Mock<IRepository<Entities.Shift>> shiftRepoMock,
            GetShiftsQueryHandler sut,
            GetShiftsQuery query,
            List<Entities.Shift> shifts
        )
        {
            // Arrange
            shiftRepoMock.Setup(x => x.ListAsync(
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(shifts);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert            
            result.Should().BeEquivalentTo(shifts, opt => opt
                .Excluding(_ => _.Id)
            );
            shiftRepoMock.Verify(x => x.ListAsync(
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory]
        [AutoMoqData]
        public async Task Handle_NoShiftsExists_ThrowArgumentNullException(
            [Frozen] Mock<IRepository<Entities.Shift>> shiftRepoMock,
            GetShiftsQueryHandler sut,
            GetShiftsQuery query
        )
        {
            // Arrange            
            shiftRepoMock.Setup(x => x.ListAsync(
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((List<Entities.Shift>?)null);

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ShiftsNotFoundException>()
                .WithMessage("No shifts found");
        }
    }
}