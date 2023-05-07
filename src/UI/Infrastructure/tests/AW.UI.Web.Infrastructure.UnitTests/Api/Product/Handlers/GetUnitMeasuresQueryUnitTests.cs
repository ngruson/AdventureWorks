using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using AW.UI.Web.Infrastructure.Api.Product.Handlers.GetUnitMeasures;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.Product.Handlers
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
            mockProductApiClient.Setup(_ => _.GetUnitMeasures())
                .ReturnsAsync((List<UnitMeasure>?)null);

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>();

            mockProductApiClient.Verify(_ => _.GetUnitMeasures());
        }
    }
}
