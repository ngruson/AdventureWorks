using AW.UI.Web.Infrastructure.Api.Interfaces;
using AW.UI.Web.Infrastructure.Api.Product.Handlers.GetProductCategories;
using AW.UI.Web.Infrastructure.Api.Product.Handlers.GetProducts;
using AW.UI.Web.Infrastructure.Api.Product.Handlers.GetUnitMeasures;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AW.UI.Web.Infrastructure.Api.ApiClients;

public class ProductApiClient : IProductApiClient
{
    private readonly HttpClient _client;
    private readonly ILogger<ProductApiClient?> _logger;

    public ProductApiClient(HttpClient client, ILogger<ProductApiClient?> logger) =>
        (_client, _logger) = (client, logger);

    public async Task<List<ProductCategory>?> GetCategories()
    {
        var requestUri = "/product-api/ProductCategory?api-version=1.0";

        _logger.LogInformation("Getting product categories");

        using var response = await _client.GetAsync(requestUri);
        response.EnsureSuccessStatusCode();
        var stream = await response.Content.ReadAsStreamAsync();

        return await stream.DeserializeAsync<List<ProductCategory>?>(
            new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter()
                },
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }
        );
    }

    public async Task<GetProductsResult?> GetProducts(
        string? category,
        string? subcategory,
        string? orderBy
    )
    {
        var requestUri = $"/product-api/Product?api-version=1.0";
        _logger.LogInformation("Getting products from API");

        using var response = await _client.GetAsync(requestUri);
        response.EnsureSuccessStatusCode();
        var stream = await response.Content.ReadAsStreamAsync();

        return await stream.DeserializeAsync<GetProductsResult?>(
            new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter()
                },
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }
        );
    }

    public async Task<Product.Handlers.GetProduct.Product?> GetProduct(string? productNumber)
    {
        var requestUri = $"/product-api/Product/{productNumber}?api-version=1.0";
        _logger.LogInformation("Getting product with product number {ProductNumber}", productNumber);

        using var response = await _client.GetAsync(requestUri);
        response.EnsureSuccessStatusCode();
        var stream = await response.Content.ReadAsStreamAsync();

        return await stream.DeserializeAsync<Product.Handlers.GetProduct.Product?>(
            new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter()
                },
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }
        );
    }

    public async Task<Product.Handlers.CreateProduct.Product?> CreateProduct(Product.Handlers.CreateProduct.Product product)
    {
        _logger.LogInformation("Call Product API to create product");
        var requestUri = $"Product?&api-version=1.0";
        var options = new JsonSerializerOptions
        {
            Converters =
            {
                new JsonStringEnumConverter()
            },
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var json = JsonSerializer.Serialize(product, options);
        _logger.LogInformation("Calling POST method on {RequestUri}", requestUri);

        using var response = await _client.PostAsync(
            requestUri,
            new StringContent(json, Encoding.UTF8, "application/json")
        );
        response.EnsureSuccessStatusCode();
        var stream = await response.Content.ReadAsStreamAsync();
        var createdProduct = await stream.DeserializeAsync<Product.Handlers.CreateProduct.Product?>(options);

        _logger.LogInformation("Returning product");
        return createdProduct;
    }

    public async Task<Product.Handlers.UpdateProduct.Product?> UpdateProduct(string key, Product.Handlers.UpdateProduct.Product product)
    {
        _logger.LogInformation("Call Product API to update product");
        var requestUri = $"Product/{key}?&api-version=1.0";
        var options = new JsonSerializerOptions
        {
            Converters =
            {
                new JsonStringEnumConverter()
            },
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var json = JsonSerializer.Serialize(product, options);
        _logger.LogInformation("Calling PUT method on {RequestUri} with {JSON}", requestUri, json);

        using var response = await _client.PutAsync(
            requestUri,
            new StringContent(json, Encoding.UTF8, "application/json")
        );
        response.EnsureSuccessStatusCode();
        var stream = await response.Content.ReadAsStreamAsync();
        var updatedProduct = await stream.DeserializeAsync<Product.Handlers.UpdateProduct.Product?>(options);

        _logger.LogInformation("Returning product {@Product}", updatedProduct);
        return updatedProduct;
    }

    public async Task DeleteProduct(string productNumber)
    {
        _logger.LogInformation("Deleting product");
        var requestUri = $"Product/{productNumber}?&api-version=1.0";
        _logger.LogInformation("Calling DELETE method on {RequestUri}", requestUri);

        using var response = await _client.DeleteAsync(requestUri);
        response.EnsureSuccessStatusCode();

        _logger.LogInformation("Product succesfully deleted");
    }

    public async Task<List<Product.Handlers.GetProductModels.ProductModel>?> GetProductModels()
    {
        var requestUri = $"/product-api/ProductModel?api-version=1.0";
        _logger.LogInformation("Getting product models");

        using var response = await _client.GetAsync(requestUri);
        response.EnsureSuccessStatusCode();
        var stream = await response.Content.ReadAsStreamAsync();

        return await stream.DeserializeAsync<List<Product.Handlers.GetProductModels.ProductModel>>(
            new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter()
                },
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }
        );
    }

    public async Task<Product.Handlers.GetProductModel.ProductModel?> GetProductModel(string name)
    {
        var requestUri = $"/product-api/ProductModel/{name}?api-version=1.0";
        _logger.LogInformation("Getting product model");

        using var response = await _client.GetAsync(requestUri);
        response.EnsureSuccessStatusCode();
        var stream = await response.Content.ReadAsStreamAsync();

        return await stream.DeserializeAsync<Product.Handlers.GetProductModel.ProductModel>(
            new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter()
                },
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }
        );
    }

    public async Task<List<UnitMeasure>?> GetUnitMeasures()
    {
        var requestUri = $"/product-api/UnitMeasure?api-version=1.0";
        _logger.LogInformation("Getting unit measures");

        using var response = await _client.GetAsync(requestUri);
        response.EnsureSuccessStatusCode();
        var stream = await response.Content.ReadAsStreamAsync();

        return await stream.DeserializeAsync<List<UnitMeasure>>(
            new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter()
                },
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }
        );
    }

    public async Task<Product.Handlers.DuplicateProduct.Product> DuplicateProduct(string productNumber)
    {
        var requestUri = $"Product/{productNumber}/duplicate?&api-version=1.0";
        var options = new JsonSerializerOptions
        {
            Converters =
            {
                new JsonStringEnumConverter()
            },
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        _logger.LogInformation("Calling POST method on {RequestUri}}", requestUri);

        using var response = await _client.PostAsync(
            requestUri,
            null
        );
        response.EnsureSuccessStatusCode();

        var stream = await response.Content.ReadAsStreamAsync();
        var product = await stream.DeserializeAsync<Product.Handlers.DuplicateProduct.Product?>(options);

        _logger.LogInformation("Returning product {@Product}", product);
        return product!;
    }
}
