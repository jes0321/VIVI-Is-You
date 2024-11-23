using System.Collections.Generic;
using System.Data;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
    [SerializeField] private List<Transform> _titleTrms;

    [SerializeField] private float force = 0.1f;
    private int count = 1;
    private void Start()
    {
        foreach (Transform logo in _titleTrms)
        {
            float ran = Random.Range(force-0.05f, force+0.05f);
            float pos = count++%2 == 0 ?  ran: -ran;
            float curPos = logo.position.y;
            logo.position = new Vector3(logo.position.x, curPos+pos, logo.position.z);
            
            float time = Random.Range(3.5f,4.5f);
            logo.DOMoveY(curPos + -pos*2, time).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        }
    }

    public void StartBtn()
    {
        if (DataManger.Instance.saveData.currentStage == 0)
        {
            SceneManager.LoadScene("0");
        }
        else
        {
            SceneManager.LoadScene(SceneName.LobbyScene);
        }
    }
}
