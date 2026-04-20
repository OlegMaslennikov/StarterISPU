using UnityEngine;

public class GameplayState : IGameState{
    private StateManager _stateManager;
    public SceneChanger SceneChanger { get; set; }

    public void EnterState(StateManager stateManager)
    {
        _stateManager = stateManager;
        Debug.Log("Gameplay State Entered");
        ExecuteState();
    }

    public void ExecuteState()
    {
        
    }

    public void ExitState()
    {
        Debug.Log("Gameplay State Exited");
    }
}