using AutoMapper;
using AW.SharedKernel.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Customer;

public class CustomerAddressViewModel : IMapFrom<Infrastructure.Api.Customer.Handlers.GetCustomers.CustomerAddress>
{
    public Guid ObjectId { get; set; }

    [Display(Name = "Address type")]
    [Required]
    public string? AddressType { get; set; }
    public AddressViewModel Address { get; set; } = new AddressViewModel();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Infrastructure.Api.Customer.Handlers.GetCustomers.CustomerAddress, CustomerAddressViewModel>();
        profile.CreateMap<Infrastructure.Api.Customer.Handlers.GetCustomer.CustomerAddress, CustomerAddressViewModel>();
        profile.CreateMap<Infrastructure.Api.Customer.Handlers.GetIndividualCustomer.CustomerAddress, CustomerAddressViewModel>()
            .ReverseMap();
        profile.CreateMap<Infrastructure.Api.Customer.Handlers.GetStoreCustomer.CustomerAddress, CustomerAddressViewModel>();
        profile.CreateMap<CustomerAddressViewModel, Infrastructure.Api.Customer.Handlers.UpdateCustomer.CustomerAddress>()
            .ReverseMap();
    }
}
