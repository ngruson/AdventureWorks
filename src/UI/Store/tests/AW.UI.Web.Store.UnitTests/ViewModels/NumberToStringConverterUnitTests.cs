using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Store.ViewModels.Converters;
using FluentAssertions;
using System;
using System.Text.Json;
using Xunit;

namespace AW.UI.Web.Store.UnitTests.ViewModels
{
    public class NumberToStringConverterUnitTests
    {
        [Theory, AutoMoqData]
        public void ReadJson_NumberElement_ReturnsString(
            NumberToStringConverter sut,
            int value
        )
        {
            //Arrange
            string json = $"{value}";

            var serializeOptions = new JsonSerializerOptions
            {
                Converters = { sut }
            };

            //Act
            var result = JsonSerializer.Deserialize(
                json,
                typeof(string),
                serializeOptions
            );

            //Assert
            result.Should().Be(value.ToString());
        }

        [Theory, AutoMoqData]
        public void ReadJson_NumberElementInQuotes_ReturnsString(
            NumberToStringConverter sut,
            int value
        )
        {
            //Arrange
            string json = $"\"{value}\"";

            var serializeOptions = new JsonSerializerOptions
            {
                Converters = { sut }
            };

            //Act
            var result = JsonSerializer.Deserialize(
                json,
                typeof(string),
                serializeOptions
            );

            //Assert
            result.Should().Be(value.ToString());
        }

        [Theory, AutoMoqData]
        public void ReadJson_Object_ThrowsJsonException(
            NumberToStringConverter sut
        )
        {
            //Arrange
            string json = "{ number : 5 }";

            var serializeOptions = new JsonSerializerOptions
            {
                Converters = { sut }
            };

            //Act
            Action act = () => JsonSerializer.Deserialize(
                json,
                typeof(string),
                serializeOptions
            );

            //Assert
            act.Should().Throw<JsonException>();
        }

        [Theory, AutoMoqData]
        public void WriteJson_NumberElement_Ok(
            NumberToStringConverter sut,
            int numberValue
        )
        {
            //Arrange
            var serializeOptions = new JsonSerializerOptions
            {
                Converters = { sut }
            };

            //Act
            var result = JsonSerializer.Serialize(
                numberValue,
                typeof(int),
                serializeOptions
            );

            //Assert
            result.Should().Be(numberValue.ToString());
        }
    }
}