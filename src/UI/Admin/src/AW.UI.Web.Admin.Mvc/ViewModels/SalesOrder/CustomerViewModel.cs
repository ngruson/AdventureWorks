﻿using AutoMapper;
using AW.SharedKernel.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Admin.Mvc.ViewModels.SalesOrder;

public class CustomerViewModel : IMapFrom<Infrastructure.Api.SalesOrder.Handlers.GetSalesOrders.Customer>
{
    public AW.SharedKernel.Interfaces.CustomerType CustomerType { get; set; }

    [Display(Name = "Customer number")]
    public string? CustomerNumber { get; set; }

    [Display(Name = "Customer name")]
    public string? CustomerName { get; set;  }
    public int SalesOrderCount { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Infrastructure.Api.SalesOrder.Handlers.GetSalesOrders.Customer, CustomerViewModel>();
        profile.CreateMap<Infrastructure.Api.SalesOrder.Handlers.GetSalesOrder.Customer, CustomerViewModel>();
    }
}
