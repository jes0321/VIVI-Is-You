using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RollBackManager : MonoSingleton<RollBackManager>
{
    [SerializeField] private InputReader _inputReader;
    
    private Stack<List<RollBackData>> _rollBackStack = new Stack<List<RollBackData>>();

    private void Awake()
    {
        _inputReader.OnRollbackEvent += HandleRollback;
    }

    private void HandleRollback()
    {
        if(_rollBackStack.Count == 0) return;
        
        List<RollBackData> dataList = new List<RollBackData>();
        dataList = _rollBackStack.Pop();

        foreach (var data in dataList)
        {
            if (false==data.moveCompo.MoveAgent(data.moveDir, false, true))
            {
                _rollBackStack.Push(dataList);
                break;
            } 
        }
    }

    public void AddRollback(MoveCompo moveCompo, Vector2 dir, bool isPush = false)
    {
        List<RollBackData> dataList = new List<RollBackData>();
        RollBackData rollbackData =  new RollBackData() { moveCompo = moveCompo, moveDir = dir };

        if (isPush)
        { 
            dataList = _rollBackStack.Pop();
            dataList.Add(rollbackData);
        }
        else
        {
            dataList.Add(rollbackData);
        }
        
        _rollBackStack.Push(dataList);
    }

}
[Serializable]
public struct RollBackData
{
    public Vector2 moveDir;
    public MoveCompo moveCompo;
}
