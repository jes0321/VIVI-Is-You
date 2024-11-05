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
        ShootRayAndApply(Vector2.right);
        ShootRayAndApply(Vector2.up);
    }

    private void ShootRayAndApply(Vector2 dir)
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, dir, 1);
        if (ray.collider != null)
        {
            if (ray.collider.TryGetComponent(out Subject subject))
            {
                RaycastHit2D otherRay = Physics2D.Raycast(transform.position, -dir, 1);
                if (otherRay.collider != null)
                {
                    if (otherRay.collider.TryGetComponent(out IVerbable verbable))
                    {
                        verbable.VerbApply(subject.GetAgents());
                    }
                }
            }
        }
    }


    /// <summary>
    /// agent 받아와서 동사 적용시켜주는 함수
    /// </summary>
    /// <param name="agents"></param>
    protected abstract void ApplyVerb(List<Agent> agents);
}
