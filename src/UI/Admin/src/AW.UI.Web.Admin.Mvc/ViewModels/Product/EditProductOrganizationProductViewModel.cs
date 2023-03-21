﻿using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Product
{
    public class EditProductOrganizationProductViewModel : IMapFrom<SharedKernel.Product.Handlers.UpdateProduct.Product>
    {
        public string? ProductNumber { get; set; }
        public string? ProductCategoryName { get; set; }
        public string? ProductSubcategoryName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EditProductOrganizationProductViewModel, SharedKernel.Product.Handlers.UpdateProduct.Product>()
                .ForMember(m => m.Name, opt => opt.Ignore())
                .ForMember(m => m.MakeFlag, opt => opt.Ignore())
                .ForMember(m => m.FinishedGoodsFlag, opt => opt.Ignore())
                .ForMember(m => m.Color, opt => opt.Ignore())
                .ForMember(m => m.SafetyStockLevel, opt => opt.Ignore())
                .ForMember(m => m.ReorderPoint, opt => opt.Ignore())
                .ForMember(m => m.Size, opt => opt.Ignore())
                .ForMember(m => m.SizeUnitMeasureCode, opt => opt.Ignore())
                .ForMember(m => m.Weight, opt => opt.Ignore())
                .ForMember(m => m.WeightUnitMeasureCode, opt => opt.Ignore())
                .ForMember(m => m.DaysToManufacture, opt => opt.Ignore())
                .ForMember(m => m.ProductLine, opt => opt.Ignore())
                .ForMember(m => m.Class, opt => opt.Ignore())
                .ForMember(m => m.Style, opt => opt.Ignore())
                .ForMember(m => m.SellStartDate, opt => opt.Ignore())
                .ForMember(m => m.SellEndDate, opt => opt.Ignore())
                .ForMember(m => m.DiscontinuedDate, opt => opt.Ignore())
                .ForMember(m => m.ProductModelName, opt => opt.Ignore())
                .ForMember(m => m.StandardCost, opt => opt.Ignore())
                .ForMember(m => m.ListPrice, opt => opt.Ignore());
        }
    }
}