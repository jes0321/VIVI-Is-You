using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StopVerbable : Object,IVerbable
{
    [SerializeField] private MapInfoSO _mapInfo;
    [SerializeField] private Tile _tile;
    public void VerbApply(List<Agent> agents)
    {
        foreach (Agent agent in agents)
        {
            Vector3Int vector = _mapInfo.ColliderTilemap.WorldToCell(agent.transform.position);
            _mapInfo.ColliderTilemap.SetTile(vector, _tile);
        }
    }
    public void VerbCancel(List<Agent> agents)
    {
        foreach(Agent agent in agents)
        {
            Vector3Int vector = _mapInfo.ColliderTilemap.WorldToCell(agent.transform.position);
            _mapInfo.ColliderTilemap.SetTile(vector, null);
        }
    }
}