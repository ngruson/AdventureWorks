using AutoFixture.Xunit2;
using AW.UI.Web.SharedKernel.Interfaces.Api;
using AW.UI.Web.SharedKernel.Product.Handlers.GetUnitMeasures;
using FluentAssertions;
using Moq;

namespace AW.UI.Web.SharedKernel.UnitTests.Product.Handlers
{
    public class GetUnitMeasuresQueryUnitTests
    {
        [Theory, AutoMoqData]
        public async Task ReturnUnitMeasuresGivenUnitMeasuresExist(
            [Frozen] Mock<IProductApiClient> mockProductApiClient,
            GetUnitMeasuresQueryHandler sut,
            GetUnitMeasuresQuery query,
            List<UnitMeasure> unitMeasures
        )
        {
            //Arrange
            mockProductApiClient.Setup(_ => _.GetUnitMeasures())
                .ReturnsAsync(unitMeasures);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().BeEquivalentTo(unitMeasures);

            mockProductApiClient.Verify(_ => _.GetUnitMeasures());
        }

        [Theory, AutoMoqData]
        public async Task ThrowArgumentNullExceptionGivenProductsAreNull(
            [Frozen] Mock<IProductApiClient> mockProductApiClient,
            GetUnitMeasuresQueryHandler sut,
            GetUnitMeasuresQuery query
        )
        {
            //Arrange

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>();

            mockProductApiClient.Verify(_ => _.GetUnitMeasures());
        }
    }
}
