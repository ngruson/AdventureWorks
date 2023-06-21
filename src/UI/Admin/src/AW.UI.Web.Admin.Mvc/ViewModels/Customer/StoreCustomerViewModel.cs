using AutoMapper;
using System.ComponentModel.DataAnnotations;
using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.Interfaces;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Customer;

public class StoreCustomerViewModel : CustomerViewModel, IMapFrom<Infrastructure.Api.Customer.Handlers.GetCustomers.StoreCustomer>
{
    public override CustomerType CustomerType => CustomerType.Store;
    public string? Name { get; set; }

    [Display(Name = "Sales person")]
    public string? SalesPerson { get; set; }
    public List<StoreCustomerContactViewModel>? Contacts { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Infrastructure.Api.Customer.Handlers.GetCustomers.StoreCustomer, StoreCustomerViewModel>();
        profile.CreateMap<Infrastructure.Api.Customer.Handlers.GetCustomer.StoreCustomer, StoreCustomerViewModel>();
        profile.CreateMap<Infrastructure.Api.Customer.Handlers.GetStoreCustomer.StoreCustomer, StoreCustomerViewModel>();
        profile.CreateMap<StoreCustomerViewModel, Infrastructure.Api.Customer.Handlers.UpdateCustomer.StoreCustomer>()
            .ForMember(m => m.Name, opt => opt.Ignore())
            .ReverseMap();
    }
}
