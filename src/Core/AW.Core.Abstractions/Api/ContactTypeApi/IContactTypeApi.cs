using AW.Core.Abstractions.Api.ContactTypeApi.ListContactTypes;
using System.Threading.Tasks;

namespace AW.Core.Abstractions.Api.ContactTypeApi
{
    public interface IContactTypeApi
    {
        Task<ListContactTypesResponse> ListContactTypesAsync();
    }
}