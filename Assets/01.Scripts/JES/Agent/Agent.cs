using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;


public enum AgentType
{
    VIVI,WALL,BOX,DOOR,FLAG
}
public abstract class Agent : MonoBehaviour, IPushable
{
    public AgentDataSO AgentType;
    public MoveCompo moveCompo { get; protected set; }
    [field: SerializeField]public InputReader inputReader{get; protected set;}

    public bool _isYouState = false;
    
    protected virtual void Awake()
    {
        
        moveCompo = GetComponent<MoveCompo>();
        
        GetComponentsInChildren<IAgentCompo>(true).ToList()
            .ForEach(component=>_compoDic.Add(component.GetType(), component));
        InitComponents();
        
        inputReader.OnMovementEvent += HandleOnMovement;
    }

    private void OnDestroy()
    {
        inputReader.OnMovementEvent -= HandleOnMovement;
    }
    private void HandleOnMovement(Vector2 obj)
    {
        if(!_isYouState) return;
        RollBackManager.Instance.ListReset();
        moveCompo.MoveAgent(obj);
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
    public void YouStateTrans(bool value)
    {
        _isYouState = value;
    }
}

public enum StateType
{
    You,Stop,Wait
}

