using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Admin.Mvc.ViewModels.Product.ModelBinders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using AW.UI.Web.Admin.Mvc.ViewModels.Product;
using Microsoft.Extensions.Primitives;
using FluentAssertions;

namespace AW.UI.Web.Admin.Mvc.UnitTests.ModelBinder;

public class EditPricingViewModelBinderUnitTests
{
    [Theory, AutoMoqData]
    public async Task BindModelGivenValidFormData(
        EditPricingViewModelBinder sut,
        EditPricingViewModel viewModel)
    {
        //Arrange
        var formData = new FormCollection(
            new Dictionary<string, StringValues>
            {
                { "Product.StandardCost", viewModel.Product!.StandardCost.ToString() },
                { "Product.ListPrice", viewModel.Product!.ListPrice.ToString() }
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
        requestMock.Verify(_ => _.Form);
        contextMock.Verify(_ => _.Request);
    }

    [Theory, AutoMoqData]
    public async Task ThrowExceptionGivenStandardCostCouldNotBeParsed(
        EditPricingViewModelBinder sut,
        EditPricingViewModel viewModel,
        string standardCost)
    {
        //Arrange
        var formData = new FormCollection(
            new Dictionary<string, StringValues>
            {
                { "Product.StandardCost", standardCost },
                { "Product.ListPrice", viewModel.Product!.ListPrice.ToString() }
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
        Func<Task> func = async () => await sut.BindModelAsync(bindingContext);

        //Assert
        await func.Should().ThrowAsync<ArgumentException>()
            .WithMessage($"Standard cost value {standardCost} could not be parsed");
    }

    [Theory, AutoMoqData]
    public async Task ThrowExceptionGivenListPriceCouldNotBeParsed(
        EditPricingViewModelBinder sut,
        EditPricingViewModel viewModel,
        string listPrice)
    {
        //Arrange
        var formData = new FormCollection(
            new Dictionary<string, StringValues>
            {
                { "Product.StandardCost", viewModel.Product!.StandardCost.ToString() },
                { "Product.ListPrice", listPrice }
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
        Func<Task> func = async () => await sut.BindModelAsync(bindingContext);

        //Assert
        await func.Should().ThrowAsync<ArgumentException>()
            .WithMessage($"List price value {listPrice} could not be parsed");
    }
}
