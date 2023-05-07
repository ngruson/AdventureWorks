using Ardalis.Result;
using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.AutoMapper;
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
        public async Task return_success_given_shift_exists(
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
            result.IsSuccess.Should().BeTrue();

            result.Value.Should().BeEquivalentTo(shift, opt => opt
                .Excluding(_ => _.Path.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase))
            );

            shiftRepoMock.Verify(x => x.SingleOrDefaultAsync(
                It.IsAny<GetShiftSpecification>(),
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory]
        [AutoMoqData]
        public async Task return_notfound_given_shift_does_not_exist(
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
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Status.Should().Be(ResultStatus.NotFound);
        }
    }
}
