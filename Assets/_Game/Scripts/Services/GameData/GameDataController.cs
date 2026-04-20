using System;
using UnityEngine;
using Zenject;

public class GameDataController : IInitializable
{
    private GameData _gameData;
    private PlayerStartDataSO _playerStartData;
    
    public event Action ChangeUIOnClick; 
    
    [Inject]
    public GameDataController(PlayerStartDataSO playerStartData, GameData gameData)
    {
        _playerStartData = playerStartData;
        _gameData = gameData;
    }
    
    public void Initialize()
    {
        Debug.Log("GameData.Initialize() called!");
        _gameData.Health = _playerStartData.MaxHealth;
        _gameData.Coins = _playerStartData.StartingCoins;
        UpdateUI();
    }

    public void ResetData()
    {
        _gameData.Health = _playerStartData.MaxHealth;
        _gameData.Coins = _playerStartData.StartingCoins;
    }
    
    public void LoadFrom(GameData other)
    {
        _gameData.Health = other.Health;
        _gameData.Coins = other.Coins;
    }
    
    public void ChangeCoins(int delta)
    {
        int newValue = _gameData.Coins + delta;
        _gameData.Coins = Math.Max(newValue, 0);
        UpdateUI();
    }

    public int GetCoins()
    {
        return _gameData.Coins;
    }
    
    public void ChangeHealth(int delta)
    {
        int newValue = _gameData.Health + delta;
        _gameData.Health = Math.Max(newValue, 0);
        UpdateUI();
    }

    public int GetHealth()
    {
        return _gameData.Health;
    }
    public void UpdateUI()
    {
        ChangeUIOnClick?.Invoke();
    }
}