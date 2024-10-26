using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    [SerializeField] private MapInfoSO _mapInfoSO;
    [SerializeField] private Tilemap _floor, _collider;
    private void Awake()
    {
        _mapInfoSO.Initalize(_floor, _collider);
    }
}
