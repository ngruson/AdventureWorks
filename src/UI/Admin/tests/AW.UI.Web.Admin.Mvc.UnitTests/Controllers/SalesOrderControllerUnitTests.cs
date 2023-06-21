using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Admin.Mvc.Controllers;
using AW.UI.Web.Admin.Mvc.ViewModels.SalesOrder;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using AW.UI.Web.Admin.Mvc.Services;
using AW.UI.Web.Infrastructure.Api.SalesOrder.Handlers.GetSalesOrders;

namespace AW.UI.Web.Admin.Mvc.UnitTests.Controllers;

public class SalesOrderControllerUnitTests
{
    public class Index
    {
        [Theory, AutoMoqData]
        public async Task Index_ReturnsViewModel(
            [Frozen] Mock<ISalesOrderService> mockSalesOrderService,
            Mock<ISalesPersonViewModelService> mockSalesPersonViewModelService,
            Mock<IMediator> mockMediator,
            SalesOrderIndexViewModel viewModel
        )
        {
            //Arrange
            mockSalesOrderService.Setup(x => x.GetSalesOrders(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<CustomerType?>()
            ))
            .ReturnsAsync(viewModel);

            var sut = new SalesOrderController(
                mockMediator.Object,
                mockSalesOrderService.Object,
                mockSalesPersonViewModelService.Object
            );

            //Act
            var actionResult = await sut.Index(
                0, string.Empty, string.Empty
            );

            //Assert
            var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
            viewResult.Model.Should().BeEquivalentTo(viewModel);

            mockSalesOrderService.Verify(x => x.GetSalesOrders(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<CustomerType?>()
            ));
        }
    }

    public class Detail
    {
        [Theory, AutoMoqData]
        public async Task Detail_ReturnsViewModel(
            [Frozen] Mock<ISalesOrderService> mockSalesOrderService,
            Mock<ISalesPersonViewModelService> mockSalesPersonViewModelService,
            Mock<IMediator> mockMediator,
            SalesOrderDetailViewModel viewModel
        )
        {
            //Arrange
            mockSalesOrderService.Setup(x => x.GetSalesOrderDetail(
                It.IsAny<string>()
            ))
            .ReturnsAsync(viewModel);

            var sut = new SalesOrderController(
                mockMediator.Object,
                mockSalesOrderService.Object,
                mockSalesPersonViewModelService.Object
            );

            //Act
            var actionResult = await sut.Detail("SO43659");

            //Assert
            var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
            viewResult.Model.Should().BeEquivalentTo(viewModel);

            mockSalesOrderService.Verify(x => x.GetSalesOrderDetail(
                It.IsAny<string>()
            ));
        }
    }
}
