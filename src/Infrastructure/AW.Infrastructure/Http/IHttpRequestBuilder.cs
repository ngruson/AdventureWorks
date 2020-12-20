using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AW.Infrastructure.Http
{
    public interface IHttpRequestBuilder
    {
        HttpRequestBuilder AddAcceptHeader(string acceptHeader);
        HttpRequestBuilder AddBearerToken(string bearerToken);
        HttpRequestBuilder AddContent(HttpContent content);
        HttpRequestBuilder AddHeader(string key, string value);
        HttpRequestBuilder AddMethod(HttpMethod method);
        HttpRequestBuilder AddRequestUri(string requestUri);
        HttpRequestBuilder AddTimeout(TimeSpan timeout);
        Task<HttpResponseMessage> SendAsync();
    }
}