using AW.Core.Abstractions.Api.StateProvinceApi.ListStateProvinces;
using System.Threading.Tasks;

namespace AW.Core.Abstractions.Api.StateProvinceApi
{
    public interface IStateProvinceApi
    {
        Task<ListStateProvincesResponse> ListStateProvincesAsync(ListStateProvincesRequest request);
    }
}