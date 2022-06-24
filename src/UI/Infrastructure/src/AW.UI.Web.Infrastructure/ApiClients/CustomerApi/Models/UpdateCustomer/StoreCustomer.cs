using AW.SharedKernel.AutoMapper;
using System.Collections.Generic;

namespace AW.UI.Web.Infrastructure.ApiClients.CustomerApi.Models.UpdateCustomer
{
    public class StoreCustomer : Customer, IMapFrom<GetCustomer.StoreCustomer>
    {
        public string Name { get; set; }
        public string SalesPerson { get; set; }
        public List<StoreCustomerContact> Contacts { get; set; }
    }
}