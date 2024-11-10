using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Subject : Object, IVerbable
{
    [SerializeField] private AgentDataSO _agentData;
    
    private Dictionary<Vector2, VerbApply> _isVerbApplyInfoDic = new Dictionary<Vector2, VerbApply>();
    protected override void Awake()
    {
        base.Awake();

        DicSetting();
        
        FindObjectsByType<Agent>(FindObjectsSortMode.None).ToList().ForEach(agent =>
        {
            if (_agentData._type == agent.AgentType) _agentData.agents.Add(agent);
        });
        RollBackManager.Instance._inputReader.OnTurnEndEvent += DirectObject;
        RollBackManager.Instance._inputReader.OnRollbackEndEvent += DirectObject;
    }
    
    private void OnDestroy()
    {
        RollBackManager.Instance._inputReader.OnTurnEndEvent -= DirectObject;
        RollBackManager.Instance._inputReader.OnRollbackEndEvent -= DirectObject;
    }
    public List<Agent> GetAgents()
    {
        return _agentData.agents;
    }
    public void VerbApply(List<Agent> agents)
    {
        agents.ForEach(agent =>
        {
            agent.UpdateData(_agentData);
            _agentData.agents.Add(agent);
        });
        agents = new List<Agent>();
    }

    public void VerbCancel(List<Agent> agents)
    {
        //여긴 해줄게 없다
    }
    private void DirectObject()
    {
        foreach (var data in _isVerbApplyInfoDic)
        {
            if (data.Value.IsApply.Value)
            {
                data.Value.IsApply.Value = ShootRayAndApply(-data.Key);
            }
        }
    }

    private bool ShootRayAndApply(Vector2 dir)
    {
        Vector3 padding = new Vector3(dir.x * 0.5f, dir.y * 0.5f, 0);
        RaycastHit2D ray = Physics2D.Raycast(transform.position + padding, dir, 1);
        
        return ray.collider != null && ray.collider.TryGetComponent(out Verb verb);
    }

    public bool IsApply(Vector2 direction, IVerbable verb)
    {
        VerbApply info =  _isVerbApplyInfoDic[direction];
        if (!info.IsApply.Value)
        {
            info.Target = verb;
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
            info.Target.VerbCancel(_agentData.agents);
            info.Target = null;
        }
    }

    private void RightVerbCancel(bool before, bool after)
    {
        if (!after)
        {
            VerbApply info =_isVerbApplyInfoDic[-Vector2.right];
            info.Target.VerbCancel(_agentData.agents);
            info.Target = null;
        }
    }
}

public class VerbApply
{
    public NotifyValue<bool> IsApply;
    public IVerbable Target;
}