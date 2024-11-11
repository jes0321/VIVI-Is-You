using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public List<StageData> stages;
    private bool[] stageUnlocked;

    private void Awake()
    {
        stageUnlocked = new bool[stages.Count];
        UnlockStage(0);
        IsStageUnlocked(0);
    }

    public void UnlockStage(int stageIndex)
    {
        if (stageIndex < stageUnlocked.Length)
        {
            for (int i = 0; i <= stageIndex; i++)
            {
                stageUnlocked[i] = true;
                Debug.Log(i + "스테이지 잠금 해제");
            }
        }
    }

    public bool IsStageUnlocked(int stageIndex)
    {
        return stageIndex < stageUnlocked.Length && stageUnlocked[stageIndex];
    }

    public void CompleteStage(int stageIndex)
    {
        if (stageIndex + 1 < stages.Count)
        {
            UnlockStage(stageIndex + 1);
        }
    }
}
