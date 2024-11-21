using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Subject : Object, IVerbable
{
    [SerializeField] private AgentDataSO _agentData;
    
    private Dictionary<Vector2, VerbApply> _isVerbApplyInfoDic = new Dictionary<Vector2, VerbApply>();
    
    private List<Agent> _transAgents = new List<Agent>();
    private AgentDataSO _transData;
    private bool _isRollback = false;
    protected override void Awake()
    {
        base.Awake();

        DicSetting();
        _agentData.ListReset();
        
        RollBackManager.Instance._inputReader.OnTurnEndEvent += DirectObject;
        RollBackManager.Instance._inputReader.OnTurnEndEvent += RollBackFalse;
        RollBackManager.Instance._inputReader.OnRollbackEvent += RollBackTrue;
        RollBackManager.Instance._inputReader.OnRollbackEndEvent += DirectObject;
        RollBackManager.Instance.OnDestroyEvent += Destroy;
    }
    private void OnEnable()
    {
        FindObjectsByType<Agent>(FindObjectsSortMode.None).ToList().ForEach(agent =>
        {
            if (_agentData._type == agent.AgentType._type)
            {
                if(!_agentData.agents.Contains(agent))
                    _agentData.agents.Add(agent);
            }
        });
    }

    private void Destroy()
    {
        RollBackManager.Instance.OnDestroyEvent -= Destroy;
        RollBackManager.Instance._inputReader.OnTurnEndEvent -= DirectObject;
        RollBackManager.Instance._inputReader.OnTurnEndEvent -= RollBackFalse;
        RollBackManager.Instance._inputReader.OnRollbackEvent -= RollBackTrue;
        RollBackManager.Instance._inputReader.OnRollbackEndEvent -= DirectObject;
    }
    public List<Agent> GetAgents()
    {
        return _agentData.agents;
    }
    public void VerbApply(List<Agent> agents)
    {
        TransAgent(agents);
        TransAgentVerbApply(agents);
        TransAgentVerbCancel(agents);
        
        RollBackManager.Instance.AddTransSubject(this);
        agents.Clear();
    }

    private void TransAgentVerbCancel(List<Agent> agents)
    {
        _transData.verbs.ForEach(verb =>
        {
            verb.VerbCancel(agents);
        });
    }

    private void TransAgent(List<Agent> agents)
    {
        if(agents.Count == 0) return;
        _transData = agents[0].AgentType;
        agents.ForEach(agent =>
        {
            _transAgents.Add(agent);
            
            agent.UpdateData(_agentData);
            _agentData.agents.Add(agent);
        });
    }
    private void TransAgentVerbApply(List<Agent> agents)
    {
        foreach (var t in _agentData.verbs)
        {
            t.VerbApply(agents);
        }
    }
    public void VerbCancel(List<Agent> agents)
    {
        if (_isRollback)
        {
            _agentData.verbs.ForEach(verb =>
            {
                verb.VerbCancel(_transAgents);
            });
            
            _transAgents.ForEach(agent =>
            {
                _transData.agents.Add(agent);
                agent.UpdateData(_transData);
                _agentData.agents.Remove(agent);
            });
            
            _transData.verbs.Remove(this);
            _transData.verbs.ForEach(verb =>
            {
                verb.VerbApply(_transAgents);
            });
            
            _isRollback = false;
            _transAgents.Clear();
            _transData = null;
        }
    }
    private void DirectObject()
    {
        foreach (var data in _isVerbApplyInfoDic)
        {
            if (data.Value.IsApply.Value)
            {
                Verb verb = ShootRayAndApply(-data.Key);
                if (verb != null)
                {
                    IVerbable verbAble = verb.ShootRayAndCancel(-data.Key);
                    if (verbAble==data.Value.Target)
                    {
                        return;
                    }
                    
                    if (verbAble != null)
                    {
                        data.Value.IsApply.Value = false;
                        IsApply(data.Key, verbAble);
                        verb.ApplyVerb(this, verbAble);
                        return;
                    }
                }
                data.Value.IsApply.Value = false;           
            }
        }
    }

    private void RollBackTrue()
    {
        _isRollback = true;
    }
    private void RollBackFalse()
    {
        _isRollback = false;
    }
    private Verb ShootRayAndApply(Vector2 dir)
    {
        Vector3 padding = new Vector3(dir.x * 0.5f, dir.y * 0.5f, 0);
        RaycastHit2D ray = Physics2D.Raycast(transform.position + padding, dir, 0.4f);

        if (ray.collider != null && ray.collider.TryGetComponent(out Verb verb))
        {
            return verb;
        }
        return null;
    }

    public bool IsApply(Vector2 direction, IVerbable verbAble)
    {
        VerbApply info =  _isVerbApplyInfoDic[direction];
        if (!info.IsApply.Value)
        {
            _agentData.verbs.Add(verbAble);
            info.Target = verbAble;
            info.IsApply.Value = true;
            return true;
        }
        return false;
    }
    
    private void DicSetting()
    {
        VerbApply rightApply = new VerbApply();
        rightApply.IsApply = new NotifyValue<bool>();
        rightApply.IsApply.Value = false;
        rightApply.IsApply.OnValueChanged += RightVerbCancel;
        _isVerbApplyInfoDic.Add(-Vector2.right, rightApply);
        
        VerbApply upApply = new VerbApply();
        upApply.IsApply = new NotifyValue<bool>();
        upApply.IsApply.Value = false;
        upApply.IsApply.OnValueChanged += UpVerbCancel;
        
        _isVerbApplyInfoDic.Add(Vector2.up, upApply);
    }

    private void UpVerbCancel(bool prev, bool next)
    {
        if (!next)
        {
            VerbApply info =  _isVerbApplyInfoDic.GetValueOrDefault(Vector2.up);
            ApplyCancel(info);
        }
    }
    private void ApplyCancel(VerbApply info)
    {
        if (info.Target as Subject == null)
            info.Target.VerbCancel(_agentData.agents);
        _agentData.verbs.Remove(info.Target);
        info.Target = null;
    }
    private void RightVerbCancel(bool before, bool after)
    {
        if (!after)
        {
            VerbApply info =_isVerbApplyInfoDic[-Vector2.right];
            ApplyCancel(info);
        }
    }
}

public class VerbApply
{
    public NotifyValue<bool> IsApply;
    public IVerbable Target;
}