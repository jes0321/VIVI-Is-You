using UnityEngine;
using System.IO;

public static class SaveManager
{
    private static readonly string JsonPath = Application.dataPath + "/SaveData/";

    private static string GetFilePath(string fileName)
    {
        return Path.Combine(JsonPath, $"{fileName}.json");
    }
    public static void Save<T>(T obj, string jsonFileName)
    {
        if (!Directory.Exists(JsonPath))
        {
            Directory.CreateDirectory(JsonPath);
            Debug.Log("저장경로" + JsonPath);
        }
        string path = JsonPath + jsonFileName + ".json";
        string json = JsonUtility.ToJson(obj);
        File.WriteAllText(path,json);
    }

    public static T Load<T>(string jsonFileName)
    {
        string path = GetFilePath(jsonFileName);
        if (!File.Exists(path))
        {
            T nullobj = default;
            JsonUtility.ToJson(nullobj);
            return nullobj;
        }


        string L = File.ReadAllText(path);
        T OBJ = JsonUtility.FromJson<T>(L);
        return OBJ;
    }
}