using UnityEngine;

[CreateAssetMenu(menuName = "Game", fileName = "PlayerStartData")]
public class PlayerStartDataSO : ScriptableObject
{
    public int MaxHealth;
    public int StartingCoins;
}
