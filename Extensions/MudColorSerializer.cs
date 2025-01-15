// ***********************************************************************
// Author           : MOLEX\kzheng
// Created          : 01/13/2025
// ***********************************************************************

using MudBlazor.Utilities;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MudBlazor.ThemeManager.Extensions
{
    /// <summary>
    /// Defines the <see cref="MudColorSerializer" />
    /// </summary>
    public class MudColorSerializer : JsonConverter<MudColor>
    {
        #region Methods

        /// <summary>
        /// The Read
        /// </summary>
        /// <param name="reader">The reader<see cref="Utf8JsonReader"/></param>
        /// <param name="typeToConvert">The typeToConvert<see cref="Type"/></param>
        /// <param name="options">The options<see cref="JsonSerializerOptions"/></param>
        /// <returns>The <see cref="MudColor"/></returns>
        public override MudColor Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected start of object");
            }

            byte r = 0, g = 0, b = 0, a = 255; // Default alpha to 255 for full opacity

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return new MudColor(r, g, b, a);
                }

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = reader.GetString();

                    reader.Read(); // Move to the value token

                    switch (propertyName)
                    {
                        case "R":
                            r = reader.GetByte();
                            break;
                        case "G":
                            g = reader.GetByte();
                            break;
                        case "B":
                            b = reader.GetByte();
                            break;
                        case "A":
                            a = reader.GetByte();
                            break;
                        default:
                            throw new JsonException($"Unexpected property: {propertyName}");
                    }
                }
            }

            throw new JsonException("Unexpected end of JSON");
        }

        /// <summary>
        /// The Write
        /// </summary>
        /// <param name="writer">The writer<see cref="Utf8JsonWriter"/></param>
        /// <param name="value">The value<see cref="MudColor"/></param>
        /// <param name="options">The options<see cref="JsonSerializerOptions"/></param>
        public override void Write(Utf8JsonWriter writer, MudColor value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteNumber("R", value.R);
            writer.WriteNumber("G", value.G);
            writer.WriteNumber("B", value.B);
            writer.WriteNumber("A", value.A);
            writer.WriteEndObject();
        }

        #endregion
    }
}
