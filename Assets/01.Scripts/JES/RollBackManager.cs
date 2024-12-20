using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RollBackManager : MonoSingleton<RollBackManager>
{
    private float _limitTime = 0.13f; //쿨타임
    private float _lastTime=0f;//마지막 타임
    public InputReader _inputReader;//인풋리더
    
    private Stack<List<RollBackData>> _rollBackStack = new Stack<List<RollBackData>>();
    public Action OnDestroyEvent;
    private List<RollBackData> _dummyList = new List<RollBackData>();
    private List<Agent> _agentList = new List<Agent>();
    private void Awake()
    {
        _inputReader.OnRollbackEvent += HandleRollback;
        _inputReader.OnRollbackEndEvent += AgentColliderOn;
    }

    private void OnDisable()
    {
        OnDestroyEvent?.Invoke();
        _inputReader.OnRollbackEvent -= HandleRollback;
    }

    private void HandleRollback()
    {
        if(_rollBackStack.Count == 0||_lastTime+_limitTime>Time.time) return;
        ListReset();
        List<RollBackData> dataList = new List<RollBackData>();
        dataList = _rollBackStack.Pop();
        
        foreach (var data in dataList)
        {
            if (data.offObj!=null)
            {
                _agentList.Add(data.offObj);
                data.offObj.GetCompo<VerbCollider>().isRollback = true;
                data.offObj.gameObject.SetActive(true);
                
                List<Agent> agents = new List<Agent>(){data.offObj};
                foreach (var verb in data.offObj.AgentType.verbs)
                {
                    verb.VerbApply(agents);
                }
            }

            if (data.subject != null)
            {
                data.subject.VerbCancel(new List<Agent>());
            }

            if (data.moveDir == Vector2.zero) continue;
            if (data.moveCompo.MoveAgent(data.moveDir, true)) continue;
            
            _rollBackStack.Push(dataList);
            _lastTime = 0;
            return;
        }

        _lastTime = Time.time;
    }

    private void AgentColliderOn()
    {
        foreach (var agent in _agentList)
        {
            agent.GetCompo<VerbCollider>().isRollback = false;
        }
        _agentList.Clear();
    }

    public void AddRollback(MoveCompo moveCompo, Vector2 dir, Agent go=null)
    {
        RollBackData rollbackData =  new RollBackData() { moveCompo = moveCompo, moveDir = dir,offObj = go};
        
        _dummyList.Add(rollbackData);
    }

    public void AddOffObject(Agent agent)
    {
        RollBackData rollbackData = new RollBackData(){moveCompo = agent.moveCompo,moveDir = Vector2.zero,offObj = agent};
        List<RollBackData> dataList = new List<RollBackData>();
        dataList = _rollBackStack.Pop();
        dataList.Add(rollbackData);
        _rollBackStack.Push(dataList);
    }
    public void AddTransSubject(Subject agent)
    {
        RollBackData rollbackData = new RollBackData(){moveCompo = agent._moveCompo,moveDir = Vector2.zero,offObj = null,subject = agent};
        List<RollBackData> dataList = new List<RollBackData>();
        dataList = _rollBackStack.Pop();
        dataList.Add(rollbackData);
        _rollBackStack.Push(dataList);
    }
    public RollBackData GetRollbackData(MoveCompo moveCompo)
    {
        List<RollBackData> dataList = new List<RollBackData>();
        dataList = _rollBackStack.Pop();
        foreach (var data in dataList)
        {
            if (data.moveCompo == moveCompo)
            {
                _rollBackStack.Push(dataList);
                return data;
            }
        }
        
        _rollBackStack.Push(dataList);
        return null;
    }

    public RollBackData GetRollbackData(Subject verbable)
    {
        List<RollBackData> dataList = new List<RollBackData>();
        dataList = _rollBackStack.Pop();
        foreach (var data in dataList)
        {
            if (data.subject == verbable)
            {
                _rollBackStack.Push(dataList);
                return data;
            }
        }
        
        _rollBackStack.Push(dataList);
        return null;
    }

    public void ListReset()
    {
        if(_dummyList.Count == 0) return;

        _rollBackStack.Push(_dummyList);
        
        _dummyList = new List<RollBackData>();
    }

}
public class RollBackData
{
    public Vector2 moveDir;
    public MoveCompo moveCompo;
    public Agent offObj;
    public Subject subject;
}
