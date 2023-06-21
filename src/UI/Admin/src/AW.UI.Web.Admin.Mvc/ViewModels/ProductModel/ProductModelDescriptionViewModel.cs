using AW.SharedKernel.AutoMapper;
using AW.UI.Web.Infrastructure.Api.Product.Handlers.GetProductModel;

namespace AW.UI.Web.Admin.Mvc.ViewModels.ProductModel;

public class ProductModelDescriptionViewModel : IMapFrom<ProductModelDescription>
{
    public string? CultureName { get; set; }
    public string? Description { get; set; }
}
