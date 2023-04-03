﻿using MediatR;

namespace AW.Services.Product.Core.Handlers.CreateProduct
{
    public class CreateProductCommand : IRequest<Product>
    {
        public CreateProductCommand(Product product)
        {
            Product = product;
        }
        public Product Product { get; set; }
    }
}
