using AutoMapper;
using AW.SharedKernel.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Customer;

public class StoreCustomerContactViewModel : IMapFrom<Infrastructure.Api.Customer.Handlers.GetCustomers.StoreCustomerContact>
{
    public Guid ObjectId { get; set; }

    public string? CustomerName { get; set; }

    [Display(Name = "Contact type")]
    [Required]
    public string? ContactType { get; set; }
    public PersonViewModel ContactPerson { get; set; } = new PersonViewModel();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Infrastructure.Api.Customer.Handlers.GetCustomers.StoreCustomerContact, StoreCustomerContactViewModel>()
            .ForMember(_ => _.CustomerName, opt => opt.Ignore());
        profile.CreateMap<Infrastructure.Api.Customer.Handlers.GetCustomer.StoreCustomerContact, StoreCustomerContactViewModel>()
            .ForMember(_ => _.CustomerName, opt => opt.Ignore());
        profile.CreateMap<Infrastructure.Api.Customer.Handlers.GetStoreCustomer.StoreCustomerContact, StoreCustomerContactViewModel>()
            .ForMember(_ => _.CustomerName, opt => opt.Ignore());
        profile.CreateMap<StoreCustomerContactViewModel, Infrastructure.Api.Customer.Handlers.UpdateCustomer.StoreCustomerContact>()
            .ReverseMap()
            .ForMember(_ => _.CustomerName, opt => opt.Ignore());
    }
}
