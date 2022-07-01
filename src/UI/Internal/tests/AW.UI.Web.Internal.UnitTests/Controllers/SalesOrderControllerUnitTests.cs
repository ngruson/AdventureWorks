using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Internal.Controllers;
using AW.UI.Web.Internal.Interfaces;
using AW.UI.Web.Internal.ViewModels.SalesOrder;
using AW.UI.Web.SharedKernel.SalesOrder.Handlers.GetSalesOrders;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace AW.UI.Web.Internal.UnitTests.Controllers
{
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
                    0, null, null
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
                mockSalesOrderService.Setup(x => x.GetSalesOrder(
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

                mockSalesOrderService.Verify(x => x.GetSalesOrder(
                    It.IsAny<string>()
                ));
            }
        }
    }
}