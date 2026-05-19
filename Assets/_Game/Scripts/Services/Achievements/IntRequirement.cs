using System;
using Newtonsoft.Json;

namespace _Game.Scripts.Services.Achievements
{
    [JsonConverter(typeof(AchievementRequirementConverter))]
    public class IntRequirement : IAchievementRequirement
    {
        public int RequiredValue { get; set; }
        
        public bool IsMet(object currentValue) => Convert.ToInt32(currentValue) >= RequiredValue;
        public string GetDescription() => $"Достигнуть {RequiredValue}";
        public object GetRequiredValue() => RequiredValue;
    }
}