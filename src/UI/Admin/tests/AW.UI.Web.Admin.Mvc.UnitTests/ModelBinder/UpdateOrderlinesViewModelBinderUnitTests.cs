using AW.SharedKernel.UnitTesting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using AW.UI.Web.Admin.Mvc.ViewModels.SalesOrder.ModelBinders;
using AW.UI.Web.Admin.Mvc.ViewModels.SalesOrder;

namespace AW.UI.Web.Admin.Mvc.UnitTests.ModelBinder
{
    public class UpdateOrderlinesViewModelBinderUnitTests
    {
        [Theory, AutoMoqData]
        public async Task BindModel_Should_ReturnViewModel(
            UpdateOrderlinesViewModelBinder sut,
            string salesOrderNumber,
            List<string> productNumbers,
            List<string> quantities
        )
        {
            //Arrange
            var formData = new FormCollection(
                new Dictionary<string, StringValues>
                {
                    { "SalesOrder.SalesOrderNumber", salesOrderNumber },
                    { "SalesOrder.OrderLines[0].ProductNumber", productNumbers[0] },
                    { "SalesOrder.OrderLines[0].OrderQty", quantities[0] },
                    { "SalesOrder.OrderLines[1].ProductNumber", productNumbers[1] },
                    { "SalesOrder.OrderLines[1].OrderQty", quantities[1] },
                    { "SalesOrder.OrderLines[2].ProductNumber", productNumbers[2] },
                    { "SalesOrder.OrderLines[2].OrderQty", quantities[2] }
                }
            );

            var requestMock = new Mock<HttpRequest>();
            requestMock.SetupGet(x => x.Form).Returns(formData);

            var contextMock = new Mock<HttpContext>();
            contextMock.SetupGet(x => x.Request).Returns(requestMock.Object);

            var bindingContext = new DefaultModelBindingContext
            {
                ActionContext = new ActionContext
                {
                    HttpContext = contextMock.Object
                },
                ModelMetadata = new TestModelMetadata(
                    ModelMetadataIdentity.ForType(typeof(UpdateOrderlinesViewModel))
                )
            };

            //Act
            await sut.BindModelAsync(bindingContext);

            //Assert

            var expected = new UpdateOrderlinesViewModel
            {
                SalesOrder = new UpdateOrderlinesSalesOrderViewModel
                {
                    SalesOrderNumber = salesOrderNumber,
                    OrderLines = new List<UpdateOrderlinesSalesOrderlineViewModel>
                    {
                        new UpdateOrderlinesSalesOrderlineViewModel
                        {
                            ProductNumber = productNumbers[0],
                            OrderQty = quantities[0]
                        },
                        new UpdateOrderlinesSalesOrderlineViewModel
                        {
                            ProductNumber = productNumbers[1],
                            OrderQty = quantities[1]
                        },
                        new UpdateOrderlinesSalesOrderlineViewModel
                        {
                            ProductNumber = productNumbers[2],
                            OrderQty = quantities[2]
                        }
                    }
                }
            };

            bindingContext.Result.Model.Should().BeEquivalentTo(expected);
        }
    }
}