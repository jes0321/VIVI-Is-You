using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public StageData stageData;

    private void Awake()
    {
        int btnNum = 1;
        GetComponentsInChildren<BtnEnable>().ToList().ForEach(btn =>
        {
            btn.Initialized(btnNum,stageData.currentStage,stageData.isFirst);
            btnNum++;
        });
        stageData.isFirst = false;
    }
}