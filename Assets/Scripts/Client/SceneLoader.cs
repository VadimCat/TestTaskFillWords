using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Client
{
    public class SceneLoader : ITickable
    {
        public event Action<float> OnProgressUpdate;
        private AsyncOperation currentLoadingOperation;
        
        public async Task LoadScene(string sceneName)
        {
            await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single).ToTask();
            var scene = SceneManager.GetSceneByName(sceneName);
            SceneManager.SetActiveScene(scene);
            currentLoadingOperation = null;
        } 

        public void Tick()
        {
            if(currentLoadingOperation != null)
                OnProgressUpdate?.Invoke(currentLoadingOperation.progress);
        }
    }
}