using UnityEngine;

public class DontDestroyOnLoadStaticObjects : MonoBehaviour
{
    private static GameObjectTags _objectTags;
    private static DontDestroyOnLoadTags _dontDestroyOnLoadTags;
    private static GameObject _database;

    private void Start()
    {
        _dontDestroyOnLoadTags = GameObject.Find("DontDestroyOnLoadStaticObjects").GetComponent<DontDestroyOnLoadTags>();
        _database = GameObject.Find(_dontDestroyOnLoadTags.Database);
    }

    public static GameObject GetDatabase()
    {
        return _database;
    }
}
