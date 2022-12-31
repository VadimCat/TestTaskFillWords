using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Views.Cell;

namespace Views.UI.Screens
{
    public class GameScreen : BaseScreen
    {
        public const float CellSizeRatio = 0.96F;
        public const float CellDistanceRatio = 0.04F;
        
        [SerializeField] private RectTransform _cellsRootTransform;
        [SerializeField] private GridLayoutGroup grid;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Button submitButton;

        private IFailEffect _failEffect;
        
        private Dictionary<Vector2Int, CellView> _cellViews = new();

        public Transform inputRoot => inputField.transform;
        public GridLayoutGroup Grid => grid;
        public string Input => inputField.text.Trim().ToUpperInvariant();

        public Button SubmitButton => submitButton;

        public void SetGridSizeByColumnCount(int count)
        {
            float commonCellSize = _cellsRootTransform.rect.width / count;
            grid.constraintCount = count;
            
            grid.cellSize = Vector2.one * commonCellSize * CellSizeRatio;
            grid.spacing = Vector2.one * commonCellSize * CellDistanceRatio;
        }

        public void SetFailAnimation(IFailEffect failEffect)
        {
            _failEffect = failEffect;
        }
        
        public void RegisterCellView(Vector2Int pos, CellView view)
        {
            _cellViews[pos] = view;
        }
        
        public async Task UnlockCellLetterAsync(Vector2Int pos, char letter)
        {
            await _cellViews[pos].SetCellLetterAsync(letter);
        }

        public void ClearInput()
        {
            inputField.text = String.Empty;
        }

        private void OnDestroy()
        {
            submitButton.onClick.RemoveAllListeners();
        }

        public async Task PlayFailAsync()
        {
            await _failEffect.PlayAnimation();
        }
    }
}