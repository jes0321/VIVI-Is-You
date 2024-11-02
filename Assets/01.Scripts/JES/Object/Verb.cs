using System;
using System.Collections.Generic;
using UnityEngine;


public abstract class Verb : Object
{
    protected override void Awake()
    {
        base.Awake();
        RollBackManager.Instance._inputReader.OnTurnEndEvent += DirectObject;
    }

    private void OnDestroy()
    {
        RollBackManager.Instance._inputReader.OnTurnEndEvent -= DirectObject;
    }

    private void DirectObject() //양쪽 감지하는 코드
    {
    }
   

    /// <summary>
    /// agent 받아와서 동사 적용시켜주는 함수
    /// </summary>
    /// <param name="agents"></param>
    protected abstract void ApplyVerb(List<Agent> agents);
}
