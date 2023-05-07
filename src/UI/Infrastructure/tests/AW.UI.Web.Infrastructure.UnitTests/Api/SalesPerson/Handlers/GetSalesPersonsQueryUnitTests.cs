using AutoFixture.Xunit2;
using AW.SharedKernel.Caching;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.SalesPerson.Handlers.GetSalesPersons;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.SalesPerson.Handlers
{
    public class GetSalesPersonsQueryUnitTests
    {
        [Theory, AutoMoqData]
        public async Task Handle_SalesPersonsExists_ReturnSalesPersons(
            List<Infrastructure.Api.SalesPerson.Handlers.GetSalesPersons.SalesPerson> salesPersons,
            [Frozen] Mock<ICache<Infrastructure.Api.SalesPerson.Handlers.GetSalesPersons.SalesPerson>> cacheMock,
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
            List<Infrastructure.Api.SalesPerson.Handlers.GetSalesPersons.SalesPerson> salesPersons,
            [Frozen] Mock<ICache<Infrastructure.Api.SalesPerson.Handlers.GetSalesPersons.SalesPerson>> cacheMock,
            GetSalesPersonsQueryHandler sut,
            GetSalesPersonsQuery query
        )
        {
            //Arrange
            cacheMock.Setup(x => x.GetData(
                    It.IsAny<Func<Infrastructure.Api.SalesPerson.Handlers.GetSalesPersons.SalesPerson, bool>>()
                )
            )
            .ReturnsAsync(salesPersons);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            cacheMock.Verify(x => x.GetData(
                    It.IsAny<Func<Infrastructure.Api.SalesPerson.Handlers.GetSalesPersons.SalesPerson, bool>>()
                )
            );

            result.Should().BeEquivalentTo(salesPersons);
        }
    }
}
