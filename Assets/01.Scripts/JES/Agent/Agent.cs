using System;
using UnityEngine;

public abstract class Agent : MonoBehaviour
{
    protected StateMachine _stateMachine;
    
    
    public MapInfoSO mapInfoSo;
    public InputReader inputReader;
    
    protected virtual void Awake()
    {
        _stateMachine = new StateMachine();
        
        _stateMachine.AddState(StateType.You,new YouState(this,_stateMachine));
    }

    private void Update()
    {
        _stateMachine.UpdateCurState();
    }
}

public enum StateType
{
    You,Stop,Wait
}