using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public Agent agent;
    
    private Dictionary<StateType, State> _stateDic;
    private Dictionary<StateType, State> _currStateDic;


    #region Setting

    public StateMachine()
    {
        _stateDic = new Dictionary<StateType, State>();
        _currStateDic = new Dictionary<StateType, State>();
    }

    public void Initalize(Agent agent)
    {
        this.agent = agent;
    }
    public void AddState(StateType type,State state)
    {
        _stateDic.Add(type, state);
    }

    #endregion

    #region Use Machine

    public void AddCurState(StateType type)
    {
        State state = _stateDic[type];
        
        state.Enter();
        _currStateDic.Add(type, state);
    }

    public void ExitCurState(StateType type)
    {
        if (!_currStateDic.ContainsKey(type))
        {
            Debug.LogError("그런건 존재하지 않아!!");
            return;
        }
        _currStateDic[type].Exit();
        _currStateDic.Remove(type);
    }

    public void UpdateCurState()
    {
        foreach (var state in _currStateDic.Values)
        {
            state.StateUpdate();
        }
    }

    #endregion
   
}