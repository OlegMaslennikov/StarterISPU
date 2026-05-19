using System;

namespace _Game.Scripts.Services.Achievements
{
    public class BoolRequirement : IAchievementRequirement
    {
        public bool RequiredValue { get; set; }
        
        public bool IsMet(object currentValue) => Convert.ToBoolean(currentValue) == RequiredValue;
        public string GetDescription() => RequiredValue ? "Активировать" : "Деактивировать";
        public object GetRequiredValue() => RequiredValue;
    }
}