using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum AgentType
{
    VIVI,WALL,BOX,DOOR,FLAG,Water,Cactus,Rock,Tree
}
public class Agent : MonoBehaviour, IPushable
{
    public AgentDataSO AgentType;
    public MoveCompo moveCompo { get; protected set; }
    [field: SerializeField]public InputReader inputReader{get; protected set;}

    public bool _isYouState = false,_isMelt=false,_isOff=false,_isOpen=false;
    
    private SpriteRenderer _spriteRenderer;
    public SpriteRenderer SpriteRenderer=> _spriteRenderer;
    public Collider2D Collider { get; protected set; }

    public EffectPlayer effectPlayer;

    [SerializeField] private SoundSO _walkSound;
    
    protected virtual void Awake()
    {
        Collider = GetComponent<Collider2D>();
        moveCompo = GetComponent<MoveCompo>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        GetComponentsInChildren<IAgentCompo>(true).ToList()
            .ForEach(component=>_compoDic.Add(component.GetType(), component));
        InitComponents();
        
    }
    private void OnEnable()
    {
        inputReader.OnMovementEvent += HandleOnMovement;
    }
    private void OnDisable()
    {
        inputReader.OnMovementEvent -= HandleOnMovement;
    }
    private void HandleOnMovement(Vector2 obj)
    {
        if(!_isYouState) return;
        RollBackManager.Instance.ListReset();
        if(_isOff) return;
        if (MoveObject(obj))
        {
            SoundPlayer player = PoolManager.Instance.Pop("SoundPlayer") as SoundPlayer;
            player.PlaySound(_walkSound);
        }
    }

    public void AgentOff(bool value)
    {
        _isOff = value;
        _spriteRenderer.enabled = !value;
        GetCompo<VerbCollider>()._collider.enabled = !value;
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
        _spriteRenderer.sprite = data._sprite;
        AgentType = data;
    }
    public void YouStateTrans(bool value)
    {
        _spriteRenderer.sortingLayerName = value ? "Player" : "Object";

        _isYouState = value;
    }
}

public enum StateType
{
    You,Stop,Wait
}

