using AutoFixture.Xunit2;
using AW.SharedKernel.Caching;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.ReferenceData.Handlers.GetStatesProvinces;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.ReferenceData.Handlers
{
    public class GetStatesProvincesQueryUnitTests
    {
        [Theory, AutoMoqData]
        public async Task Handle_ReturnStatesProvinces(
            List<StateProvince> statesProvinces,
            [Frozen] Mock<ICache<StateProvince>> cacheMock,
            GetStatesProvincesQueryHandler sut,
            GetStatesProvincesQuery query
        )
        {
            //Arrange
            cacheMock.Setup(x => x.GetData(
                    It.IsAny<Func<StateProvince, bool>>()
                )
            )
            .ReturnsAsync(statesProvinces);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            cacheMock.Verify(x => x.GetData(
                    It.IsAny<Func<StateProvince, bool>>()
                )
            );
            result.Should().BeEquivalentTo(statesProvinces);
        }
    }
}
