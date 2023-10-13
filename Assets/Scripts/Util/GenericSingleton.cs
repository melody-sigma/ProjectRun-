using UnityEngine;

public class GenericSingleton<T> where T : MonoBehaviour
{
    private static T _instance;
    public static T getInstance()
    {
        if (_instance == null)
        {
            GameObject temp = new GameObject();
            _instance = temp.AddComponent<T>();
            Object.DontDestroyOnLoad(temp);
        }
        return _instance;
    }
}
