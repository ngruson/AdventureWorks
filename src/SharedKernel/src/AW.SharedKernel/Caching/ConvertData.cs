using System.Text.Json;

namespace AW.SharedKernel.Caching;

public static class ConvertData<T>
{
    public static async IAsyncEnumerable<T?> ByteArrayToObjectList(byte[] inputByteArray, JsonSerializerOptions? options = null)
    {
        var deserializedList = JsonSerializer.DeserializeAsyncEnumerable<T>(
            new MemoryStream(inputByteArray), 
            options
        );

        await foreach (var _item in deserializedList)
        {
            yield return _item;
        }
    }

    public static async Task<byte[]> ObjectListToByteArray(List<T> inputList, JsonSerializerOptions? options = null)
    {
        using (var memoryStream = new MemoryStream())
        {
            await JsonSerializer.SerializeAsync(memoryStream, inputList, options);
            return memoryStream.ToArray();
        }
    }

    public static async Task<T?> ByteArrayToObject(byte[] inputByteArray)
    {
        return await JsonSerializer.DeserializeAsync<T?>(new MemoryStream(inputByteArray));
    }

    public static async Task<byte[]> ObjectToByteArray(T input)
    {
        using (var memoryStream = new MemoryStream())
        {
            await JsonSerializer.SerializeAsync(memoryStream, input);
            return memoryStream.ToArray();
        }
    }
}
