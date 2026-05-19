using System;
using System.Collections.Generic;

namespace _Game.Scripts.Services.Achievements
{
    public interface IAchievement
    {
        string Id { get; }
        string Name { get; }
        string Description { get; }
        bool IsUnlocked { get; set; }
        DateTime? UnlockedAt { get; set; }
        List<IAchievementRequirement> Requirements { get; }
        bool CheckUnlock(object currentValue);
        void Unlock();
    }
}