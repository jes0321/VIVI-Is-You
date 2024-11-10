using System.Collections.Generic;

public class PushVerbable : Object,IVerbable
{
    public void VerbApply(List<Agent> agents)
    {
        foreach (var agent in agents)
        {
            agent.IsPushable = true;
        }
    }

    public void VerbCancel(List<Agent> agents)
    {
        foreach (var agent in agents)
        {
            agent.IsPushable = false;
        }
    }
}