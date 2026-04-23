using System;
using UnityEngine;

[Serializable]
public class GameData 
{
    [SerializeField]
    private int _health;
    
    [SerializeField]
    private int _coins;
    
    public int Health 
    { 
        get => _health;
        set => _health = value;
    }
    
    public int Coins 
    { 
        get => _coins;
        set => _coins = value;
    }
}