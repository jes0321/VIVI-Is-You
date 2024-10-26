using DG.Tweening;
using UnityEngine;

public class YouState : State
{
   
    public YouState(Agent agent, StateMachine stateMachine) : base(agent, stateMachine)
    {
    }

    public override void Enter()
    {
        _agent.inputReader.OnMovementEvent += HandleOnMovement;
    }

    private void HandleOnMovement(Vector2 obj)
    {
        _agent.moveCompo.MoveAgent(obj);

    }

   

    public override void Exit()
    {
        _agent.inputReader.OnMovementEvent -= HandleOnMovement;
    }
}
