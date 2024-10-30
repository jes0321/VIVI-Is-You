using System;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Object : MonoBehaviour,IPushable
{
    private MoveCompo _moveCompo;
    public bool IsPushable { get; set; } = true;

    private void Awake()
    {
        _moveCompo = GetComponent<MoveCompo>();
    } 
    public bool MoveObject(Vector2 dir)
    {
       return _moveCompo.MoveAgent(dir);
    }
}
