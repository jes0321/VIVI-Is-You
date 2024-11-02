using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Object : MonoBehaviour,IPushable
{
    private MoveCompo _moveCompo;
    public bool IsPushable { get; set; } = true;

    protected virtual void Awake()
    {
        _moveCompo = GetComponent<MoveCompo>();
    } 
    public bool MoveObject(Vector2 dir)
    {
       return _moveCompo.MoveAgent(dir);
    }
}

public enum VerbType
{
    Is,Has,Make
}