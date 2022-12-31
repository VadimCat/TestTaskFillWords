using UnityEngine;

namespace Views.UI.Screens
{
    public class LoadingScreen : BaseScreen
    {
        [SerializeField] private TextLoadingBar loadingBar;
        
        public void SetProgress(float progress)
        {
            loadingBar.SetLoadingProgress(progress);
        }
    }
}