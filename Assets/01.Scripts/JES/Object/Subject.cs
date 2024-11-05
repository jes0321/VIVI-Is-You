using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEditor.Analytics;
using UnityEditor.Build.Content;
using UnityEngine;

public class Subject : Object, IVerbable
{
    [SerializeField] private AgentDataSO _agentData;
    private List<Agent> _agents = new List<Agent>();//자신이 조종하는 Agent 리스트
    protected override void Awake()
    {
        base.Awake();

        FindObjectsByType<Agent>(FindObjectsSortMode.None).ToList().ForEach(agent =>
        {
            if (_agentData._type == agent.AgentType) _agents.Add(agent);
        });
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
        });
        agents = new List<Agent>();
    }
}
