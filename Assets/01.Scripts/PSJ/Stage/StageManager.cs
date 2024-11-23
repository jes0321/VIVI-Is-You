using System.Linq;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private void Awake()
    {
        StageData data = DataManger.Instance.saveData;
        int btnNum = 1;
        GetComponentsInChildren<BtnEnable>().ToList().ForEach(btn =>
        {
            btn.Initialized(btnNum,data.currentStage,data.isFirst);
            btnNum++;
        });
        data.isFirst = false;
    }
}

public class SceneName
{
    public const string LobbyScene = "LobbyScene";
}