using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RollBackManager : MonoBehaviour
{
    private List<MoveCompo> _moveCompos = new List<MoveCompo>();
    [SerializeField] private InputReader _inputReader;
    private void Awake()
    {
        FindObjectsByType<MoveCompo>(FindObjectsSortMode.None).ToList().ForEach(compo=>_moveCompos.Add(compo));

        _inputReader.OnRollbackEvent += HandleRollback;
    }
    private void HandleRollback()=>
        _moveCompos.ForEach(compo=>compo.HandleRollback());
}
