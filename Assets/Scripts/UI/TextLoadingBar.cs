using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TextLoadingBar : MonoBehaviour
    {
        [SerializeField] private Image loadingBar;
        [SerializeField] private TMP_Text progress;

        private Tween currentTween;
        private const string ProgressTemplate = "{0}%";

        public void SetLoadingProgress(float normalProgress)
        {
            loadingBar.fillAmount = normalProgress;
            SetTextProgress(normalProgress);
        }

        private void SetTextProgress(float normalProgress)
        {
            progress.text = string.Format(ProgressTemplate, (normalProgress * 100).ToString("N0"));
        }
    }
}