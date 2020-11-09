using MediatR;
using System.Collections.Generic;

namespace AW.Application.SalesTerritory.GetSalesTerritories
{
    public class GetSalesTerritoriesQuery : IRequest<IEnumerable<TerritoryDto>>
    {
    }
}