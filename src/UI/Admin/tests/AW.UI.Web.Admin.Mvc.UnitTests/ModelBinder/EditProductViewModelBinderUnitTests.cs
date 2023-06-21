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

public class EditProductViewModelBinderUnitTests
{
    [Theory, AutoMoqData]
    public async Task BindModelGivenFormData(
        EditProductViewModelBinder sut,
        EditProductViewModel viewModel)
    {
        //Arrange
        var formData = new FormCollection(
            new Dictionary<string, StringValues>
            {
                { "Product.MakeFlag", viewModel.Product!.MakeFlag.ToString() },
                { "Product.FinishedGoods", viewModel.Product!.FinishedGoodsFlag.ToString() },
                { "Product.DaysToManufacture", viewModel.Product!.DaysToManufacture.ToString() },
                { "Product.SafetyStockLevel", viewModel.Product!.SafetyStockLevel.ToString() },
                { "Product.ReorderPoint", viewModel.Product!.ReorderPoint.ToString() }
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
                ModelMetadataIdentity.ForType(typeof(EditProductViewModel))
            )
        };

        //Act
        await sut.BindModelAsync(bindingContext);

        //Assert
        requestMock.Verify(_ => _.Form);
        contextMock.Verify(_ => _.Request);
    }

    [Theory, AutoMoqData]
    public async Task ThrowExceptionGivenMakeFlagCouldNotBeParsed(
        EditProductViewModelBinder sut,
        EditProductViewModel viewModel,
        string makeFlag)
    {
        //Arrange
        var formData = new FormCollection(
            new Dictionary<string, StringValues>
            {
                { "Product.MakeFlag", makeFlag },
                { "Product.FinishedGoodsFlag", viewModel.Product!.FinishedGoodsFlag.ToString() },
                { "Product.DaysToManufacture", viewModel.Product!.DaysToManufacture.ToString() },
                { "Product.SafetyStockLevel", viewModel.Product!.SafetyStockLevel.ToString() },
                { "Product.ReorderPoint", viewModel.Product!.ReorderPoint.ToString() }
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
                ModelMetadataIdentity.ForType(typeof(EditProductViewModel))
            )
        };

        //Act
        Func<Task> func = async () => await sut.BindModelAsync(bindingContext);

        //Assert
        await func.Should().ThrowAsync<ArgumentException>()
            .WithMessage($"MakeFlag value {makeFlag} could not be parsed");
    }

    [Theory, AutoMoqData]
    public async Task ThrowExceptionGivenFinishedGoodsFlagCouldNotBeParsed(
        EditProductViewModelBinder sut,
        EditProductViewModel viewModel,
        string finishedGoodsFlag)
    {
        //Arrange
        var formData = new FormCollection(
            new Dictionary<string, StringValues>
            {
                { "Product.MakeFlag", viewModel.Product!.MakeFlag.ToString() },
                { "Product.FinishedGoodsFlag", finishedGoodsFlag },
                { "Product.DaysToManufacture", viewModel.Product!.DaysToManufacture.ToString() },
                { "Product.SafetyStockLevel", viewModel.Product!.SafetyStockLevel.ToString() },
                { "Product.ReorderPoint", viewModel.Product!.ReorderPoint.ToString() }
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
                ModelMetadataIdentity.ForType(typeof(EditProductViewModel))
            )
        };

        //Act
        Func<Task> func = async () => await sut.BindModelAsync(bindingContext);

        //Assert
        await func.Should().ThrowAsync<ArgumentException>()
            .WithMessage($"FinishedGoodsFlag value {finishedGoodsFlag} could not be parsed");
    }

    [Theory, AutoMoqData]
    public async Task ThrowExceptionGivenDaysToManufactureCouldNotBeParsed(
        EditProductViewModelBinder sut,
        EditProductViewModel viewModel,
        string daysToManufacture)
    {
        //Arrange
        var formData = new FormCollection(
            new Dictionary<string, StringValues>
            {
                { "Product.MakeFlag", viewModel.Product!.MakeFlag.ToString() },
                { "Product.FinishedGoodsFlag", viewModel.Product!.FinishedGoodsFlag.ToString() },
                { "Product.DaysToManufacture", daysToManufacture },
                { "Product.SafetyStockLevel", viewModel.Product!.SafetyStockLevel.ToString() },
                { "Product.ReorderPoint", viewModel.Product!.ReorderPoint.ToString() }
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
                ModelMetadataIdentity.ForType(typeof(EditProductViewModel))
            )
        };

        //Act
        Func<Task> func = async () => await sut.BindModelAsync(bindingContext);

        //Assert
        await func.Should().ThrowAsync<ArgumentException>()
            .WithMessage($"DaysToManufacture value {daysToManufacture} could not be parsed");
    }

    [Theory, AutoMoqData]
    public async Task ThrowExceptionGivenSafetyStockLevelCouldNotBeParsed(
        EditProductViewModelBinder sut,
        EditProductViewModel viewModel,
        string safetyStockLevel)
    {
        //Arrange
        var formData = new FormCollection(
            new Dictionary<string, StringValues>
            {
                { "Product.MakeFlag", viewModel.Product!.MakeFlag.ToString() },
                { "Product.FinishedGoodsFlag", viewModel.Product!.FinishedGoodsFlag.ToString() },
                { "Product.DaysToManufacture", viewModel.Product!.DaysToManufacture.ToString() },
                { "Product.SafetyStockLevel", safetyStockLevel },
                { "Product.ReorderPoint", viewModel.Product!.ReorderPoint.ToString() }
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
                ModelMetadataIdentity.ForType(typeof(EditProductViewModel))
            )
        };

        //Act
        Func<Task> func = async () => await sut.BindModelAsync(bindingContext);

        //Assert
        await func.Should().ThrowAsync<ArgumentException>()
            .WithMessage($"SafetyStockLevel value {safetyStockLevel} could not be parsed");
    }

    [Theory, AutoMoqData]
    public async Task ThrowExceptionGivenReorderPointCouldNotBeParsed(
        EditProductViewModelBinder sut,
        EditProductViewModel viewModel,
        string reorderPoint)
    {
        //Arrange
        var formData = new FormCollection(
            new Dictionary<string, StringValues>
            {
                { "Product.MakeFlag", viewModel.Product!.MakeFlag.ToString() },
                { "Product.FinishedGoodsFlag", viewModel.Product!.FinishedGoodsFlag.ToString() },
                { "Product.DaysToManufacture", viewModel.Product!.DaysToManufacture.ToString() },
                { "Product.SafetyStockLevel", viewModel.Product!.SafetyStockLevel.ToString() },
                { "Product.ReorderPoint", reorderPoint }
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
                ModelMetadataIdentity.ForType(typeof(EditProductViewModel))
            )
        };

        //Act
        Func<Task> func = async () => await sut.BindModelAsync(bindingContext);

        //Assert
        await func.Should().ThrowAsync<ArgumentException>()
            .WithMessage($"ReorderPoint value {reorderPoint} could not be parsed");
    }
}
