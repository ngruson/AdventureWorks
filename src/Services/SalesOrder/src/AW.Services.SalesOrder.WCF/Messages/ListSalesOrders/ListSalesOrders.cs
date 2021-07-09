using AutoMapper;
using AW.Services.SalesOrder.Core.Handlers.GetSalesOrders;
using AW.SharedKernel.AutoMapper;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AW.Services.SalesOrder.WCF.Messages.ListSalesOrders
{
    public class ListSalesOrders : IMapFrom<IEnumerable<SalesOrderDto>>
    {
        [XmlElement(Namespace = "http://services.aw.com/SalesOrderService/1.0/ListSalesOrders")]
        public List<SalesOrder> SalesOrder { get; set; } = new List<SalesOrder>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<IEnumerable<SalesOrderDto>, ListSalesOrders>()
                .ForMember(m => m.SalesOrder, opt => opt.MapFrom(src => src));
        }
    }
}