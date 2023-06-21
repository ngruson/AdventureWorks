using AutoMapper;
using AW.SharedKernel.AutoMapper;
using UpdateCustomer = AW.UI.Web.Infrastructure.Api.Customer.Handlers.UpdateCustomer;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Customer;

public class EditStoreCustomerContactPersonPhoneViewModel : IMapFrom<UpdateCustomer.PersonPhone>
{
    public string? PhoneNumberType { get; set; }
    public string? PhoneNumber { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditStoreCustomerContactPersonPhoneViewModel, UpdateCustomer.PersonPhone>()
            .ForMember(_ => _.ObjectId, opt => opt.Ignore());
    }
}
