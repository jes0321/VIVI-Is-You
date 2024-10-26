using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class Agent : MonoBehaviour
{
    protected StateMachine _stateMachine;
    
    public MapInfoSO mapInfoSo;
    public InputReader inputReader;
    
    private float moveTime = 0.2f;
    private float lastMoveTime = 0.0f;
    protected virtual void Awake()
    {
        _stateMachine = new StateMachine();
        
        _stateMachine.AddState(StateType.You,new YouState(this,_stateMachine));

        inputReader.OnRollbackEvent += HandleRollback;
    }

   

    private void Update()
    {
        _stateMachine.UpdateCurState();
    }

    #region MoveAgent

    public bool MoveAgent(Vector2 obj, bool isRoll = false)
    {
        if(!(lastMoveTime + moveTime < Time.time)) return false;
       
        var moveVector = MoveVectorSetting(obj,isRoll);
        Vector3 movePos =  mapInfoSo.CellCenterPos(transform.position,moveVector);
        
        transform.DOMove(movePos, 0.2f).SetEase(Ease.OutQuint);
        
        lastMoveTime = Time.time;
        return true;
    }
    
    private Vector2Int MoveVectorSetting(Vector2 obj,bool isRoll)
    {
        Vector2Int moveVector = Vector2Int.RoundToInt(obj);
        if (Mathf.Abs(moveVector.x) > 0)
        {
            moveVector.y = 0;
        }

        if (!isRoll)
        {
            _rollbackVecList.Push(moveVector*-1);
        }
        return moveVector;
    }

    #endregion

    #region Rollback

    private Stack<Vector2> _rollbackVecList = new Stack<Vector2>();
    private bool _isRollback => _rollbackVecList.Count != 0;
    private void HandleRollback()
    {
        if(!_isRollback) return;
        
        Vector2 move =  _rollbackVecList.Pop();
        
        if (!MoveAgent(move, true)) _rollbackVecList.Push(move);
    }

    #endregion
}

public enum StateType
{
    You,Stop,Wait
}
