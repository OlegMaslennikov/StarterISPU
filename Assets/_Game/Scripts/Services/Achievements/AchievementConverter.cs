using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace _Game.Scripts.Services.Achievements
{
    public class AchievementConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => typeof(Achievement).IsAssignableFrom(objectType);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            var achievement = new Achievement
            {
                Id = obj["Id"]?.Value<string>(),
                Name = obj["Name"]?.Value<string>(),
                Description = obj["Description"]?.Value<string>(),
                IsUnlocked = obj["IsUnlocked"]?.Value<bool>() ?? false,
                UnlockedAt = obj["UnlockedAt"]?.Value<DateTime?>(),
                Requirements = new List<IAchievementRequirement>()
            };

            var requirementsArray = obj["Requirements"] as JArray;
            if (requirementsArray != null)
            {
                foreach (var req in requirementsArray)
                {
                    var requirement = req.ToObject<IAchievementRequirement>(serializer);
                    if (requirement != null)
                        achievement.Requirements.Add(requirement);
                }
            }

            return achievement;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var achievement = (Achievement)value;
            writer.WriteStartObject();
            
            writer.WritePropertyName("Id");
            writer.WriteValue(achievement.Id);
            
            writer.WritePropertyName("Name");
            writer.WriteValue(achievement.Name);
            
            writer.WritePropertyName("Description");
            writer.WriteValue(achievement.Description);
            
            writer.WritePropertyName("IsUnlocked");
            writer.WriteValue(achievement.IsUnlocked);
            
            if (achievement.UnlockedAt.HasValue)
            {
                writer.WritePropertyName("UnlockedAt");
                writer.WriteValue(achievement.UnlockedAt.Value);
            }
            
            writer.WritePropertyName("Requirements");
            writer.WriteStartArray();
            foreach (var req in achievement.Requirements)
            {
                serializer.Serialize(writer, req);
            }
            writer.WriteEndArray();
            
            writer.WriteEndObject();
        }
    }
}