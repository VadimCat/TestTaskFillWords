using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using Zenject;

namespace Core
{
    public class SceneLoader : ITickable
    {
        public event Action<float> OnProgressUpdate;
        private AsyncOperation _currentLoadingOperation;
        
        public async Task LoadScene(string sceneName)
        {
            _currentLoadingOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            await _currentLoadingOperation.ToTask();
            var scene = SceneManager.GetSceneByName(sceneName);
            SceneManager.SetActiveScene(scene);
            _currentLoadingOperation = null;
        } 

        public void Tick()
        {
            if(_currentLoadingOperation != null)
                OnProgressUpdate?.Invoke(_currentLoadingOperation.progress);
        }
    }
}