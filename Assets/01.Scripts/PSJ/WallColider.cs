using UnityEngine;
using UnityEngine.Tilemaps;

public class WallCollider : MonoBehaviour
{

    private Tilemap _tilemap;

    [SerializeField] private Tile _groundTile;
    [SerializeField] private Tile _wallTile;

    private void Awake()
    {
        _tilemap = GetComponent<Tilemap>();
    }
}
