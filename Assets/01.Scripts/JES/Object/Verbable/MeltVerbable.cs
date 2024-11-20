using System.Collections.Generic;

public class MeltVerbable : Object, IVerbable
{
    public void VerbApply(List<Agent> agents)
    {
        foreach (var agent in agents)
        {
            agent._isMelt = true;
        }
    }

    public void VerbCancel(List<Agent> agents)
    {
        foreach (var agent in agents)
        {
            agent._isMelt = false;
        }
    }
}