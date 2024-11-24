using System.Collections;
using TMPro;
using UnityEngine;

public class WinAction : MonoSingleton<WinAction>
{
    [SerializeField] private GameObject _mapGrid;
    [SerializeField] private TextMeshProUGUI _winText;

    public void WinText()
    {
        _mapGrid.SetActive(false);
        _winText.enabled = true;
    }
}
