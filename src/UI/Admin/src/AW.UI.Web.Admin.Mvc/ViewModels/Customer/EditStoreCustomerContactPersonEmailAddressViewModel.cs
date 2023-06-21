using AutoMapper;
using AutoMapper.EquivalencyExpression;
using AW.SharedKernel.AutoMapper;
using UpdateCustomer = AW.UI.Web.Infrastructure.Api.Customer.Handlers.UpdateCustomer;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Customer;

public class EditStoreCustomerContactPersonEmailAddressViewModel : IMapFrom<UpdateCustomer.PersonEmailAddress>
{
    public string? EmailAddress { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditStoreCustomerContactPersonEmailAddressViewModel, UpdateCustomer.PersonEmailAddress>()
            .ForMember(_ => _.ObjectId, opt => opt.Ignore())
            .EqualityComparison((src, dest) => src.EmailAddress == dest.EmailAddress);
    }
}
