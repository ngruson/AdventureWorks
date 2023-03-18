﻿using AW.UI.Web.Admin.Mvc.ViewModels.Product;

namespace AW.UI.Web.Admin.Mvc.Services
{
    public interface IProductService
    {
        Task<ProductIndexViewModel> GetProducts(int pageIndex, int pageSize);
        Task<ProductDetailViewModel> GetProductDetail(string productNumber);
        Task UpdateProduct(UpdateProductViewModel viewModel);
    }
}
