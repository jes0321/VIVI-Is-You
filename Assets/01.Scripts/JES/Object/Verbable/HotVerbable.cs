using System;
using System.Collections.Generic;

public class HotVerbable : Object, IVerbable
{
    public void VerbApply(List<Agent> agents)
    {
        agents.ForEach(agent =>
        {
            agent.GetCompo<VerbCollider>().ToggleHotCollider(true);
        });
    }

    public void VerbCancel(List<Agent> agents)
    {
        agents.ForEach(agent =>
        {
            agent.GetCompo<VerbCollider>().ToggleHotCollider(false);
        });
    }
}