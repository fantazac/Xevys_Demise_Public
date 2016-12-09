using UnityEngine;

public class LoadDatabaseAccount : MonoBehaviour
{
    public void ChangeDatabaseAccount()
    {
        DontDestroyOnLoadStaticObjects.GetDatabase().GetComponent<DatabaseController>().ChangeAccount();
    }
}
