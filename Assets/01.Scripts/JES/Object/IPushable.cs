using System;
using UnityEngine;

public class IPushable :MonoBehaviour
{
    public MoveCompo moveCompo{get;set;}

    public void Initalize(MoveCompo move)
    {
        moveCompo = move;
    }
    public void MoveSubject(Vector2 dir)
    {
        moveCompo.MoveAgent(dir);
    }
}
