using System.Collections.Generic;
using UnityEngine;

public class PushVerbable : Object,IVerbable
{
    public void VerbApply(List<Agent> agents)
    {
        foreach (var agent in agents)
        {
            agent.Collider.enabled = true;
            agent.IsPushable = true;
        }
    }

    public void VerbCancel(List<Agent> agents)
    {
        foreach (var agent in agents)
        {
            agent.Collider.enabled = false;
            agent.IsPushable = false;
        }
    }
}