using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AW.Infrastructure.Http
{
    public class HttpRequestFactory : IHttpRequestFactory
    {
        private readonly IHttpRequestBuilder requestBuilder;

        public HttpRequestFactory(IHttpRequestBuilder requestBuilder)
        {
            this.requestBuilder = requestBuilder;
        }

        private async Task<string> GetBearerToken(string accessToken)
        {
            if (!string.IsNullOrEmpty(accessToken))
                return accessToken;
            else if (HttpContextHelper.HttpContext != null)
            {
                return await AuthenticationHttpContextExtensions.GetTokenAsync(
                    HttpContextHelper.HttpContext, "access_token");
            }
            return null;
        }

        public async Task<HttpResponseMessage> Get(string requestUri, string accessToken = "")
        {
            var builder = requestBuilder
                .AddMethod(HttpMethod.Get)
                .AddRequestUri(requestUri)
                .AddBearerToken(await GetBearerToken(accessToken));

            return await builder.SendAsync();
        }

        public async Task<HttpResponseMessage> Get(string requestUri, Dictionary<string, string> headers, string accessToken = "")
        {
            var builder = requestBuilder
                .AddMethod(HttpMethod.Get)
                .AddRequestUri(requestUri)
                .AddHeaders(headers)
                .AddBearerToken(await GetBearerToken(accessToken));            

            return await builder.SendAsync();
        }

        public async Task<HttpResponseMessage> Post(
           string requestUri, object value, string accessToken = "")
        {
            var builder = requestBuilder
                .AddMethod(HttpMethod.Post)
                .AddRequestUri(requestUri)
                .AddContent(new JsonContent(value))
                .AddBearerToken(await GetBearerToken(accessToken));

            return await builder.SendAsync();
        }

        public async Task<HttpResponseMessage> Put(
           string requestUri, object value, string accessToken = "")
        {
            var builder = requestBuilder
                .AddMethod(HttpMethod.Put)
                .AddRequestUri(requestUri)
                .AddContent(new JsonContent(value))
                .AddBearerToken(await GetBearerToken(accessToken));

            return await builder.SendAsync();
        }

        public async Task<HttpResponseMessage> Put(string requestUri, string accessToken = "")
        {
            var builder = requestBuilder
                .AddMethod(HttpMethod.Put)
                .AddRequestUri(requestUri)
                .AddBearerToken(await GetBearerToken(accessToken)
            );

            return await builder.SendAsync();
        }

        public async Task<HttpResponseMessage> Patch(
           string requestUri, object value, string accessToken = "")
        {
            var builder = requestBuilder
                .AddMethod(new HttpMethod("PATCH"))
                .AddRequestUri(requestUri)
                .AddContent(new PatchContent(value))
                .AddBearerToken(await GetBearerToken(accessToken));

            return await builder.SendAsync();
        }

        public async Task<HttpResponseMessage> Delete(string requestUri, string accessToken = "")
        {
            var builder = requestBuilder
                .AddMethod(HttpMethod.Delete)
                .AddRequestUri(requestUri)
                .AddBearerToken(await GetBearerToken(accessToken));

            return await builder.SendAsync();
        }
    }
}