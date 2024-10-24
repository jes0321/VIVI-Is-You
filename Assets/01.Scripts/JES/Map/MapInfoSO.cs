using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "MapInfo", menuName = "SO/MapInfo")]
public class MapInfoSO : ScriptableObject
{
    private Tilemap _floorTilemap,_colliderTilemap;

    public void Initalize(Tilemap floorTilemap, Tilemap colliderTilemap)
    {
        _floorTilemap = floorTilemap;
        _colliderTilemap = colliderTilemap;

    }

    public Vector3 CellCenterPos(Vector3 pos, Vector2Int dir)
    {
        Vector3Int curPos = _floorTilemap.WorldToCell(pos);

        Vector3Int nextPos = curPos + new Vector3Int(dir.x,dir.y,0);
        
        Vector3 worldPos = _floorTilemap.GetCellCenterWorld(nextPos);
        
        return worldPos;
    }
}
