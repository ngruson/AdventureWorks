using AutoMapper;
using AW.Services.SalesPerson.Core.Handlers.GetSalesPerson;
using AW.Services.SalesPerson.Core.Handlers.GetSalesPersons;
using AW.Services.SalesPerson.REST.API.Controllers;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.SalesPerson.REST.API.UnitTests
{
    public class SalesPersonControllerUnitTests
    {
        [Fact]
        public async Task GetSalesPersons_ShouldReturnSalesPersons_WhenGivenSalesPersons()
        {
            //Arrange
            var mapper = new MapperConfiguration(opts =>
            {
                opts.AddProfile<MappingProfile>();
            }).CreateMapper();

            var dto = new List<Core.Handlers.GetSalesPersons.SalesPersonDto>
            {
                new Core.Handlers.GetSalesPersons.SalesPersonDto { FirstName = "Stephen" },
                new Core.Handlers.GetSalesPersons.SalesPersonDto { FirstName = "Michael" }
            };

            var mockLogger = new Mock<ILogger<SalesPersonController>>();
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<GetSalesPersonsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(dto);

            var controller = new SalesPersonController(
                mockLogger.Object,
                mockMediator.Object,
                mapper
            );

            //Act
            var request = new GetSalesPersonsQuery();
            var actionResult = await controller.GetSalesPersons(request);

            //Assert
            var okObjectResult = actionResult as OkObjectResult;
            okObjectResult.Should().NotBeNull();

            var salesPersons = okObjectResult.Value as List<Models.SalesPerson>;
            salesPersons.Count.Should().Be(2);
        }

        [Fact]
        public async Task GetSalesPersons_ShouldReturnNotFound_WhenGivenNoSalesPersons()
        {
            //Arrange
            var mapper = new MapperConfiguration(opts =>
            {
                opts.AddProfile<MappingProfile>();
            }).CreateMapper();            
            
            var mockLogger = new Mock<ILogger<SalesPersonController>>();
            var mockMediator = new Mock<IMediator>();

            var controller = new SalesPersonController(
                mockLogger.Object,
                mockMediator.Object,
                mapper
            );

            //Act
            var request = new GetSalesPersonsQuery();
            var actionResult = await controller.GetSalesPersons(request);

            //Assert
            var notFoundResult = actionResult as NotFoundResult;
            notFoundResult.Should().NotBeNull();
        }

        [Fact]
        public async Task GetSalesPerson_ShouldReturnSalesPerson_GivenSalesPerson()
        {
            //Arrange
            var mapper = new MapperConfiguration(opts =>
            {
                opts.AddProfile<MappingProfile>();
            }).CreateMapper();

            var dto = new Core.Handlers.GetSalesPerson.SalesPersonDto { FirstName = "Stephen" };

            var mockLogger = new Mock<ILogger<SalesPersonController>>();
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<GetSalesPersonQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(dto);

            var controller = new SalesPersonController(
                mockLogger.Object,
                mockMediator.Object,
                mapper
            );

            //Act
            var request = new GetSalesPersonQuery();
            var actionResult = await controller.GetSalesPerson(request);

            //Assert
            var okObjectResult = actionResult as OkObjectResult;
            okObjectResult.Should().NotBeNull();

            var salesPerson = okObjectResult.Value as Models.SalesPerson;
            salesPerson.Should().NotBeNull();
        }

        [Fact]
        public async Task GetSalesPerson_ShouldReturnNotFound_GivenNoSalesPerson()
        {
            //Arrange
            var mapper = new MapperConfiguration(opts =>
            {
                opts.AddProfile<MappingProfile>();
            }).CreateMapper();

            var mockLogger = new Mock<ILogger<SalesPersonController>>();
            var mockMediator = new Mock<IMediator>();

            var controller = new SalesPersonController(
                mockLogger.Object,
                mockMediator.Object,
                mapper
            );

            //Act
            var request = new GetSalesPersonQuery();
            var actionResult = await controller.GetSalesPerson(request);

            //Assert
            var notFoundResult = actionResult as NotFoundResult;
            notFoundResult.Should().NotBeNull();
        }
    }
}