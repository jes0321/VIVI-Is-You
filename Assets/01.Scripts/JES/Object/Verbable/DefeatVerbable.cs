using System.Collections.Generic;
using UnityEngine;

public class DefeatVerbable : Object, IVerbable
{
    public void VerbApply(List<Agent> agents)
    {
        agents.ForEach(agent =>
        {
            agent.GetCompo<VerbCollider>().ToggleAttribueCollider(AttributeType.Defeat,true);
        });
    }
    public void VerbCancel(List<Agent> agents)
    {
        agents.ForEach(agent =>
        {
            agent.GetCompo<VerbCollider>().ToggleAttribueCollider(AttributeType.Defeat,false);
        });
    }
}
