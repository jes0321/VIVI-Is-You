using System.Collections.Generic;
using UnityEngine;

public class WinVerbable : Object, IVerbable
{
    public void VerbApply(List<Agent> agents)
    {
        agents.ForEach(agent =>
        {
            agent.GetCompo<WinCollider>().ToggleWinCollider(true);
        });
    }

    public void VerbCancel(List<Agent> agents)
    {
        agents.ForEach(agent =>
        {
            agent.GetCompo<WinCollider>().ToggleWinCollider(false);
        });
    }
}
