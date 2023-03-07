using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;

namespace AW.SharedKernel.UnitTesting.Graph
{
    public class MockAuthenticationProvider : IAuthenticationProvider
    {
        public Task AuthenticateRequestAsync(RequestInformation request, Dictionary<string, object>? additionalAuthenticationContext = null, CancellationToken cancellationToken = default)
        {
            return Task.Run(() => { }, cancellationToken);
        }
    }
}
