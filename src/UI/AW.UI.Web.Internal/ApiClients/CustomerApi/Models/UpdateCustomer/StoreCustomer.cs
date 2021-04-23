using AW.UI.Web.Internal.Common;
using System.Collections.Generic;

namespace AW.UI.Web.Internal.ApiClients.CustomerApi.Models.UpdateCustomer
{
    public class StoreCustomer : Customer, IMapFrom<GetCustomer.StoreCustomer>
    {
        public override CustomerType CustomerType => CustomerType.Store;
        public string Name { get; set; }
        public string SalesPerson { get; set; }
        public List<StoreCustomerContact> Contacts { get; set; }
    }
}