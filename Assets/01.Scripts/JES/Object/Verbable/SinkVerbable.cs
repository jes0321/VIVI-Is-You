using System.Collections.Generic;

public class SinkVerbable : Object, IVerbable
{
    public void VerbApply(List<Agent> agents)
    {
        agents.ForEach(agent =>
        {
            agent.GetCompo<VerbCollider>().ToggleSinkCollider(true);
        });
    }

    public void VerbCancel(List<Agent> agents)
    {
        agents.ForEach(agent =>
        {
            agent.GetCompo<VerbCollider>().ToggleSinkCollider(false);
        });
    }
}