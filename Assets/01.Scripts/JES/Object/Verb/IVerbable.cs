using System;
using System.Collections.Generic;
using UnityEngine;

public interface IVerbable
{
    public void VerbApply(List<Agent> agents);
    public void VerbCancel(List<Agent> agents);
    public bool IsApply(Vector2 direction,Action<List<Agent>> cancel);
}