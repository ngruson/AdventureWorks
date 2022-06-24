using AutoFixture.Xunit2;
using AW.SharedKernel.Caching;
using AW.UI.Web.SharedKernel.SalesPerson.Handlers.GetSalesPersons;
using FluentAssertions;
using Moq;

namespace AW.UI.Web.SharedKernel.UnitTests.SalesPerson.Handlers
{
    public class GetSalesPersonsQueryUnitTests
    {
        [Theory, AutoMoqData]
        public async Task Handle_SalesPersonsExists_ReturnSalesPersons(
            List<SharedKernel.SalesPerson.Handlers.GetSalesPersons.SalesPerson> salesPersons,
            [Frozen] Mock<ICache<SharedKernel.SalesPerson.Handlers.GetSalesPersons.SalesPerson>> cacheMock,
            GetSalesPersonsQueryHandler sut,
            GetSalesPersonsQuery query
        )
        {
            //Arrange
            cacheMock.Setup(x => x.GetData())
                .ReturnsAsync(salesPersons);

            query.Territory = "";

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            cacheMock.Verify(x => x.GetData());

            result.Should().BeEquivalentTo(salesPersons);
        }

        [Theory, AutoMoqData]
        public async Task Handle_TerritoryFilter_ReturnSalesPersons(
            List<SharedKernel.SalesPerson.Handlers.GetSalesPersons.SalesPerson> salesPersons,
            [Frozen] Mock<ICache<SharedKernel.SalesPerson.Handlers.GetSalesPersons.SalesPerson>> cacheMock,
            GetSalesPersonsQueryHandler sut,
            GetSalesPersonsQuery query
        )
        {
            //Arrange
            cacheMock.Setup(x => x.GetData(
                    It.IsAny<Func<SharedKernel.SalesPerson.Handlers.GetSalesPersons.SalesPerson, bool>>()
                )
            )
            .ReturnsAsync(salesPersons);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            cacheMock.Verify(x => x.GetData(
                    It.IsAny<Func<SharedKernel.SalesPerson.Handlers.GetSalesPersons.SalesPerson, bool>>()
                )
            );

            result.Should().BeEquivalentTo(salesPersons);
        }
    }
}