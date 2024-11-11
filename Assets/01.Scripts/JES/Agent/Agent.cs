using System;
using System.Collections.Generic;
using System.Linq;
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
    
    public AgentDataSO AgentType;
    public MoveCompo moveCompo { get; protected set; }
    [field: SerializeField]public InputReader inputReader{get; protected set;}

    public bool _isYouState = false;
    
    protected virtual void Awake()
    {
        _stateMachine = new StateMachine();

        _stateMachine.AddState(StateType.You, new YouState(this, _stateMachine));
        
        moveCompo = GetComponent<MoveCompo>();
        
        GetComponentsInChildren<IAgentCompo>(true).ToList()
            .ForEach(component=>_compoDic.Add(component.GetType(), component));
        InitComponents();
        
        _stateMachine.Initalize(this);
    }

    #region compoSet

    private Dictionary<Type, IAgentCompo> _compoDic = new Dictionary<Type, IAgentCompo>();
    public T GetCompo<T>(bool isDerived = false) where T : IAgentCompo
    {
        if (_compoDic.TryGetValue(typeof(T), out var component))
        {
            return (T)component;
        }

        if (isDerived != false)
        {
            Type findType = _compoDic.Keys.FirstOrDefault(t=>t.IsSubclassOf(typeof(T)));
            if(findType != null)
                return (T)_compoDic[findType];
        }
        return default;
    }
    private void InitComponents()
    {
        _compoDic.Values.ToList().ForEach(compo=>compo.Initialize(this));
    }

    #endregion
    #region Pushable

    public bool IsPushable { get; set; } = false;
    public bool MoveObject(Vector2 dir)
    {
        return moveCompo.MoveAgent(dir);
    }

    #endregion
    public void UpdateData(AgentDataSO data)
    {
        GetComponent<SpriteRenderer>().sprite = data._sprite;
        AgentType = data;
    }
    private void Update()
    {
        _stateMachine.UpdateCurState();
    }
    public void YouStateTrans(bool value)
    {
        if (value)
        {
            _stateMachine.AddCurState(StateType.You);
        }
        else
        {
            _stateMachine.ExitCurState(StateType.You);
        }
        _isYouState = value;
    }
}

public enum StateType
{
    You,Stop,Wait
}

