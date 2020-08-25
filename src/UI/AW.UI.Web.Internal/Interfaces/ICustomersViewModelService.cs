﻿using AW.UI.Web.Internal.ViewModels;
using System.Threading.Tasks;

namespace AW.UI.Web.Internal.Interfaces
{
    public interface ICustomersViewModelService
    {
        Task<CustomersIndexViewModel> GetCustomers(int pageIndex, int pageSize, string territory, string customerType);
        Task<CustomerDetailViewModel> GetCustomer(string accountNumber);
    }
}