using MediatR;
using System.Collections.Generic;

namespace AW.Application.GetSalesTerritories
{
    public class GetSalesTerritoriesQuery : IRequest<IEnumerable<TerritoryDto>>
    {
    }
}