using System;

namespace _Game.Scripts.Services.Achievements
{
    public class FloatRequirement : IAchievementRequirement
    {
        public float RequiredValue { get; set; }
        
        public bool IsMet(object currentValue) => Convert.ToSingle(currentValue) >= RequiredValue;
        public string GetDescription() => $"Достигнуть {RequiredValue:F1}";
        public object GetRequiredValue() => RequiredValue;
    }
}