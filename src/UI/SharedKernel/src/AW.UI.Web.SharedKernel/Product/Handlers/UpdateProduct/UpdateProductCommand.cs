﻿using MediatR;

namespace AW.UI.Web.SharedKernel.Product.Handlers.UpdateProduct
{
    public class UpdateProductCommand : IRequest
    {
        public UpdateProductCommand(string key, Product product)
        {
            Key = key;
            Product = product;
        }

        public string Key { get; set; }
        public Product Product { get; set; }
    }
}
