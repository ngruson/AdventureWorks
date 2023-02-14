using Microsoft.Graph;

namespace AW.SharedKernel.UnitTesting.Graph
{
    public class MockHttpProvider : IHttpProvider
    {
        public ISerializer Serializer { get; } = new Serializer();

        public TimeSpan OverallTimeout { get; set; } = TimeSpan.FromSeconds(10);
        public Dictionary<string, object> Responses { get; set; } = new Dictionary<string, object>();
        public event EventHandler<MockRequestExecutingEventArgs>? OnRequestExecuting;
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return Task.Run(() =>
            {
                string key = "GET:" + request.RequestUri!.ToString();
                var response = new HttpResponseMessage();
                if (OnRequestExecuting != null)
                {
                    var args = new MockRequestExecutingEventArgs(request);
                    OnRequestExecuting.Invoke(this, args);
                    if (args.Result != null)
                    {
                        response.Content = new StringContent(Serializer.SerializeObject(args.Result));
                    }
                }
                if (Responses.TryGetValue(key, out var value))
                {
                    response.Content = new StringContent(Serializer.SerializeObject(value));
                }
                return response;
            });
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption, CancellationToken cancellationToken)
        {
            return SendAsync(request);
        }
    }
}
