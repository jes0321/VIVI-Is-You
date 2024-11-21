using System.Collections.Generic;
using UnityEngine;

public class ShutVerbable : Object,IVerbable
{
    public void VerbApply(List<Agent> agents)
    {
        agents.ForEach(agent =>
        {
            agent.GetCompo<VerbCollider>().ToggleAttribueCollider(AttributeType.Shut,true);
        });
    }

    public void VerbCancel(List<Agent> agents)
    {
        agents.ForEach(agent =>
        {
            agent.GetCompo<VerbCollider>().ToggleAttribueCollider(AttributeType.Shut,false);
        });
    }
}