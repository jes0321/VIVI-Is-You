using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEditor.Analytics;
using UnityEngine;

public class Subject : Object
{
    [SerializeField] private AgentType _agentType;
    private List<Agent> _agents = new List<Agent>();//자신이 조종하는 Agent 리스트

    protected override void Awake()
    {
        base.Awake();

        FindObjectsByType<Agent>(FindObjectsSortMode.None).ToList().ForEach(agent =>
        {
            if (_agentType == agent.AgentType) _agents.Add(agent);
        });
    }
    public List<Agent> GetAgents()
    {
        return _agents;
    }
}
