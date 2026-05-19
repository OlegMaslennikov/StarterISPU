namespace _Game.Scripts.Services.Achievements
{
    public class StringRequirement : IAchievementRequirement
    {
        public string RequiredValue { get; set; }
        
        public bool IsMet(object currentValue) => currentValue?.ToString() == RequiredValue;
        public string GetDescription() => $"Получить значение '{RequiredValue}'";
        public object GetRequiredValue() => RequiredValue;
    }
}