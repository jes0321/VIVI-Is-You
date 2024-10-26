using DG.Tweening;
using UnityEngine;

public class YouState : State
{
    private float moveTime = 0.2f;
    private float lastMoveTime = 0.0f;
    public YouState(Agent agent, StateMachine stateMachine) : base(agent, stateMachine)
    {
    }

    public override void Enter()
    {
        _agent.inputReader.OnMovementEvent += HandleOnMovement;
    }

    private void HandleOnMovement(Vector2 obj)
    {
        if (moveTime + lastMoveTime < Time.time)
        { 
            var moveVector = MoveVectorSetting(obj);
            Vector3 movePos =  _agent.mapInfoSo.CellCenterPos(_agent.transform.position,moveVector);
            _agent.transform.DOMove(movePos, moveTime).SetEase(Ease.OutQuint);
            lastMoveTime = Time.time;
        }
    }

    private Vector2Int MoveVectorSetting(Vector2 obj)
    {
        Vector2Int moveVector = Vector2Int.RoundToInt(obj);
        if (Mathf.Abs(moveVector.x) > 0)
        {
            moveVector.y = 0;
        }
        return moveVector;
    }

    public override void Exit()
    {
        _agent.inputReader.OnMovementEvent -= HandleOnMovement;
    }
}
