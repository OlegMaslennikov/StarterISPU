namespace _Game.Scripts.Services.Achievements
{
    public interface IAchievementRequirement
    {
        bool IsMet(object currentValue);
        string GetDescription();
        object GetRequiredValue();
    }
}