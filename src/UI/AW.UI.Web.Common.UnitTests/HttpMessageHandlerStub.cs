using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AW.UI.Web.Common.UnitTests
{
    public class HttpMessageHandlerStub : HttpMessageHandler
    {
        private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> sendAsync;

        public HttpMessageHandlerStub(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> sendAsync) =>
            this.sendAsync = sendAsync;

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return await sendAsync(request, cancellationToken);
        }
    }
}