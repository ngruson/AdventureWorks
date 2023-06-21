﻿using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.Interfaces;

namespace AW.Services.Customer.Core.Handlers.GetCustomer
{
    public class StoreCustomer : Customer, IMapFrom<Entities.StoreCustomer>
    {
        private CustomerType _customerType = CustomerType.Store;
        public override CustomerType CustomerType 
        { 
            get 
            { 
                return _customerType; 
            } 
            set
            {
                _customerType = value;
            }

        }
        public string? Name { get; set; }
        public string? SalesPerson { get; set; }
        public List<StoreCustomerContact> Contacts { get; set; } = new List<StoreCustomerContact>();
    }
}
