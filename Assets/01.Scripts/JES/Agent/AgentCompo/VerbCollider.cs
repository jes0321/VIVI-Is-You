using System;
using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum AttributeType
{
    Win,Defeat,Hot,Sink,Shut
}
public class VerbCollider : MonoBehaviour, IAgentCompo
{
    private Agent _agent;
    private Agent _triggerAgent;
    public BoxCollider2D _collider;
    private StageData _stageData;

    private string _stageName => SceneManager.GetActiveScene().name;
    
    public bool _isWin=false, _isDefeat=false,_isHot=false,_isSink=false,_isShut =false;
    
    public bool isRollback=false;

    public void Initialize(Agent agent)
    {
        _agent = agent;
        _collider = GetComponent<BoxCollider2D>();
        _stageData =DataManger.Instance.saveData;
    }
    private bool IsFalseAndTrue()
    {
        return _isWin || _isDefeat|| _isHot|| _isSink|| _isShut;
    }
    public void ToggleAttribueCollider(AttributeType type, bool value)
    {
        switch (type)
        {
            case AttributeType.Win:
                _isWin = value;
                break;
            case AttributeType.Defeat:
                _isDefeat = value;
                break;
            case AttributeType.Sink:
                _isSink = value;
                break;
            case AttributeType.Hot:
                _isHot = value;
                break;
            case AttributeType.Shut:
                _isShut = value;
                break;
        }
        if (!(IsFalseAndTrue()&&!value))
            _collider.enabled = value;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Agent>(out Agent agent))
            TriggerEvent(agent);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        
         _triggerAgent = null;
    }

    private void TriggerEvent(Agent agent)
    {
        if(isRollback) return;
        
        _triggerAgent = agent;
        if (_isSink)
        {
            if (agent.Collider != _agent.Collider)
            {
                AgentOffEvent(agent);
                AgentOffEvent(_agent);
            }
        }
        else if (_isDefeat && agent._isYouState)
        {
            AgentOffEvent(agent);
            AgentOffEvent(_agent);
            AgentOnEvent(_agent);
        }
        else if (_isHot)
        {
            if (agent._isMelt)
            {
                AgentOffEvent(agent);
                AgentOffEvent(_agent);
                AgentOnEvent(_agent);
            }
        } 
        else if (_isShut)
        {
            if (agent._isOpen)
            {
                AgentOffEvent(agent);
                AgentOffEvent(_agent);
            }
        }
        else if (_isWin && agent._isYouState) 
            WinActionEvent(agent);
    }
    private void Update()
    {
        TriggerEvent(_agent);
        if (_triggerAgent != null)
        {
            TriggerEvent(_triggerAgent);
        }
    }

    private void AgentOffEvent(Agent agent)
    {
        RollBackData data =RollBackManager.Instance.GetRollbackData(agent.moveCompo);
        if (data != null)
        {
            Debug.Log(1);
            data.offObj = agent;
        }
        else
        {
            RollBackManager.Instance.AddOffObject(agent);
        }
        agent.gameObject.SetActive(false);
    }
    private void AgentOnEvent(Agent agent)
    {
        agent.gameObject.SetActive(true);
    }

    private void WinActionEvent(Agent agent)
    {
        agent._isYouState = false;
        WinAction.Instance.HandleFadeEvent(false);
        if (int.Parse(_stageName) == _stageData.currentStage)
        {
            _stageData.currentStage++;
            _stageData.isFirst = true;
        }
    }
}