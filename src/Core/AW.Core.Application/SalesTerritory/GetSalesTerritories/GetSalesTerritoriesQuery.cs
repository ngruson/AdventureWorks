using MediatR;
using System.Collections.Generic;

namespace AW.Core.Application.SalesTerritory.GetSalesTerritories
{
    public class GetSalesTerritoriesQuery : IRequest<IEnumerable<TerritoryDto>>
    {
    }
}