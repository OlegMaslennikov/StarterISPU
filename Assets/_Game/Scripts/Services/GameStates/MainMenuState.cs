using UnityEngine;

public class MainMenuState : IGameState
{ 
    private StateManager _stateManager;
    
    public SceneChanger SceneChanger { get; set; }

    public void EnterState(StateManager stateManager)
    {
        Debug.Log("MainMenu State Entered");
        _stateManager = stateManager;
        ExecuteState();
    }

    public void ExecuteState()
    {
        SceneChanger.ChangeScene((int)ScenesInProject.MainMenu);
    }

    public void ExitState()
    {
        Debug.Log("MainMenu State Exited");
    }
}