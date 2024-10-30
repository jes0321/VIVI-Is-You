using System;
using UnityEngine;

public interface IPushable
{
    public bool IsPushable { get; set; }
    public bool MoveObject(Vector2 dir);
}
