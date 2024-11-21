using System.Collections.Generic;
using UnityEngine;

public class WinVerbable : Object, IVerbable
{
    public void VerbApply(List<Agent> agents)
    {
        agents.ForEach(agent =>
        {
            EffectPlayer player = PoolManager.Instance.Pop("WinEffect") as EffectPlayer;
            player.SetPositionAndPlay(agent.transform.position);
            agent.effectPlayer = player;
            agent.GetCompo<VerbCollider>().ToggleAttribueCollider(AttributeType.Win,true);
        });
    }

    public void VerbCancel(List<Agent> agents)
    {
        agents.ForEach(agent =>
        {
            agent.effectPlayer.StopEffect();
            agent.effectPlayer = null;
            agent.GetCompo<VerbCollider>().ToggleAttribueCollider(AttributeType.Win,false);
        });
    }
}
