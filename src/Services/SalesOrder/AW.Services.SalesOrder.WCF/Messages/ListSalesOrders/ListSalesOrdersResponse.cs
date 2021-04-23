using AutoMapper;
using AW.Services.SalesOrder.Application.Common;
using AW.Services.SalesOrder.Application.GetSalesOrders;
using System.Xml.Serialization;

namespace AW.Services.SalesOrder.WCF.Messages.ListSalesOrders
{
    [XmlType(AnonymousType = true, Namespace = "http://services.aw.com/SalesOrderService/1.0")]
    [XmlRoot(Namespace = "http://services.aw.com/SalesOrderService/1.0", IsNullable = false)]
    public class ListSalesOrdersResponse : IMapFrom<GetSalesOrdersDto>
    {
        public int TotalSalesOrders { get; set; }
        public ListSalesOrders SalesOrders { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetSalesOrdersDto, ListSalesOrdersResponse>();
        }
    }
}