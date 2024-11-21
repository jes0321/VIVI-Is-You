using System.Collections.Generic;
using UnityEngine;

public class WinVerbable : Object, IVerbable
{
    private List<EffectPlayer> _effectPlayer = new List<EffectPlayer>();
    public void VerbApply(List<Agent> agents)
    {
        agents.ForEach(agent =>
        {
            EffectPlayer player = PoolManager.Instance.Pop("WinEffect") as EffectPlayer;
            player.SetPositionAndPlay(agent.transform.position);
            _effectPlayer.Add(player);
            agent.GetCompo<VerbCollider>().ToggleAttribueCollider(AttributeType.Win,true);
        });
    }

    public void VerbCancel(List<Agent> agents)
    {
        agents.ForEach(agent =>
        {
            _effectPlayer[0].StopEffect();
            _effectPlayer.RemoveAt(0);
            agent.GetCompo<VerbCollider>().ToggleAttribueCollider(AttributeType.Win,false);
        });
    }
}
