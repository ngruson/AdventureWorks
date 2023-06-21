using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.ValueTypes;
using UpdateCustomer = AW.UI.Web.Infrastructure.Api.Customer.Handlers.UpdateCustomer;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Customer;

public class EditStoreCustomerContactPersonViewModel : IMapFrom<UpdateCustomer.Person>
{
    public string? Title { get; set; }
    public NameFactory? Name { get; set; }

    public string? Suffix { get; set; }

    public List<EditStoreCustomerContactPersonEmailAddressViewModel> EmailAddresses { get; set; } = new();
    public List<EditStoreCustomerContactPersonPhoneViewModel> PhoneNumbers { get; set; } = new();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditStoreCustomerContactPersonViewModel, UpdateCustomer.Person>()
            .ForMember(_ => _.ObjectId, opt => opt.Ignore());
    }
}
