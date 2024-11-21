using System.Collections.Generic;

public class OpenVerbable : Object,IVerbable
{
    public void VerbApply(List<Agent> agents)
    {
        foreach (var agent in agents)
        {
            agent._isOpen = true;
        }
    }

    public void VerbCancel(List<Agent> agents)
    {
        foreach (var agent in agents)
        {
            agent._isOpen = false;
        }
    }
}