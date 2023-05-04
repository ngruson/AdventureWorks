using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.AutoMapper;
using AW.Services.HumanResources.Core.Exceptions;
using AW.Services.HumanResources.Core.Handlers.UpdateShift;
using AW.Services.HumanResources.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;

namespace AW.Services.HumanResources.Core.UnitTests.Handlers
{
    public class UpdateShiftCommandUnitTests
    {
        [Theory]
        [AutoMapperData(typeof(MappingProfile))]
        public async Task return_updated_shift_given_shift_exists(
            [Frozen] Mock<IRepository<Entities.Shift>> shiftRepoMock,
            Entities.Shift shift,
            UpdateShiftCommandHandler sut,
            UpdateShiftCommand command
        )
        {
            //Arrange
            shiftRepoMock.Setup(_ => _.SingleOrDefaultAsync(
                    It.IsAny<GetShiftSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            ).
            ReturnsAsync(shift);

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            shiftRepoMock.Verify(x => x.SingleOrDefaultAsync(
                It.IsAny<GetShiftSpecification>(),
                It.IsAny<CancellationToken>()
            ));
            shiftRepoMock.Verify(x => x.UpdateAsync(
                It.IsAny<Entities.Shift>(),
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory]
        [AutoMoqData]
        public async Task throw_shiftnotfoundexception_given_shift_does_not_exist(
            [Frozen] Mock<IRepository<Entities.Shift>> shiftRepoMock,
            UpdateShiftCommandHandler sut,
            UpdateShiftCommand command
        )
        {
            // Arrange
            shiftRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetShiftSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.Shift?)null);

            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ShiftNotFoundException>()
                .WithMessage($"Shift '{command.Key}' not found");
        }
    }
}
