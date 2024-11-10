using System;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;


public enum AgentType
{
    VIVI,WALL,BOX,DOOR
}
public abstract class Agent : MonoBehaviour, IPushable
{
    protected StateMachine _stateMachine;
    
    public AgentType AgentType;
    public MoveCompo moveCompo { get; protected set; }
    [field: SerializeField]public InputReader inputReader{get; protected set;}
    protected virtual void Awake()
    {
        _stateMachine = new StateMachine();

        _stateMachine.AddState(StateType.You, new YouState(this, _stateMachine));
        
        moveCompo = GetComponent<MoveCompo>();
    }
    public void UpdateData(AgentDataSO data)
    {
        GetComponent<SpriteRenderer>().sprite = data._sprite;
        AgentType = data._type;
    }
    private void Update()
    {
        _stateMachine.UpdateCurState();
    }

    public bool IsPushable { get; set; } = false;
    public bool MoveObject(Vector2 dir)
    {
        return moveCompo.MoveAgent(dir);
    }
}

public enum StateType
{
    You,Stop,Wait
}

