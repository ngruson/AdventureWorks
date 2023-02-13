using System.Buffers;
using System.Text;
using System.Text.Json;
using AW.Services.SharedKernel.JsonConverters;
using AW.Services.SharedKernel.ValueTypes;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Xunit;

namespace AW.Services.SharedKernel.UnitTests
{
    public class EmailAddressConverterUnitTests
    {
        public class Read
        {
            [Theory, AutoMoqData]
            public void ReturnsEmailAddressGivenAsString(
                EmailAddressConverter sut,
                JsonSerializerOptions options
            )
            {
                //Arrange
                var emailAddress = EmailAddress.Create("test@test.com");

                var message = new
                {
                    EmailAddress = emailAddress.Value.Value
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
                result.Should().Be(emailAddress.Value);
            }
        }

        public class Write
        {
            [Theory, AutoMoqData]
            public void ReturnsTrueGivenBooleanAsString(
                EmailAddressConverter sut,
                JsonSerializerOptions options
            )   
            {
                //Arrange
                var emailAddress = EmailAddress.Create("test@test.com");

                var stream = new MemoryStream();
                var writer = new Utf8JsonWriter(stream);

                //Act
                writer.WriteStartObject();
                writer.WritePropertyName("EmailAddress");

                sut.Write(writer, emailAddress.Value, options);

                writer.WriteEndObject();
                writer.Flush();
                stream.Position = 0;

                //Assert
                var reader = new StreamReader(stream);
                string json = reader.ReadToEnd();

                var doc = JsonDocument.Parse(json);
                doc.RootElement.TryGetProperty("EmailAddress", out var property)
                    .Should().BeTrue();
                property.GetString().Should().Be(emailAddress.Value.Value);
            }
        }
    }
}
