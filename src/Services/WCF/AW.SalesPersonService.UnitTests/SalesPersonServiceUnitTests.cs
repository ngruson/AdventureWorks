using AW.Core.Application.SalesPerson.GetSalesPerson;
using AW.Core.Application.SalesPerson.GetSalesPersons;
using AW.SalesPersonService.Messages.GetSalesPerson;
using AW.SalesPersonService.Messages.ListSalesPersons;
using FluentAssertions;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AW.SalesPersonService.UnitTests
{
    [TestClass]
    public class SalesPersonServiceUnitTests
    {
        [TestMethod]
        public async Task ListCustomers_ReturnsCustomers()
        {
            //Arrange
            var dto = new List<Core.Application.SalesPerson.GetSalesPersons.SalesPersonDto>
            {
                new Core.Application.SalesPerson.GetSalesPersons.SalesPersonDto { FirstName = "Stephen" },
                new Core.Application.SalesPerson.GetSalesPersons.SalesPersonDto { FirstName = "Michael" }
            };

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<GetSalesPersonsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(dto);
            var salesPersonService = new SalesPersonService(
                mockMediator.Object
            );

            //Act
            var request = new ListSalesPersonsRequest();
            var result = await salesPersonService.ListSalesPersons(request);

            //Assert
            result.Should().NotBeNull();
            result.SalesPersons.Count().Should().Be(2);
        }

        [TestMethod]
        public async Task GetSalesPerson_ReturnsSalesPerson()
        {
            //Arrange
            var dto = new Core.Application.SalesPerson.GetSalesPerson.SalesPersonDto { FirstName = "Stephen" };

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<GetSalesPersonQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(dto);
            var salesPersonService = new SalesPersonService(
                mockMediator.Object
            );

            //Act
            var request = new GetSalesPersonRequest();
            var result = await salesPersonService.GetSalesPerson(request);

            //Assert
            result.Should().NotBeNull();
            result.SalesPerson.Should().NotBeNull();
        }
    }
}