using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private Tile _floorTile;
    
    private Tilemap _tileMap;

    private void Awake()
    {
        _tileMap = GetComponentInChildren<Tilemap>();
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            List<Agent> agents = new List<Agent>();
            FindObjectsByType<Agent>(FindObjectsSortMode.None).ToList().ForEach(agent =>
            {
                if(agent.AgentType == AgentType.WALL)
                    agents.Add(agent);
            });
            VerbApply(agents);
        }
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            List<Agent> agents = new List<Agent>();
            FindObjectsByType<Agent>(FindObjectsSortMode.None).ToList().ForEach(agent =>
            {
                if (agent.AgentType == AgentType.WALL)
                    agents.Add(agent);
            });
            VerbCancel(agents);
        }
    }

    public void VerbApply(List<Agent> list)
    {
        foreach (Agent agent in list)
        {
            Vector3Int vector = _tileMap.WorldToCell(agent.transform.position);
            _tileMap.SetTile(vector, _floorTile);
        }
    }

    public void VerbCancel(List<Agent> list)
    {
        foreach(Agent agent in list)
        {
            Vector3Int vector = _tileMap.WorldToCell(agent.transform.position);
            _tileMap.SetTile(vector, null);
        }
    }
}
