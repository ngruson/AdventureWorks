using AutoMapper;
using AW.Application.AutoMapper;
using AW.UI.Web.Internal.CustomerService;

namespace AW.UI.Web.Internal.ViewModels
{
    public class ContactInfoViewModel : IMapFrom<ContactInfo>
    {
        public ContactInfoChannelTypeViewModel ContactInfoChannelType { get; set; }
        public string ContactInfoType { get; set; }
        public string Value { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ContactInfo, ContactInfoViewModel>();
            profile.CreateMap<ContactInfo1, ContactInfoViewModel>();
        }
    }
}