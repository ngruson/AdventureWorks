﻿using FluentValidation;

namespace AW.Application.Product.GetProducts
{
    public class GetProductsQueryValidator : AbstractValidator<GetProductsQuery>
    {
        public GetProductsQueryValidator()
        {
            RuleFor(cmd => cmd.PageIndex)
                .GreaterThan(-1).WithMessage("Page index must be 0 or positive");

            RuleFor(cmd => cmd.PageSize)
                .GreaterThan(0).WithMessage("Page size must be positive");
        }
    }
}