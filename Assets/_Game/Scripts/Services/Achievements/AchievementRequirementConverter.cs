using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace _Game.Scripts.Services.Achievements
{
    public class AchievementRequirementConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => 
            typeof(IAchievementRequirement).IsAssignableFrom(objectType);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            string typeName = obj["$type"]?.Value<string>();
            
            if (string.IsNullOrEmpty(typeName))
                throw new JsonSerializationException("Missing $type discriminator");

            return typeName switch
            {
                nameof(IntRequirement) => obj.ToObject<IntRequirement>(serializer),
                nameof(FloatRequirement) => obj.ToObject<FloatRequirement>(serializer),
                nameof(StringRequirement) => obj.ToObject<StringRequirement>(serializer),
                nameof(BoolRequirement) => obj.ToObject<BoolRequirement>(serializer),
                _ => throw new JsonSerializationException($"Unknown requirement type: {typeName}")
            };
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JObject obj = JObject.FromObject(value);
            obj.AddFirst(new JProperty("$type", value.GetType().Name));
            obj.WriteTo(writer);
        }
    }
}