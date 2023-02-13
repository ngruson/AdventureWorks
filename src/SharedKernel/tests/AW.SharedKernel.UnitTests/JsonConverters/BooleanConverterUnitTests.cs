using System.Buffers;
using System.Text;
using System.Text.Json;
using AW.SharedKernel.JsonConverters;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Xunit;

namespace AW.SharedKernel.UnitTests.JsonConverters
{
    public class BooleanConverterUnitTests
    {
        public class Read
        {
            [Theory, AutoMoqData]
            public void ReturnsTrueGivenBooleanAsString(
                BooleanConverter sut,
                JsonSerializerOptions options
            )
            {
                //Arrange
                var message = new
                {
                    IsValid = "true"
                };

                var json = new ReadOnlySequence<byte>(
                        Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message)
                    )
                );

                var reader = new Utf8JsonReader(json);
                while (reader.TokenType != JsonTokenType.String)
                    reader.Read();

                //Act
                var result = sut.Read(ref reader, typeof(object), options);

                //Assert
                result.Should().BeTrue();
            }

            [Theory, AutoMoqData]
            public void ThrowJsonExceptionGivenBooleanAsNumber(
                BooleanConverter sut,
                JsonSerializerOptions options
            )
            {
                //Arrange
                Action act = () =>
                {
                    var message = new
                    {
                        IsValid = 1
                    };

                    var json = new ReadOnlySequence<byte>(
                            Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message)
                        )
                    );

                    var reader = new Utf8JsonReader(json);
                    while (reader.TokenType != JsonTokenType.Number)
                        reader.Read();

                    sut.Read(ref reader, typeof(object), options);
                };

                //Assert
                act.Should().Throw<JsonException>();
            }
        }

        public class Write
        {
            [Theory, AutoMoqData]
            public void ReturnsTrueGivenBooleanAsString(
                BooleanConverter sut,
                JsonSerializerOptions options
            )
            {
                //Arrange
                var stream = new MemoryStream();
                var writer = new Utf8JsonWriter(stream);

                //Act
                writer.WriteStartObject();
                writer.WritePropertyName("IsValid");
                
                sut.Write(writer, true, options);

                writer.WriteEndObject();
                writer.Flush();
                stream.Position = 0;

                //Assert
                var reader = new StreamReader(stream);
                string json = reader.ReadToEnd();

                var doc = JsonDocument.Parse(json);
                doc.RootElement.TryGetProperty("IsValid", out var property)
                    .Should().BeTrue();
                property.GetBoolean().Should().BeTrue();
            }
        }
    }
}
