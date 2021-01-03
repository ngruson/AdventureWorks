using AW.Core.Abstractions.Api.SalesTerritoryApi.ListTerritories;
using System.Threading.Tasks;

namespace AW.Core.Abstractions.Api.SalesTerritoryApi
{
    public interface ISalesTerritoryApi
    {
        Task<ListTerritoriesResponse> ListTerritoriesAsync();
    }
}