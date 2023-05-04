using AutoFixture.Xunit2;
using AW.Services.HumanResources.Core.AutoMapper;
using AW.Services.HumanResources.Core.Handlers.CreateShift;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using Moq;

namespace AW.Services.HumanResources.Core.UnitTests.Handlers
{
    public class CreateShiftCommandUnitTests
    {
        [Theory]
        [AutoMapperData(typeof(MappingProfile))]
        public async Task return_shift_given_shift_was_created(
            [Frozen] Mock<IRepository<Entities.Shift>> shiftRepoMock,
            CreateShiftCommandHandler sut,
            CreateShiftCommand command,
            Entities.Shift shift
        )
        {
            //Arrange
            command.Shift.StartTime = "07:00:00";
            command.Shift.EndTime = "09:00:00";

            shiftRepoMock.Setup(_ => _.AddAsync(
                    It.IsAny<Entities.Shift>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(shift);

            //Act
            var actual = await sut.Handle(command, CancellationToken.None);

            //Assert

            shiftRepoMock.Verify(x => x.AddAsync(
                It.IsAny<Entities.Shift>(),
                It.IsAny<CancellationToken>()
            ));
        }
    }
}
