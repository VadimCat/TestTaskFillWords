using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Views.UI.Screens
{
    public class ScreenNavigator : MonoBehaviour, IInitializable
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private List<BaseScreen> screens;

        private Dictionary<Type, BaseScreen> screenOrigins;

        private BaseScreen CurrentScreen;

        public void Initialize()
        {
            SceneManager.sceneLoaded += SetCamera;
            
            screenOrigins = new Dictionary<Type, BaseScreen>();
            foreach (var screen in screens)
            {
                screenOrigins[screen.GetType()] = screen;
            }
        }

        public async Task<TScreen> PushScreen<TScreen>() where TScreen : BaseScreen
        {
            if (CurrentScreen != null)
            {
                await CloseCurrent();
            }

            CurrentScreen = Instantiate(screenOrigins[typeof(TScreen)], transform);
            await CurrentScreen.AnimateShow();
            return (TScreen)CurrentScreen;
        }

        public async Task CloseScreen<TScreen>() where TScreen : BaseScreen
        {
            if (CurrentScreen is TScreen)
            {
                await CurrentScreen.AnimateClose();
                Destroy(CurrentScreen.gameObject);
                CurrentScreen = null;
            }
        }

        private void SetCamera(Scene arg0, LoadSceneMode arg1)
        {
            canvas.worldCamera = Camera.main;
        }

        private async Task CloseCurrent()
        {
            await CurrentScreen.AnimateClose();
            Destroy(CurrentScreen.gameObject);
            CurrentScreen = null;
        }
    }
}