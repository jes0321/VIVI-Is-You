using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VerbCollider : MonoBehaviour, IAgentCompo
{
    private Agent _agent;
    private BoxCollider2D _collider;
    [SerializeField] private StageData _stageData;
    private string _stageName => SceneManager.GetActiveScene().name;
    
    private bool _isWin=false, _isDefeat=false,_isHot=false,_isSink=false;
    
    public void Initialize(Agent agent)
    {
        _agent = agent;
        _collider = GetComponent<BoxCollider2D>();
    }
    private bool IsFalseAndTrue()
    {
        return _isWin || _isDefeat|| _isHot|| _isSink;
    }
    public void ToggleWinCollider(bool value)
    {
        _isWin = value;
        if (IsFalseAndTrue()&&!value)
            return;
        _collider.enabled = value;
    }
    public void ToggleSinkCollider(bool value)
    {
        _isSink= value;
        if (IsFalseAndTrue()&&!value)
            return;
        _collider.enabled = value;
    }
    public void ToggleHotCollider(bool value)
    {
        _isHot = value;
        if (IsFalseAndTrue()&&!value)
            return;
        _collider.enabled = value;
    }
    public void ToggleDefeatCollider(bool value)
    {
        _isDefeat = value;
        if (IsFalseAndTrue()&&!value)
            return;
        _collider.enabled = value;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Agent>(out Agent agent)&&agent._isYouState)
        {
            if (_isDefeat)
                AgentOffEvent(agent);
            else if (_isHot)
            {
                if (agent._isMelt)
                    AgentOffEvent(agent);
            }
            else if (_isSink) {
                    
                AgentOffEvent(agent);
                AgentOffEvent(_agent);
            }
            else if (_isWin)
                WinAction();
        }
    }

    private static void AgentOffEvent(Agent agent)
    {
        RollBackManager.Instance.AddRollback(agent.moveCompo,Vector2.zero,agent);
        agent.AgentOnOff(true);
    }

    private void WinAction()
    {
        if(int.Parse(_stageName) == _stageData.currentStage)
        {
            _stageData.currentStage++;
            _stageData.isFirst = true;
        }

        SceneManager.LoadScene(SceneName.LobbyScene);
    }
}