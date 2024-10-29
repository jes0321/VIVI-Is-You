using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RollBackManager : MonoSingleton<RollBackManager>
{
    private float _limitTime = 0.13f; //쿨타임
    private float _lastTime=0f;//마지막 타임
    [SerializeField] private InputReader _inputReader;//인풋리더
    
    private Stack<List<RollBackData>> _rollBackStack = new Stack<List<RollBackData>>();
    private List<RollBackData> _dummyList = new List<RollBackData>();
    private void Awake()
    {
        _inputReader.OnRollbackEvent += HandleRollback;
    }

    private void OnDestroy()
    {
        _inputReader.OnRollbackEvent -= HandleRollback;
    }

    private void HandleRollback()
    {
        if(_rollBackStack.Count == 0||_lastTime+_limitTime>Time.time) return;
        List<RollBackData> dataList = new List<RollBackData>();
        dataList = _rollBackStack.Pop();
        
        foreach (var data in dataList)
        {
            if (false==data.moveCompo.MoveAgent(data.moveDir,true))
            {
                _rollBackStack.Push(dataList);
                _lastTime = 0;
                return;
            } 
        }
        _lastTime = Time.time;
    }

    public void AddRollback(MoveCompo moveCompo, Vector2 dir)
    {
        RollBackData rollbackData =  new RollBackData() { moveCompo = moveCompo, moveDir = dir };
        
        _dummyList.Add(rollbackData);
    }

    public void ListReset()
    {
        if(_dummyList.Count == 0) return;

        _rollBackStack.Push(_dummyList);
        
        _dummyList = new List<RollBackData>();
    }

}
public struct RollBackData
{
    public Vector2 moveDir;
    public MoveCompo moveCompo;
}
