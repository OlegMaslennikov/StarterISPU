using System;
using TMPro;
using UnityEngine;
using Zenject;

public class HpAndCoinsText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private TextMeshProUGUI _coinsText;
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
        _coinsText.text = _gameDataController.GetCoins().ToString();
        _hpText.text = _gameDataController.GetHealth().ToString();
        _gameDataController.ChangeUIOnClick += UpdateUI;
    }

    public void UpdateUI()
    {
        _coinsText.text = _gameDataController.GetCoins().ToString();
        _hpText.text = _gameDataController.GetHealth().ToString();
    }

    private void OnDestroy()
    {
        if (_gameDataController != null)
            _gameDataController.ChangeUIOnClick -= UpdateUI;
    }
}
