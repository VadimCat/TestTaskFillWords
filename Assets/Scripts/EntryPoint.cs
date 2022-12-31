using Core.Audio;
using Core.States;
using UnityEngine;
using Views.UI.Screens;
using Zenject;

public class EntryPoint : MonoBehaviour, IInitializable
{
    private AppSession _appSession;

    [Inject]
    private void SetDependencies(ScreenNavigator screenNavigator, AppSession appSession, AudioService audioService)
    {
        _appSession = appSession;
    }

    public void Initialize()
    {
        _appSession.StateMachine.Enter<LoadingState>();
    }
}