using System;
using UnityEngine;

[Serializable]
public class GameData 
{
    public int Health;
    public int Coins;
    private PlayerStartDataSO _playerStartDataSo;
    
    public GameData(PlayerStartDataSO playerStartDataSo)
    {
        _playerStartDataSo = playerStartDataSo;
    }
}