using System.Collections.Generic;

public class SinkVerbable : Object, IVerbable
{
    public void VerbApply(List<Agent> agents)
    {
        agents.ForEach(agent =>
        {
            agent.GetCompo<VerbCollider>().ToggleAttribueCollider(AttributeType.Sink,true);
        });
    }

    public void VerbCancel(List<Agent> agents)
    {
        agents.ForEach(agent =>
        {
            agent.GetCompo<VerbCollider>().ToggleAttribueCollider(AttributeType.Sink,false);
        });
    }
}