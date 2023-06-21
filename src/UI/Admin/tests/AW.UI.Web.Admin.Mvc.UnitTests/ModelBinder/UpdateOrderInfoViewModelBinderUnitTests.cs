using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Admin.Mvc.ViewModels.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Moq;
using Xunit;
using AW.UI.Web.Admin.Mvc.ViewModels.SalesOrder.ModelBinders;
using AW.UI.Web.Admin.Mvc.ViewModels.SalesOrder;
using FluentAssertions;

namespace AW.UI.Web.Admin.Mvc.UnitTests.ModelBinder;

public class UpdateOrderInfoViewModelBinderUnitTests
{
    [Theory, AutoMoqData]
    public async Task BindModelGivenValidFormData(
        UpdateOrderInfoViewModelBinder sut,
        UpdateOrderInfoViewModel viewModel)
    {
        //Arrange
        viewModel.SalesOrder!.DueDate = viewModel.SalesOrder.DueDate.Date;
        viewModel.SalesOrder!.ShipDate = viewModel.SalesOrder.ShipDate?.Date;

        var formData = new FormCollection(
            new Dictionary<string, StringValues>
            {
                { "SalesOrder.SalesOrderNumber", viewModel.SalesOrder!.SalesOrderNumber },
                { "SalesOrder.RevisionNumber", viewModel.SalesOrder!.RevisionNumber },
                { "SalesOrder.OnlineOrderFlag", viewModel.SalesOrder!.OnlineOrderFlag.ToString() },
                { "SalesOrder.DueDate", viewModel.SalesOrder!.DueDate.ToString() },
                { "SalesOrder.ShipDate", viewModel.SalesOrder!.ShipDate?.ToShortDateString() },
                { "SalesOrder.PurchaseOrderNumber", viewModel.SalesOrder!.PurchaseOrderNumber },
                { "SalesOrder.AccountNumber", viewModel.SalesOrder!.AccountNumber },
                { "SalesOrder.ShipMethod", viewModel.SalesOrder!.ShipMethod },
                { "SalesOrder.Territory", viewModel.SalesOrder!.Territory },
                { "SalesOrder.SalesPerson", viewModel.SalesOrder!.SalesPerson },
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
                ModelMetadataIdentity.ForType(typeof(EditPricingViewModel))
            )
        };

        //Act
        await sut.BindModelAsync(bindingContext);

        //Assert
        bindingContext.Result.Model.Should().BeEquivalentTo(viewModel);
    }
}
