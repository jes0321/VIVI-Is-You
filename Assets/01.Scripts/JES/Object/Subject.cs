using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Subject : Object, IVerbable
{
    [SerializeField] private AgentDataSO _agentData;
    private List<Agent> _agents = new List<Agent>();//자신이 조종하는 Agent 리스트
    
    private Dictionary<Vector2, VerbApply> _isVerbApplyInfoDic = new Dictionary<Vector2, VerbApply>();
    protected override void Awake()
    {
        base.Awake();

        DicSetting();
        
        FindObjectsByType<Agent>(FindObjectsSortMode.None).ToList().ForEach(agent =>
        {
            if (_agentData._type == agent.AgentType) _agents.Add(agent);
        });
        RollBackManager.Instance._inputReader.OnTurnEndEvent += DirectObject;
    }
    
    private void OnDestroy()
    {
        RollBackManager.Instance._inputReader.OnTurnEndEvent -= DirectObject;
    }
    public List<Agent> GetAgents()
    {
        return _agents;
    }
    public void VerbApply(List<Agent> agents)
    {
        agents.ForEach(agent =>
        {
            agent.UpdateData(_agentData);
            _agents.Add(agent);
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

    public bool IsApply(Vector2 direction, Action<List<Agent>> cancel)
    {
        VerbApply info =  _isVerbApplyInfoDic.GetValueOrDefault(direction);
        if (!info.IsApply.Value)
        {
            info.IsApply.Value = true;
            info.CancelAction = cancel;
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
            info.CancelAction?.Invoke(_agents);
            info.CancelAction = null;
        }
    }

    private void RightVerbCancel(bool before, bool after)
    {
        if (!after)
        {
            VerbApply info =  _isVerbApplyInfoDic.GetValueOrDefault(-Vector2.right);
            info.CancelAction?.Invoke(_agents);
            info.CancelAction = null;
        }
    }
}

public struct VerbApply
{
    public NotifyValue<bool> IsApply;
    public Action<List<Agent>> CancelAction;
}