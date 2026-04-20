using Cysharp.Threading.Tasks;
using UnityEngine;

public class BootstarpState : IGameState
{
    private StateManager _stateManager;

    public SceneChanger SceneChanger { get; set; }

    public async void EnterState(StateManager stateManager)
    {
        _stateManager = stateManager;
        Debug.Log("Bootstrap State Entered");
        await InitializeServices();
    }

    private async UniTask InitializeServices()
    {
        Debug.Log("3");
        await UniTask.Delay(333);
        Debug.Log("2");
        await UniTask.Delay(333);
        Debug.Log("1");
        await UniTask.Delay(333);
        Debug.Log("Start");
        ExecuteState();
    }

    public void ExecuteState()
    {
        _stateManager.StartState(new MainMenuState());
    }

    public void ExitState()
    {
        Debug.Log("Bootstrap State Exited");
    }
}