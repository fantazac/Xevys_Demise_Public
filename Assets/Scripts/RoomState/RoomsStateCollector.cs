using System.Collections.Generic;
using UnityEngine;

public class RoomsStateCollector : MonoBehaviour
{
    [SerializeField]
    public List<TriggerRoomStateListener> RoomCleanerTriggers;

    public delegate void OnMainLevelStartedHandler();
    public static event OnMainLevelStartedHandler OnMainLevelStarted;

    private void Start()
    {
        OnMainLevelStarted();
    }
}
