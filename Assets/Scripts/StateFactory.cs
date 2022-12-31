using System;
using Client;
using Core;
using Ji2Core.Core.Audio;
using UI.Screens;
using View;

public class StateFactory
{
    private readonly ScreenNavigator _screenNavigator;
    private readonly SceneLoader _sceneLoader;
    private readonly UpdateService _updateService;
    private readonly CellView.Factory _cellFactory;
    private readonly AudioService _audioService;

    public StateFactory(ScreenNavigator screenNavigator, SceneLoader sceneLoader, UpdateService updateService,
        CellView.Factory cellFactory, AudioService audioService)
    {
        _screenNavigator = screenNavigator;
        _sceneLoader = sceneLoader;
        _updateService = updateService;
        _cellFactory = cellFactory;
        _audioService = audioService;
    }

    public IState Create<TState>(StateMachine stateMachine) where TState : IState
    {
        if (typeof(TState) == typeof(LoadingState))
        {
            return new LoadingState(stateMachine, _screenNavigator, _sceneLoader, _updateService);
        }

        if (typeof(TState) == typeof(GameState))
        {
            return new GameState(stateMachine, _screenNavigator, _cellFactory, _audioService);
        }

        throw new NotImplementedException($"No factory implementation for {typeof(TState)}");
    }
}