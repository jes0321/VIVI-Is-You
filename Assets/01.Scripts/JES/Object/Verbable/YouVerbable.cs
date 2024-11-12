using System.Collections.Generic;
using UnityEngine;

public class YouVerbable : Object, IVerbable
{
    public void VerbApply(List<Agent> agents)
    {
        foreach (var agent in agents)
        {
            agent.YouStateTrans(true);
        }
    }

    public void VerbCancel(List<Agent> agents)
    {
        foreach (var agent in agents)
        {
            agent.YouStateTrans(false);
        }
    }
}
