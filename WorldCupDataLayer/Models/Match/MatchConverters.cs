using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldCupDataLayer.Models.Match
{
    // This static class provides a centralized location for JSON serialization settings.
    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            // Ignores metadata properties that might be included in JSON but are not needed in our C# classes.
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,

            // Prevents automatic parsing of dates into DateTime objects (useful if dates are in string format).
            DateParseHandling = DateParseHandling.None,

            // Specifies custom converters for handling special data types.
            Converters =
        {
            TypeOfEventConverter.Singleton, // Converts TypeOfEvent enum to/from JSON string.
            PositionConverter.Singleton,    // Converts Position enum to/from JSON string.
            new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal } // Ensures proper date handling.
        },
        };
    }

    // A custom JSON converter to handle cases where numeric values (long) are represented as strings in JSON.
    internal class ParseStringConverter : JsonConverter
    {
        // Determines whether this converter can be used for a given type.
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        // Custom deserialization: Converts JSON string to a long (integer).
        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null; // Handles null values.

            var value = serializer.Deserialize<string>(reader); // Read the JSON value as a string.
            if (long.TryParse(value, out long l)) // Try converting the string to a long.
            {
                return l; // Return the parsed long value.
            }

            throw new Exception("Cannot unmarshal type long"); // Error handling if conversion fails.
        }

        // Custom serialization: Converts long (integer) to a string before writing to JSON.
        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString()); // Converts long to string for JSON output.
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter(); // Singleton instance.
    }

    // Custom converter to handle conversion between JSON string and the TypeOfEvent enum.
    internal class TypeOfEventConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TypeOfEvent) || t == typeof(TypeOfEvent?);

        // Converts JSON string to TypeOfEvent enum during deserialization.
        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;

            var value = serializer.Deserialize<string>(reader); // Read the value as a string.
            switch (value)
            {
                case "goal":
                    return TypeOfEvent.Goal;
                case "substitution-in":
                    return TypeOfEvent.SubstitutionIn;
                case "substitution-out":
                    return TypeOfEvent.SubstitutionOut;
                case "yellow-card":
                    return TypeOfEvent.YellowCard;
            }

            throw new Exception("Cannot unmarshal type TypeOfEvent");
        }

        // Converts TypeOfEvent enum back to a JSON string during serialization.
        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }

            var value = (TypeOfEvent)untypedValue;
            switch (value)
            {
                case TypeOfEvent.Goal:
                    serializer.Serialize(writer, "goal");
                    return;
                case TypeOfEvent.SubstitutionIn:
                    serializer.Serialize(writer, "substitution-in");
                    return;
                case TypeOfEvent.SubstitutionOut:
                    serializer.Serialize(writer, "substitution-out");
                    return;
                case TypeOfEvent.YellowCard:
                    serializer.Serialize(writer, "yellow-card");
                    return;
            }

            throw new Exception("Cannot marshal type TypeOfEvent");
        }

        public static readonly TypeOfEventConverter Singleton = new TypeOfEventConverter();
    }

    // Custom converter to handle conversion between JSON string and the Position enum.
    internal class PositionConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Position) || t == typeof(Position?);

        // Converts JSON string to Position enum during deserialization.
        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;

            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Defender":
                    return Position.Defender;
                case "Forward":
                    return Position.Forward;
                case "Goalie":
                    return Position.Goalie;
                case "Midfield":
                    return Position.Midfield;
            }

            throw new Exception("Cannot unmarshal type Position");
        }

        // Converts Position enum back to a JSON string during serialization.
        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }

            var value = (Position)untypedValue;
            switch (value)
            {
                case Position.Defender:
                    serializer.Serialize(writer, "Defender");
                    return;
                case Position.Forward:
                    serializer.Serialize(writer, "Forward");
                    return;
                case Position.Goalie:
                    serializer.Serialize(writer, "Goalie");
                    return;
                case Position.Midfield:
                    serializer.Serialize(writer, "Midfield");
                    return;
            }

            throw new Exception("Cannot marshal type Position");
        }

        public static readonly PositionConverter Singleton = new PositionConverter();
    }
}
