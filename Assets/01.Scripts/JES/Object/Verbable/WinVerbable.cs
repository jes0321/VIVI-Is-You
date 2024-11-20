using System.Collections.Generic;
using UnityEngine;

public class WinVerbable : Object, IVerbable
{
    private EffectPlayer _effectPlayer = null;
    public void VerbApply(List<Agent> agents)
    {
        agents.ForEach(agent =>
        {
            _effectPlayer = PoolManager.Instance.Pop("WinEffect") as EffectPlayer;
            _effectPlayer.SetPositionAndPlay(agent.transform.position);
            agent.GetCompo<VerbCollider>().ToggleWinCollider(true);
        });
    }

    public void VerbCancel(List<Agent> agents)
    {
        agents.ForEach(agent =>
        {
            _effectPlayer.StopEffect();
            agent.GetCompo<VerbCollider>().ToggleWinCollider(false);
        });
    }
}
