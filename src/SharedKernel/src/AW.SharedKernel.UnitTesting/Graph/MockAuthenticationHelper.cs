using Microsoft.Graph;
using System.Net.Http;
using System.Threading.Tasks;

namespace AW.SharedKernel.UnitTesting.Graph
{
    public class MockAuthenticationHelper : IAuthenticationProvider
    {
        public Task AuthenticateRequestAsync(HttpRequestMessage request)
        {
            return Task.Run(() => { });
        }
    }
}