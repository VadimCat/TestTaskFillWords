using System.Threading.Tasks;
using Views.UI.Screens;

namespace Core.States
{
    public class LoadingState : IState
    {
        private const string GameSceneName = "GameScene";
        private readonly StateMachine _stateMachine;
        private readonly ScreenNavigator _screenNavigator;
        private readonly SceneLoader _sceneLoader;

        private LoadingScreen _loadingScreen;
    
        public LoadingState(StateMachine stateMachine, ScreenNavigator screenNavigator, SceneLoader sceneLoader, UpdateService updateService)
        {
            _stateMachine = stateMachine;
            _screenNavigator = screenNavigator;
            _sceneLoader = sceneLoader;
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
}