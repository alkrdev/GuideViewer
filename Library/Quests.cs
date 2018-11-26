using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Library
{
    public partial class Quests
    {
        [JsonProperty("quests")]
        public List<Quest> QuestsList { get; set; }

        [JsonProperty("loggedIn")]
        public string LoggedIn { get; set; } 

    }

    public partial class Quests
    {
        public static Quests FromJson(string json) => JsonConvert.DeserializeObject<Quests>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Quests self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new StatusConverter(),
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class StatusConverter : JsonConverter {
        public override bool CanConvert(Type t) => t == typeof(Status) || t == typeof(Status?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer) {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value) {
                case "COMPLETED":
                    return Status.Completed;
                case "NOT_STARTED":
                    return Status.NotStarted;
                case "STARTED":
                    return Status.Started;
            }

            throw new Exception("Cannot unmarshal type Status");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer) {
            var value = (Status) untypedValue;
            switch (value) {
                case Status.Completed:
                    serializer.Serialize(writer, "COMPLETED");
                    return;
                case Status.NotStarted:
                    serializer.Serialize(writer, "NOT_STARTED");
                    return;
                case Status.Started:
                    serializer.Serialize(writer, "STARTED");
                    return;
            }

            throw new Exception("Cannot marshal type Status");
        }

    }
}
