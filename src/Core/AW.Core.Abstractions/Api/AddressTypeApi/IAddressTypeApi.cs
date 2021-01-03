using AW.Core.Abstractions.Api.AddressTypeApi.ListAddressTypes;
using System.Threading.Tasks;

namespace AW.Core.Abstractions.Api.AddressTypeApi
{
    public interface IAddressTypeApi
    {
        Task<ListAddressTypesResponse> ListAddressTypesAsync();
    }
}