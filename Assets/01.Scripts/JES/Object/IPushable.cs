using System;
using UnityEngine;

public interface IPushable
{
    public bool IsPushable { get; set; }
    public void MoveObject(Vector2 dir);
}
