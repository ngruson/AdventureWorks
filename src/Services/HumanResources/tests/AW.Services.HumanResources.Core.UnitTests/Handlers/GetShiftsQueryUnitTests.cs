using Ardalis.Result;
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
        public async Task return_success_given_shifts_exist(
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
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(shifts, opt => opt
                .Excluding(_ => _.Id)
            );
            shiftRepoMock.Verify(x => x.ListAsync(
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory]
        [AutoMoqData]
        public async Task return_notfound_given_shifts_do_not_exist(
            [Frozen] Mock<IRepository<Entities.Shift>> shiftRepoMock,
            GetShiftsQueryHandler sut,
            GetShiftsQuery query
        )
        {
            // Arrange            
            #pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            shiftRepoMock.Setup(x => x.ListAsync(
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((List<Entities.Shift>?)null);
            #pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Status.Should().Be(ResultStatus.NotFound);
        }
    }
}
