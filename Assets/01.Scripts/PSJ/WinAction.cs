using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinAction : MonoSingleton<WinAction>
{
    [SerializeField] private GameObject _mapGrid;
    [SerializeField] private TextMeshProUGUI _winText;

    public void WinText()
    {
        _mapGrid.SetActive(false);
        _winText.enabled = true;
        StartCoroutine(WaitText());
    }
    
    IEnumerator WaitText()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneName.LobbyScene);
    }
}
