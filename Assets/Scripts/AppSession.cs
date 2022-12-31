using Core.States;

public class AppSession
{
    public StateMachine StateMachine { get; }

    public AppSession(StateMachine stateStateMachine)
    {
        StateMachine = stateStateMachine;
    }
}