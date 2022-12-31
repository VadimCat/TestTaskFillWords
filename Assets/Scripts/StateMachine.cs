using System;
using System.Collections.Generic;

public class StateMachine
{
    private readonly StateFactory _stateFactory;
    private Dictionary<Type, IState> _states = new();
    private IState _currentState;

    public StateMachine(StateFactory stateFactory)
    {
        _stateFactory = stateFactory;
        
        _states.Add(typeof(LoadingState), stateFactory.Create<LoadingState>(this));
        _states.Add(typeof(GameState), stateFactory.Create<GameState>(this));
    }

    public void Enter<TState>() where TState : IState
    {
        _currentState?.Exit();
        _currentState = _states[typeof(TState)];
        _currentState.Enter();
    }
}