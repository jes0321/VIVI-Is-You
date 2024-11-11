using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;


public abstract class Verb : Object
{
    protected override void Awake()
    {
        base.Awake();
        RollBackManager.Instance._inputReader.OnTurnEndEvent += DirectObject;
        RollBackManager.Instance._inputReader.OnRollbackEndEvent += DirectObject;
    }

    private void Start()
    {
        DirectObject();
    }

    private void OnDestroy()
    {
        RollBackManager.Instance._inputReader.OnTurnEndEvent -= DirectObject;
        RollBackManager.Instance._inputReader.OnRollbackEndEvent -= DirectObject;
    }

    private void DirectObject() //양쪽 감지하는 코드
    {
        ShootRayAndApply(-Vector2.right);
        ShootRayAndApply(Vector2.up);
    }

    private void ShootRayAndApply(Vector2 dir)
    {
        Vector3 padding = new Vector3(dir.x * 0.5f, dir.y * 0.5f, 0);
        RaycastHit2D ray = Physics2D.Raycast(transform.position+padding, dir, 1);
        if (ray.collider != null&&ray.collider.TryGetComponent(out Subject subject))
        {
            RaycastHit2D otherRay = Physics2D.Raycast(transform.position+-padding, -dir, 1);
            if (otherRay.collider != null&&otherRay.collider.TryGetComponent(out IVerbable verbable))
            {
                if(subject.IsApply(dir,verbable))
                    ApplyVerb(subject, verbable);
            }
        }
    }
    /// <summary>
    /// agent 받아와서 동사 적용시켜주는 함수
    /// </summary>
    /// <param name="agents"></param>
    protected abstract void ApplyVerb(Subject subject, IVerbable verbable);
}
