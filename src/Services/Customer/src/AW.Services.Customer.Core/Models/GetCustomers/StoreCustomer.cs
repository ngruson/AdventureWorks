using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.Services.Customer.Core.Handlers.GetCustomers;
using System.Collections.Generic;

namespace AW.Services.Customer.Core.Models.GetCustomers
{
    public class StoreCustomer : Customer, IMapFrom<StoreCustomerDto>
    {
        public string Name { get; set; }
        public string SalesPerson { get; set; }

        public List<StoreCustomerContact> Contacts { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<StoreCustomerDto, StoreCustomer>();
        }
    }
}