using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Subject : Object
{
    [SerializeField] private AgentType _agentType;
    private List<Agent> _agents = new List<Agent>();
    protected override void Awake()
    {
        base.Awake();
        
        GetComponents<Agent>().ToList().ForEach(agent =>
        {
            if (_agentType == agent.AgentType) _agents.Add(agent);
        });
    }
}
