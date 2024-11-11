using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class MoveCompo : MonoBehaviour
{
    private float moveTime = 0.12f;
    private float lastMoveTime = 0.0f;
    [SerializeField] private MapInfoSO mapInfoSo;

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
        
        Vector3 movePos =  mapInfoSo.CellCenterPos(transform.position,moveVector);
        
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position+new Vector3(Vec.x*0.6f,Vec.y*0.6f,0), Vec,0.4f);
    
        // 레이가 충돌한 경우 IPushable 인터페이스가 있는지 확인
        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent(out IPushable pushable))
            { 
                if (pushable.IsPushable)
                {
                    return pushable.MoveObject(Vec);
                }
                return true;
            }
        }
        return true;
    } 
    #endregion
    
}
