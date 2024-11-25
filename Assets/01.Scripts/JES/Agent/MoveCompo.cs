using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class MoveCompo : MonoBehaviour,IAgentCompo
{
    private Agent _agent;
    
    private float moveTime = 0.12f;
    private float lastMoveTime = 0.0f;
    [SerializeField] private MapInfoSO mapInfoSo;

    
    public void Initialize(Agent agent)
    {
        _agent = agent;
    }
    #region MoveAgent

    public bool MoveAgent(Vector2 inputVector,bool isRoll = false)
    {
        if(!(lastMoveTime + moveTime < Time.time)) return false;
       
        var moveVector = MoveVectorSetting(inputVector,isRoll);
        
        if(moveVector == Vector3.zero) return false;
        
        transform.DOMove(moveVector, 0.12f).SetEase(Ease.OutQuint);
        
        lastMoveTime = Time.time;
        return true;
    }
    
    private Vector3 MoveVectorSetting(Vector2 inputVector,bool isRoll)
    {
        Vector2Int moveVector = Vector2Int.RoundToInt(inputVector);
        if (Mathf.Abs(moveVector.x) > 0)
        {
            moveVector.y = 0;
        }

        Vector3 movePos;
        if (_agent._isOpen&&FrontIsShut(moveVector))
        {
            movePos = mapInfoSo.CellCenterPosNoneLimit(transform.position,moveVector);
        }
        else
        {
            movePos =  mapInfoSo.CellCenterPos(transform.position,moveVector);
        }
        
        if (!isRoll&&movePos!=Vector3.zero)
        {
            if (!PushDitect(moveVector))
            {
                return Vector3.zero;
            }
            RollBackManager.Instance.AddRollback(this,moveVector*-1);
        }
        return movePos;
    }

    #endregion

    #region Push
    
    private bool PushDitect(Vector2 Vec)
    {
        RaycastHit2D[] col = Physics2D.RaycastAll(transform.position+new Vector3(Vec.x*0.6f,Vec.y*0.6f,0), Vec,0.4f);
        bool value = true;
        foreach (var hit in col)
        {
            if (hit.collider.TryGetComponent(out IPushable pushable))
            {
                if (pushable.IsPushable)
                {
                    if (!pushable.MoveObject(Vec))
                        value = false;
                }
            }
        }
        return value;
    } 
    #endregion

    private bool FrontIsShut(Vector2 Vec)
    {
        RaycastHit2D[] col =
            Physics2D.RaycastAll(transform.position + new Vector3(Vec.x * 0.6f, Vec.y * 0.6f, 0), Vec, 0.4f);
        bool value=false;
        foreach (var hit in col)
        {
            if (hit.collider.TryGetComponent(out Agent agent))
            {
                if (agent.GetCompo<VerbCollider>()._isShut)
                {
                    value = true;
                }
            }
        }
        return value;
    }

}
