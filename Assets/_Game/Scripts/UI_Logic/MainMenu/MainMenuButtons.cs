using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button loadButton;
    [SerializeField] private Button optionsButton;
    private StateManager _stateManager;
    private SceneChanger _sceneChanger;
    private SaveLoadManager _saveLoadManager;
    private GameDataController _gameDataController;

    private void Start()
    {
        startGameButton.onClick?.AddListener(StartGame);
        loadButton.onClick?.AddListener(LoadGame);
        optionsButton.onClick?.AddListener(ShowOptions);
    }
    [Inject]
    public void Construct(StateManager stateManager, SceneChanger sceneChanger, SaveLoadManager saveLoadManager, GameDataController gameDataController)
    {
        _stateManager = stateManager;
        _sceneChanger = sceneChanger;
        _saveLoadManager = saveLoadManager;
        _gameDataController = gameDataController;
    }
    
    private void StartGame()
    {
        _stateManager.StartState(new GameplayState());
        _gameDataController.ResetData();
        _sceneChanger.ChangeScene((int)ScenesInProject.Level1);
    }
    
    private void LoadGame()
    {
        _stateManager.StartState(new GameplayState());
        _saveLoadManager.GameLoad();
        _sceneChanger.ChangeScene((int)ScenesInProject.Level1);
    }

    public void ShowOptions()
    {
        Debug.Log("Options");
    }
    private void OnDestroy()
    {
        startGameButton.onClick?.RemoveListener(StartGame);
        loadButton.onClick?.RemoveListener(LoadGame);
        optionsButton.onClick?.RemoveListener(ShowOptions);
    }
}