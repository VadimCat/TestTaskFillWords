using System.Threading.Tasks;
using Core.Audio;
using Models;
using Newtonsoft.Json;
using Presenters;
using UnityEngine;
using Views.Cell;
using Views.UI.Screens;

namespace Core.States
{
    public class GameState : IState
    {
        private const string LevelConfigPath = "Levels/level0";

        private readonly StateMachine _stateMachine;
        private readonly ScreenNavigator _screenNavigator;
        private readonly CellView.Factory _cellFactory;
        private readonly AudioService _audioService;
        private LevelPresenter _levelPresenter;

        public GameState(StateMachine stateMachine, ScreenNavigator screenNavigator, CellView.Factory cellFactory,
            AudioService audioService)
        {
            _stateMachine = stateMachine;
            _screenNavigator = screenNavigator;
            _cellFactory = cellFactory;
            _audioService = audioService;
        }

        public async Task Enter()
        {
            var gameScreen = await _screenNavigator.PushScreen<GameScreen>();
            var model = LoadLevel();
            _levelPresenter = new LevelPresenter(model, gameScreen, _cellFactory, _audioService);
            _levelPresenter.StartLevel();
        }

        private LevelModel LoadLevel()
        {
            var textLevelAsset = Resources.Load<TextAsset>(LevelConfigPath);
            var lvl = JsonConvert.DeserializeObject<LevelConfig>(textLevelAsset.text);
            return new LevelModel(lvl);
        }

        public async Task Exit()
        {
            await _screenNavigator.CloseScreen<GameScreen>();
        }
    }
}