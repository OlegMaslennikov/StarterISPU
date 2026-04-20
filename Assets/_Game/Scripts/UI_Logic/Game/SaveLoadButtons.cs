using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SaveLoadButtons : MonoBehaviour
{
    [SerializeField] private Button saveGameButton;
    [SerializeField] private Button loadGameButton;

    private SaveLoadManager _saveLoadManager;
    
    [Inject]
    public void Construct(SaveLoadManager saveLoadManager)
    {
        _saveLoadManager = saveLoadManager;
        Initialize();
    }

    private void Initialize()
    {
        saveGameButton.onClick?.AddListener(SaveGame);
        loadGameButton.onClick?.AddListener(LoadGame);
    }

    private void SaveGame()
    {
        _saveLoadManager.GameSave();
    }

    public void LoadGame()
    {
        _saveLoadManager.GameLoad();
    }
    
    private void OnDestroy()
    {
        saveGameButton.onClick?.RemoveAllListeners();
        loadGameButton.onClick?.RemoveAllListeners();
    }
}