using UnityEngine;

public class State
{
    protected Agent _agent;
    protected StateMachine _stateMachine;

    public State(Agent agent, StateMachine stateMachine)
    {
        _agent = agent;
        _stateMachine = stateMachine;
    }
    public virtual void Enter()
    {
        
    }

    public virtual void Exit()
    {
        
    }

    public virtual void StateUpdate()
    {
        
    }
}
