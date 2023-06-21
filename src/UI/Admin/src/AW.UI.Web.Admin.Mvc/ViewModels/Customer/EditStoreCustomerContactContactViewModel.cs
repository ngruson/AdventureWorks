using AutoMapper;
using AW.SharedKernel.AutoMapper;
using UpdateCustomer = AW.UI.Web.Infrastructure.Api.Customer.Handlers.UpdateCustomer;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Customer;

public class EditStoreCustomerContactContactViewModel : IMapFrom<UpdateCustomer.StoreCustomerContact>
{
    public Guid ObjectId { get; set; }
    public string? ContactType { get; set; }
    public EditStoreCustomerContactPersonViewModel ContactPerson { get; set; } = new EditStoreCustomerContactPersonViewModel();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditStoreCustomerContactContactViewModel, UpdateCustomer.StoreCustomerContact>();
    }
}
