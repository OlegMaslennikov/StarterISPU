using Zenject;

public class GameStateFactory
{
    private StateManager _stateManager;
    private SceneChanger _sceneChanger;
    
    [Inject]
    public GameStateFactory(StateManager stateManager, SceneChanger sceneChanger)
    {
        _stateManager = stateManager;
        _sceneChanger = sceneChanger;
    }
    
    public IGameState CreateState(IGameState newState)
    {
        return newState;    
    }
}