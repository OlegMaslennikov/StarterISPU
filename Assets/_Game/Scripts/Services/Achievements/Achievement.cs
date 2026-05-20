using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace _Game.Scripts.Services.Achievements
{
    [JsonConverter(typeof(AchievementConverter))]
    public class Achievement : IAchievement
    {
        public AchievementManager AchievementManager;
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsUnlocked { get; set; }
        public DateTime? UnlockedAt { get; set; }
        public List<IAchievementRequirement> Requirements { get; set; } = new();
        

        public bool CheckUnlock(object currentValue)
        {
            if (IsUnlocked) return true;
            
            bool allMet = Requirements.All(req => req.IsMet(currentValue));
            
            if (allMet)
                Unlock();
                
            return allMet;
        }

        public void Unlock()
        {
            if (IsUnlocked) return;
            
            IsUnlocked = true;
            UnlockedAt = DateTime.Now;
            AchievementManager.AchievementUnlockedHandler(this);           
            Debug.Log($"Achievement Unlocked: {Name} - {Description}");
        }
    }
}