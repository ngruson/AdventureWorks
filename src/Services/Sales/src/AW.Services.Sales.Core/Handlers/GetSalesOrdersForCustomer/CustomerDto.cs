using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Sales.Core.Handlers.GetSalesOrdersForCustomer
{
    public class CustomerDto : IMapFrom<Entities.Customer>
    {
        public CustomerType CustomerType { get; }
        public string CustomerNumber { get; set; }
        public string FullName { get; set; }
        public string Territory { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.Customer, CustomerDto>();
        }
    }    
}