using System.Net.Http;

namespace AW.SharedKernel.UnitTesting.Graph
{
    public class MockRequestExecutingEventArgs
    {
        public HttpRequestMessage RequestMessage { get; }
        public object Result { get; set; }

        public MockRequestExecutingEventArgs(HttpRequestMessage message)
        {
            RequestMessage = message;
        }
    }
}