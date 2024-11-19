using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCollider : MonoBehaviour, IAgentCompo
{
    private BoxCollider2D _collider;
    [SerializeField] private StageData _stageData;
    private string _stageName => SceneManager.GetActiveScene().name;
    
    public void Initialize(Agent agent)
    {
        _collider = GetComponent<BoxCollider2D>();
    }
    public void ToggleWinCollider(bool value)
    {
        _collider.enabled = value;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Agent>()._isYouState)
        {
            if(int.Parse(_stageName) == _stageData.currentStage)
            {
                _stageData.currentStage++;
                _stageData.isFirst = true;
            }
            SceneManager.LoadScene(SceneName.LobbyScene);
        }
    }
}