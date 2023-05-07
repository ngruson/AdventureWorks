using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AW.UI.Web.Infrastructure.Api.ApiClients
{
    public static class StreamExtensions
    {
        public static async Task<T?> DeserializeAsync<T>(
            this Stream stream
        )
        {
            return await JsonSerializer.DeserializeAsync<T?>(stream);
        }

        public static async Task<T?> DeserializeAsync<T>(
            this Stream stream,
            JsonSerializerOptions options
        )
        {
            return await JsonSerializer.DeserializeAsync<T?>(stream, options);
        }

        public static async Task<T?> DeserializeAsync<T>(
            this Stream stream,
            CancellationToken cancellationToken
        )
        {
            return await JsonSerializer.DeserializeAsync<T?>(stream, (JsonSerializerOptions?)null, cancellationToken);
        }

        public static async Task<T?> DeserializeAsync<T>(
            this Stream stream,
            JsonSerializerOptions options,
            CancellationToken cancellationToken
        )
        {
            return await JsonSerializer.DeserializeAsync<T?>(stream, options, cancellationToken);
        }
    }
}