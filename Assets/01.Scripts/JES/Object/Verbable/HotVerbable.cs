using System;
using System.Collections.Generic;

public class HotVerbable : Object, IVerbable
{
    public void VerbApply(List<Agent> agents)
    {
        agents.ForEach(agent =>
        {            agent.GetCompo<VerbCollider>().ToggleAttribueCollider(AttributeType.Hot,true);

        });
    }

    public void VerbCancel(List<Agent> agents)
    {
        agents.ForEach(agent =>
        {
            agent.GetCompo<VerbCollider>().ToggleAttribueCollider(AttributeType.Hot,false);
        });
    }
}