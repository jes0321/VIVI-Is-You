using System.Collections;
using TMPro;
using UnityEngine;

public class WinAction : MonoSingleton<WinAction>
{
    [SerializeField] private GameObject _mapGrid;
    [SerializeField] private TextMeshProUGUI _winText;

    public void InitializeGrid()
    {
        _mapGrid.SetActive(false);
        _winText.enabled = true;
    }

    public void WinText()
    {
        InitializeGrid();
    }
}
