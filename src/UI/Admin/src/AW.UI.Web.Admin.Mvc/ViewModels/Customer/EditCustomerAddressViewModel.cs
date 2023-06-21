using AutoMapper;
using AW.SharedKernel.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Customer;

public class EditCustomerAddressViewModel : IMapFrom<Infrastructure.Api.Customer.Handlers.UpdateCustomer.CustomerAddress>
{
    public Guid CustomerId { get; set; }
    public Guid ObjectId { get; set; }

    [Display(Name = "Address type")]
    [Required]
    public string? AddressType { get; set; }
    public AddressViewModel Address { get; set; } = new AddressViewModel();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditCustomerAddressViewModel, Infrastructure.Api.Customer.Handlers.UpdateCustomer.CustomerAddress>();
    }
}
