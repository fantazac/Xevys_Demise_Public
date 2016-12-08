using UnityEngine;

public class TriggerRoomStateListener : MonoBehaviour
{
    [SerializeField]
    public ActivateTriggersOnRoomCleaned RoomCleaner;

    public bool State { get; private set; }

    private void Start()
    {
        if (GetComponent<ActivateTrigger>() == null)
        {
            GetComponent<Health>().OnDeath += RoomCleaned;
        }
        else
        {
            GetComponent<ActivateTrigger>().OnTrigger += RoomCleaned;
        }
    }

    public void RoomCleaned()
    {
        State = true;
    }
}