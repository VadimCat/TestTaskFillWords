using System.Threading.Tasks;
using UI.Screens;
using UnityEngine;

namespace Client.Presenters
{
    public class LoadingPresenter
    {
        private readonly LoadingScreen _loadingScreen;

        public LoadingPresenter(LoadingScreen loadingScreen, AsyncOperation asyncOperation)
        {
            _loadingScreen = loadingScreen;
        }
        
        
        // private async Task LoadAsync()
        // {
        //     levelService.OnProgressUpdate += UpdateLoadingScreen;
        //     var level = await levelService.LoadNextLevelAsync();
        //     
        //     levelService.OnProgressUpdate -= UpdateLoadingScreen;
        //
        //     await screenNavigator.CloseScreen<LoadingScreen>();
        //     level.Start();
        // }
        
        private void UpdateLoadingScreen(float progress)
        {
            _loadingScreen.SetProgress(progress);
        }
    }
}