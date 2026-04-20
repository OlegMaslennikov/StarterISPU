using UnityEngine;

public class PauseState : IGameState
{
    private StateManager _stateManager;
    public SceneChanger SceneChanger { get; set; }

    public void EnterState(StateManager stateManager)
    {
        _stateManager = stateManager;
        Debug.Log("Pause State Entered");
        Time.timeScale = 0f;
    }

    public void ExecuteState()
    {
        
    }

    public void ExitState()
    {
        Debug.Log("Pause State Exited");
        Time.timeScale = 1f;
    }
}