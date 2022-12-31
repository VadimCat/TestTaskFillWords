using System.Threading.Tasks;
using Client;
using Core;
using UI.Screens;
using UnityEngine;

public class LoadingState : IState
{
    private const string GameSceneName = "GameScene";
    private readonly StateMachine _stateMachine;
    private readonly ScreenNavigator _screenNavigator;
    private readonly SceneLoader _sceneLoader;
    private readonly UpdateService _updateService;

    private LoadingScreen _loadingScreen;
    
    public LoadingState(StateMachine stateMachine, ScreenNavigator screenNavigator, SceneLoader sceneLoader, UpdateService updateService)
    {
        _stateMachine = stateMachine;
        _screenNavigator = screenNavigator;
        _sceneLoader = sceneLoader;
        _updateService = updateService;
    }

    public async Task Enter()
    {
        var loadingTask = _sceneLoader.LoadScene(GameSceneName);

        _loadingScreen = await _screenNavigator.PushScreen<LoadingScreen>();
        
        _sceneLoader.OnProgressUpdate += UpdateProgress;
        
        await loadingTask;

        _sceneLoader.OnProgressUpdate -= UpdateProgress;
        
        await _screenNavigator.CloseScreen<LoadingScreen>();

        _loadingScreen = null;

        _stateMachine.Enter<GameState>();
    }

    private void UpdateProgress(float progress)
    {
        _loadingScreen.SetProgress(progress);
    }

    public async Task Exit()
    {
        await _screenNavigator.CloseScreen<LoadingScreen>();
    }
}