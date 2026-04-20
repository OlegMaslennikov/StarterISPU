using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LevelButtons : MonoBehaviour
{
    private const int DAMAGE_AMOUNT = 10;
    private const int COINS_DELTA = 1;
    
    [SerializeField] private Button changeHPButton;
    [SerializeField] private Button changeCoinsButton;


    
    private GameDataController _gameDataController;
    private PlayerStartDataSO _startDataSo;
    
    [Inject]
    public void Construct(GameDataController gameDataController, PlayerStartDataSO startDataSo)
    {
        _gameDataController = gameDataController;
        _startDataSo = startDataSo;
        Initialize();
    }
    
    private void Initialize()
    {
        if (_startDataSo == null)
        {
            Debug.LogError("Failed to load PlayerStartDataSO!");
            return;
        }
        changeHPButton.onClick?.AddListener(ChangeHP);
        changeCoinsButton.onClick?.AddListener(ChangeCoins);
        Debug.Log("LevelButtons Initialized");
    }
    
    private void ChangeCoins()
    {
        _gameDataController.ChangeCoins(COINS_DELTA);
    }

    private void ChangeHP()
    {
        _gameDataController.ChangeHealth(-DAMAGE_AMOUNT);
    }
    
    private void OnDestroy()
    {
        changeHPButton.onClick?.RemoveListener(ChangeHP);
        changeCoinsButton.onClick?.RemoveListener(ChangeCoins);
    }
}