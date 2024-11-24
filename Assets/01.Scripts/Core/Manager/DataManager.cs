using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManger : MonoSingleton<DataManger>
{
    public StageData saveData;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        LoadData();

        SceneManager.sceneUnloaded += (scene)=>SaveData();
    }

    private void SaveData()
    {
        SaveManager.Save(saveData,"StageJson");

        LoadData();
    }

    private void LoadData()
    {
        saveData = SaveManager.Load<StageData>("StageJson");
        if (saveData == null)
        {
            saveData = new StageData();
            SaveData();
            Debug.Log("새로운 세이브 파일을 생성했습니다.");
        }
    }
    private void OnApplicationQuit()
    {
        SaveData();
    }
}

public class StageData
{
    public int currentStage=0;
    public bool isFirst = false;
    public float sfxVol = 1, bgmVol = 1,MasterVol=1;
}