using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AgentDataSO", menuName = "SO/AgentData")]
public class AgentDataSO : ScriptableObject
{
    public Sprite _sprite;
    public AgentType _type;
    public List<Agent> agents = new List<Agent>();
    public List<IVerbable> verbs = new List<IVerbable>();
    public bool isMove = false;
    public void ListReset()
    {
        agents.Clear();
        verbs.Clear();
    }
}
