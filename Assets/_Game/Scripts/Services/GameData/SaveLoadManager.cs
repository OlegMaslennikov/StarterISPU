using System;
using System.IO;
using UnityEngine;
using Zenject;

public class SaveLoadManager
{
    private PlayerStartDataSO startDataSo; 
    private GameData _gameData;
    private GameDataController _gameDataController;
    
    private readonly string saveFileName = "gameData.json";


    [Inject]
    public SaveLoadManager(PlayerStartDataSO StartDataSo, GameData GameData, GameDataController gameDataController)
    {
        startDataSo = StartDataSo;
        _gameData = GameData;
        _gameDataController = gameDataController;
    }
    
    public void GameSave()
    {
        try
        {
            string json = JsonUtility.ToJson(_gameData, true);
            File.WriteAllText(saveFileName, json);
        }
        catch (Exception e)
        {
            Debug.LogError($"Save failed: {e.Message}");
        }
    }

    public void GameLoad()
    {
        if (!File.Exists(saveFileName))
        {
            Debug.LogWarning("Save file not found, creating new gameData.");
            CreateDefaultProfile();
            GameSave(); 
            return;
        }

        string json = File.ReadAllText(saveFileName);
        GameData loadedData = JsonUtility.FromJson<GameData>(json);

        if (loadedData == null)
        {
            Debug.LogError("Failed to parse JSON — file may be corrupted. Creating new gameData.");
            CreateDefaultProfile();
            GameSave();
            return;
        }
        
        _gameDataController.LoadFrom(loadedData);
        _gameDataController.UpdateUI();
    }

    public void DeleteSave()
    {
        if (File.Exists(saveFileName))
        {
            File.Delete(saveFileName);
            Debug.Log("Save file deleted.");
        }
    }

    private void CreateDefaultProfile()
    {
        _gameDataController.Initialize();
    }
}