using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;

namespace _Game.Scripts.Services.Achievements
{
    public class AchievementManager
    {
        private Dictionary<string, Achievement> _achievements = new();
        private Dictionary<string, object> _progress = new(); 
        
        public AchievementManager()
        {
            InitializeDefaultAchievements();
        }
        
        private void InitializeDefaultAchievements()
        {
            var buttonPressAchievement = new Achievement
            {
                AchievementManager = this,
                Id = "button_master",
                Name = "Button Master",
                Description = "Нажми на кнопку 5 раз",
                Requirements = new List<IAchievementRequirement>
                
                {
                    new IntRequirement { RequiredValue = 5 },
                }
            };
            AddAchievement(buttonPressAchievement);
            
            var speedAchievement = new Achievement
            {
                AchievementManager = this,
                Id = "high_speed",
                Name = "High Speed",
                Description = "Набери скорость 150",
                Requirements = new List<IAchievementRequirement>
                {
                    new FloatRequirement { RequiredValue = 150f },
                }
            };
            AddAchievement( speedAchievement);
            
            var difficultyHardAchievement = new Achievement
            {
                AchievementManager = this,
                Id = "difficulty_hard_master",
                Name = "Hard Difficulty Master",
                Description = "Пройди игру на Hard",
                Requirements = new List<IAchievementRequirement>
                {
                    new StringRequirement { RequiredValue = "Hard" },
                }
            };
            AddAchievement(difficultyHardAchievement);
            
            var difficultyExpertAchievement = new Achievement
            {
                AchievementManager = this,
                Id = "difficulty_expert_master",
                Name = "Expert Difficulty Master",
                Description = "Пройди игру на Expert",
                Requirements = new List<IAchievementRequirement>
                {
                    new StringRequirement { RequiredValue = "Expert" }
                }
            };
            AddAchievement(difficultyExpertAchievement);
            
            var specialAchievement = new Achievement
            {
                AchievementManager = this,
                Id = "secret_finder",
                Name = "Secret Finder",
                Description = "Найди секретную кнопку",
                Requirements = new List<IAchievementRequirement>
                {
                    new BoolRequirement { RequiredValue = true }
                }
            };
            AddAchievement(specialAchievement);
        }
        
        public void AddAchievement(Achievement achievement)
        {
            if (!_achievements.ContainsKey(achievement.Id))
            {
                _achievements[achievement.Id] = achievement;
            }
        }
        
        public void UpdateProgress(string achievementId, object currentValue)
        {
            if (_achievements.TryGetValue(achievementId, out var achievement))
            {
                achievement.CheckUnlock(currentValue);
                UpdateAllAchievements();
            }
        }
        
        public void UpdateAllAchievements()
        {
            foreach (var achievement in _achievements.Values)
            {
                if(achievement.IsUnlocked == false) return;
            }
            Debug.Log("All achievements have been unlocked");
        }
        
        public Achievement GetAchievement(string id)
        {
            _achievements.TryGetValue(id, out var achievement);
            return achievement;
        }
        
        public List<Achievement> GetAllAchievements() => _achievements.Values.ToList();
        
        public List<Achievement> GetUnlockedAchievements() => 
            _achievements.Values.Where(a => a.IsUnlocked).ToList();
        
        public List<Achievement> GetLockedAchievements() => 
            _achievements.Values.Where(a => !a.IsUnlocked).ToList();
        
        public float GetCompletionPercentage() => 
            _achievements.Count == 0 ? 0 : (float)GetUnlockedAchievements().Count / _achievements.Count * 100f;
        
        public void AchievementUnlockedHandler(IAchievement achievement)
        {
            SaveAchievements();
        }
        
        
        public void SaveAchievements(string filePath = "achievements.json")
        {
            try
            {
                var saveData = new
                {
                    Achievements = _achievements.Values.Select(a => new
                    {
                        a.Id,
                        a.IsUnlocked,
                        a.UnlockedAt
                    }).ToList(),
                    SaveTime = DateTime.Now
                };
        
                string json = JsonConvert.SerializeObject(saveData, Formatting.Indented);
                File.WriteAllText(filePath, json);
        
                Debug.Log($"Achievements saved to {filePath}");
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to save achievements: {e.Message}");
            }
        }

        public void LoadAchievements(string filePath = "achievements.json")
        {
            if (!File.Exists(filePath)) return;
    
            try
            {
                string json = File.ReadAllText(filePath);
        
                var saveData = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        
                if (saveData != null && saveData.ContainsKey("Achievements"))
                {
                    var achievementsData = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(saveData["Achievements"].ToString());
            
                    foreach (var achievementData in achievementsData)
                    {
                        string id = achievementData["Id"]?.ToString();
                        bool isUnlocked = Convert.ToBoolean(achievementData["IsUnlocked"]);
                        DateTime? unlockedAt = null;
                
                        if (achievementData.ContainsKey("UnlockedAt") && achievementData["UnlockedAt"] != null)
                        {
                            unlockedAt = Convert.ToDateTime(achievementData["UnlockedAt"]);
                        }
                
                        if (!string.IsNullOrEmpty(id) && _achievements.TryGetValue(id, out var achievement))
                        {
                            achievement.IsUnlocked = isUnlocked;
                            achievement.UnlockedAt = unlockedAt;
                        }
                    }
                }
        
                Debug.Log($"Achievements loaded from {filePath}");
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to load achievements: {e.Message}");
            }
        }
        
        [Serializable]
        private class AchievementSaveData
        {
            public List<Achievement> Achievements { get; set; }
            public DateTime SaveTime { get; set; }
        }
        
    }
    
}