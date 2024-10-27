using System;
using UnityEngine;

public abstract class Object : MonoBehaviour,IPushable
{
    private MoveCompo _moveCompo;
    public bool IsPushable { get; set; } = true;

    private void Awake()
    {
        _moveCompo = GetComponent<MoveCompo>();
    } 
    public void MoveObject(Vector2 dir)
    {
        _moveCompo.MoveAgent(dir);
    }
}
