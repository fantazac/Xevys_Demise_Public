using System.Collections.Generic;
using UnityEngine;

public class RoomsStateCollector : MonoBehaviour
{
    [SerializeField]
    public List<TriggerRoomStateListener> RoomCleanerTriggers;

    private void Start()
    {
        GameObject database = DontDestroyOnLoadStaticObjects.GetDatabase();
        database.GetComponent<AccountRoomStateDataHandler>().LoadRoomsWithTrigger();
        database.GetComponent<DatabaseController>().ChangeAccountRoomState();
    }
}
