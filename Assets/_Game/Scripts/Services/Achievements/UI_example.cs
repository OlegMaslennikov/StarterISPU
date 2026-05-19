using System;
using System.Runtime.InteropServices;
using _Game.Scripts.Services.Achievements;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UI_example : MonoBehaviour
{
    [SerializeField] private Button _loadButton;
    [SerializeField] private Button _printButton;
    [SerializeField] private Button _secretButton;
    [SerializeField] private Button _counterButton;
    [SerializeField] private Slider _speedSlider;
    [SerializeField] private Toggle _hardModeToggle;
    [SerializeField] private Toggle _expertModeToggle;
    [SerializeField] private TMP_InputField _hardInputField;
    [SerializeField] private TMP_InputField _expertInputField;
    
    private AchievementManager _achievementManager;

    [SerializeField] private int _buttonClickCount; 
    private int ButtonClickCount
    {
        get => _buttonClickCount;
        set
        {
            _buttonClickCount = value;
            _achievementManager.UpdateProgress("button_master", _buttonClickCount);
        }
    }
    
    [SerializeField] private float _sliderValue; 
    private float SliderValue
    {
        get => _sliderValue;
        set
        {
            _sliderValue = value;
            _achievementManager.UpdateProgress("high_speed", _sliderValue);
        }
    }
    
    
    [Inject]
    public void Construct(AchievementManager achievementManager)
    {
        _achievementManager = achievementManager;
    }

    private void Start()
    {
        _loadButton?.onClick.AddListener(LoadButtonClick);
        _printButton?.onClick.AddListener(PrintButtonClick);
        _secretButton?.onClick.AddListener(SecretButtonClick);
        _counterButton?.onClick.AddListener(CounterButtonClick);
        _speedSlider?.onValueChanged.AddListener(ChangeSpeed);
        _hardInputField?.onEndEdit.AddListener(ChangeHardInput);
        _expertInputField?.onEndEdit.AddListener(ChangeExpertInput);
    }

    private void SecretButtonClick()
    {
        _achievementManager.UpdateProgress("secret_finder", true);
    }
    
    private void LoadButtonClick()
    {
        _achievementManager.LoadAchievements();
    }

    private void CounterButtonClick()
    {
        ButtonClickCount++;
    }
    public void ChangeSpeed(float value)
    {
        SliderValue = value;
    }

    public void ChangeHardInput( string value)
    {
        _achievementManager.UpdateProgress("difficulty_hard_master", value);
    }
    
    public void ChangeExpertInput( string value)
    {
        _achievementManager.UpdateProgress("difficulty_expert_master", value);
    }
    
    private void PrintButtonClick()
    {
        Debug.Log("ALL ACHIEVEMENTS:");
        foreach (var achievement in _achievementManager.GetAllAchievements())
        {
            string status = achievement.IsUnlocked ? "yes" : "no";
            Debug.Log($"{status} {achievement.Name}: {achievement.Description}");
                
            if (!achievement.IsUnlocked)
            {
                foreach (var req in achievement.Requirements)
                {
                    Debug.Log($"   - {req.GetDescription()}");
                }
            }
            else
            {
                Debug.Log($"   Unlocked at: {achievement.UnlockedAt}");
            }
        }
    }

}
