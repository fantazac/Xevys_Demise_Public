using UnityEngine;

public class DontDestroyOnLoadTags : MonoBehaviour
{
    public string Database { get; private set; }

    private void Start()
    {
        Database = "Database";
    }
}
