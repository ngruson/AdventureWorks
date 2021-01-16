using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AW.Infrastructure.Http
{
    public class HttpRequestBuilder : IHttpRequestBuilder
    {
        private HttpMethod method = null;
        private string requestUri = "";
        private HttpContent content = null;
        private string bearerToken = "";
        private string acceptHeader = "application/json";
        private TimeSpan timeout = new TimeSpan(0, 0, 15);
        private readonly Dictionary<string, string> headers = new Dictionary<string, string>();

        public HttpRequestBuilder AddMethod(HttpMethod method)
        {
            this.method = method;
            return this;
        }

        public HttpRequestBuilder AddRequestUri(string requestUri)
        {
            this.requestUri = requestUri;
            return this;
        }

        public HttpRequestBuilder AddContent(HttpContent content)
        {
            this.content = content;
            return this;
        }

        public HttpRequestBuilder AddBearerToken(string bearerToken)
        {
            this.bearerToken = bearerToken;
            return this;
        }

        public HttpRequestBuilder AddAcceptHeader(string acceptHeader)
        {
            this.acceptHeader = acceptHeader;
            return this;
        }

        public HttpRequestBuilder AddHeader(string key, string value)
        {
            headers.Add(key, value);
            return this;
        }

        public HttpRequestBuilder AddHeaders(Dictionary<string, string> headers)
        {
            headers
                .ToList()
                .ForEach(hdr =>
                   {
                       if (!this.headers.ContainsKey(hdr.Key))
                           AddHeader(hdr.Key, hdr.Value);
                   }
                );

            return this;
        }

        public HttpRequestBuilder AddTimeout(TimeSpan timeout)
        {
            this.timeout = timeout;
            return this;
        }

        public async Task<HttpResponseMessage> SendAsync()
        {
            var request = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(requestUri)
            };

            if (content != null)
                request.Content = content;

            if (!string.IsNullOrEmpty(bearerToken))
                request.Headers.Authorization =
                  new AuthenticationHeaderValue("Bearer", bearerToken);

            request.Headers.Accept.Clear();
            if (!string.IsNullOrEmpty(acceptHeader))
                request.Headers.Accept.Add(
                   new MediaTypeWithQualityHeaderValue(acceptHeader));

            headers.ToList().ForEach(x =>
            {
                request.Headers.Add(x.Key, x.Value);
            });

            var client = new HttpClient
            {
                Timeout = timeout
            };

            return await client.SendAsync(request);
        }
    }
}