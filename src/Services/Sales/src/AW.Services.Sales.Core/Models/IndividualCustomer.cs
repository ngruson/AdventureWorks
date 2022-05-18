using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.ValueTypes;

namespace AW.Services.Sales.Core.Models
{
    public class IndividualCustomer : Customer, IMapFrom<Handlers.GetSalesOrders.IndividualCustomerDto>
    {
        public string Title { get; set; }
        public NameFactory Name { get; set; }
        public string Suffix { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Handlers.GetSalesOrders.IndividualCustomerDto, IndividualCustomer>();
            profile.CreateMap<Handlers.GetSalesOrdersForCustomer.IndividualCustomerDto, IndividualCustomer>();
            profile.CreateMap<Handlers.GetSalesOrder.IndividualCustomerDto, IndividualCustomer>();
            profile.CreateMap<IndividualCustomer, Handlers.UpdateSalesOrder.IndividualCustomerDto>()
                .ReverseMap();
        }
    }
}