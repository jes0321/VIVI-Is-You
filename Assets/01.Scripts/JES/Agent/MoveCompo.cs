using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class MoveCompo : MonoBehaviour
{
    private float moveTime = 0.2f;
    private float lastMoveTime = 0.0f;
    [SerializeField] private MapInfoSO mapInfoSo;

    #region MoveAgent

    public bool MoveAgent(Vector2 inputVector, bool isRoll = false)
    {
        if(!(lastMoveTime + moveTime < Time.time)) return false;
       
        var moveVector = MoveVectorSetting(inputVector,isRoll);
        Vector3 movePos =  mapInfoSo.CellCenterPos(transform.position,moveVector);
        
        if (movePos == Vector3.zero)
        {
            Debug.Log("0이라 스택에서 뺐어");
            _rollbackVecList.Pop();
            return false;
        }
        transform.DOMove(movePos, 0.2f).SetEase(Ease.OutQuint);
        
        lastMoveTime = Time.time;
        return true;
    }
    
    private Vector2Int MoveVectorSetting(Vector2 inputVector,bool isRoll)
    {
        Vector2Int moveVector = Vector2Int.RoundToInt(inputVector);
        if (Mathf.Abs(moveVector.x) > 0)
        {
            moveVector.y = 0;
        }
        
        if (!isRoll)
        {
            PushDitect(moveVector);
            _rollbackVecList.Push(moveVector*-1);
        }
        return moveVector;
    }

    #endregion
    
    #region Rollback

    private Stack<Vector2> _rollbackVecList = new Stack<Vector2>();
    private bool _isRollback => _rollbackVecList.Count != 0;
    public void HandleRollback()
    {
        if(!_isRollback) return;
        
        Vector2 move =  _rollbackVecList.Pop();
        
        if (!MoveAgent(move, true)) _rollbackVecList.Push(move);
    }

    #endregion

    #region Push

    private void PushDitect(Vector2 Vec)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position+new Vector3(Vec.x*0.6f,Vec.y*0.6f,0), Vec,0.4f);
    
        // 레이가 충돌한 경우 IPushable 인터페이스가 있는지 확인
        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent(out IPushable pushable))
            { 
                if (pushable.IsPushable)
                {
                    Debug.Log("IPushable 객체 감지됨!");
                    pushable.MoveObject(Vec); // 감지된 객체에 동작 수행
                }
            }
            else
            {
                Debug.Log(hit.collider.gameObject.name);
                Debug.Log("IPushable이 아니거나 Pushable 상태 아님.");
            }
        }
        else
        {
            Debug.Log("레이가 아무 객체와도 충돌하지 않음.");
        }
    } 
    #endregion
    
}
