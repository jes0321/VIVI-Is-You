using System;
using UnityEngine;

public class WinCollider : MonoBehaviour, IAgentCompo
{
    private BoxCollider2D _collider;
    
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
            Debug.Log("니 성공해따 다음 스테이지 가라~");
        }
    }
}