using System.Collections.Generic;

namespace AW.Core.Abstractions.Api.SalesTerritoryApi.ListTerritories
{
    public class ListTerritoriesResponse
    {
        public List<Territory> Territories { get; set; } = new List<Territory>();
    }
}