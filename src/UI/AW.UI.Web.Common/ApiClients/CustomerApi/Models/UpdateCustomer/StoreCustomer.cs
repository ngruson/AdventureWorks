using AW.Common.AutoMapper;
using System.Collections.Generic;

namespace AW.UI.Web.Common.ApiClients.CustomerApi.Models.UpdateCustomer
{
    public class StoreCustomer : Customer, IMapFrom<GetCustomer.StoreCustomer>
    {
        public override CustomerType CustomerType => CustomerType.Store;
        public string Name { get; set; }
        public string SalesPerson { get; set; }
        public List<StoreCustomerContact> Contacts { get; set; }
    }
}