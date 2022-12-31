using Core.Audio;
using Models;
using UnityEngine;
using Views.Cell;
using Views.UI;
using Views.UI.Screens;

namespace Presenters
{
    public class LevelPresenter
    {
        private readonly LevelModel _levelModel;
        private readonly GameScreen _gameScreen;
        private readonly CellView.Factory _cellViewFactory;
        private readonly AnimationsQueuePlayer _animationsQueuePlayer = new();
        private readonly AudioService _audioService;

        public LevelPresenter(LevelModel levelModel, GameScreen gameScreen, CellView.Factory cellViewFactory,
            AudioService audioService)
        {
            _levelModel = levelModel;
            _gameScreen = gameScreen;
            _cellViewFactory = cellViewFactory;
            _audioService = audioService;
        }

        private void OnCellUnlock(Vector2Int position, char letter)
        {
            _animationsQueuePlayer.EnqueueAnimation(() => _gameScreen.UnlockCellLetterAsync(position, letter));
        }

        public void StartLevel()
        {
            SetFailEffect();
            SetSubscriptions();
            BuildLevelView();
        }

        private void BuildLevelView()
        {
            _gameScreen.SetGridSizeByColumnCount(_levelModel.LevelWidth);
            _gameScreen.Grid.constraintCount = _levelModel.LevelWidth;
            int x = _levelModel.LevelWidth;
            int y = _levelModel.LevelHeight;

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    var cell = _cellViewFactory.Create();
                    cell.transform.SetParent(_gameScreen.Grid.transform);
                    cell.gameObject.name = $"{i} {j}";
                    _gameScreen.RegisterCellView(new Vector2Int(i, j), cell);
                }
            }
        }

        private void SetSubscriptions()
        {
            _levelModel.InputFail += OnInputFail;
            _levelModel.CellUnlock += OnCellUnlock;
            _gameScreen.SubmitButton.onClick.AddListener(OnSubmit);
        }

        private void SetFailEffect()
        {
            var effectProvider = new FailEffectProvider(_gameScreen.inputRoot, _audioService);
            effectProvider.LoadConfigs();
            _gameScreen.SetFailAnimation(effectProvider.GetFailEffect());
        }

        private void OnInputFail()
        {
            _gameScreen.PlayFailAsync();
        }

        private void OnSubmit()
        {
            _levelModel.TryUnlockWord(_gameScreen.Input);
            _gameScreen.ClearInput();
        }
    }
}