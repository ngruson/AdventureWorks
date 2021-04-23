using AutoMapper;
using AW.Services.SalesPerson.Application.GetSalesPerson;
using AW.Services.SalesPerson.Application.GetSalesPersons;
using AW.Services.SalesPerson.WCF.Messages.GetSalesPerson;
using AW.Services.SalesPerson.WCF.Messages.ListSalesPersons;
using FluentAssertions;
using MediatR;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.SalesPerson.WCF.UnitTests
{
    public class SalesPersonServiceUnitTests
    {
        [Fact]
        public async Task ListCustomers_ReturnsCustomers()
        {
            //Arrange
            var mapper = new MapperConfiguration(opts =>
            {
                opts.AddProfile<MappingProfile>();
            }).CreateMapper();

            var dto = new List<Application.GetSalesPersons.SalesPersonDto>
            {
                new Application.GetSalesPersons.SalesPersonDto { FirstName = "Stephen" },
                new Application.GetSalesPersons.SalesPersonDto { FirstName = "Michael" }
            };

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<GetSalesPersonsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(dto);
            var salesPersonService = new SalesPersonService(
                mockMediator.Object,
                mapper
            );

            //Act
            var request = new ListSalesPersonsRequest();
            var result = await salesPersonService.ListSalesPersons(request);

            //Assert
            result.Should().NotBeNull();
            result.SalesPersons.Count().Should().Be(2);
        }

        [Fact]
        public async Task GetSalesPerson_ReturnsSalesPerson()
        {
            //Arrange
            var mapper = new MapperConfiguration(opts =>
            {
                opts.AddProfile<MappingProfile>();
            }).CreateMapper();

            var dto = new Application.GetSalesPerson.SalesPersonDto { FirstName = "Stephen" };

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<GetSalesPersonQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(dto);
            var salesPersonService = new SalesPersonService(
                mockMediator.Object,
                mapper
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