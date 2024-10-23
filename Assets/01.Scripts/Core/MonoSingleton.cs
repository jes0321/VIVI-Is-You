using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance = null;
    private static bool IsDestroyed = false;

    [SerializeField] protected static bool _isDontDestroyOnLoad = false;
    
    public static T Instance
    {
        get
        {
            if (IsDestroyed)
                _instance = null;

            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                    Debug.LogError($"{typeof(T).Name} singleton is not exist");
                else
                {
                    if(_isDontDestroyOnLoad)
                        DontDestroyOnLoad(_instance);
                    IsDestroyed = false;
                }
            }
            return _instance;
        }
    }

    private void OnDisable()
    {
        IsDestroyed = true;
    }
}
