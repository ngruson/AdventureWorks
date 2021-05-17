using AutoMapper;
using AW.Common.AutoMapper;
using AW.Common.Interfaces;
using System.Collections.Generic;
using m = AW.UI.Web.Common.ApiClients.SalesPersonApi.Models;

namespace AW.UI.Web.Internal.ViewModels.SalesPerson
{
    public class SalesPersonViewModel : IPerson, IMapFrom<m.SalesPerson>
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string Territory { get; set; }
        public List<SalesPersonEmailAddress> EmailAddresses { get; set; }
        public List<SalesPersonPhone> PhoneNumbers { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<m.SalesPerson, SalesPersonViewModel>();
        }
    }
}