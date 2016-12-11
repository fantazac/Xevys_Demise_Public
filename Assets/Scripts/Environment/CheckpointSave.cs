using UnityEngine;

public class CheckpointSave : MonoBehaviour
{
    [SerializeField]
    private int _checkpoint = 0;

    public delegate void OnCheckpointReachedHandler();
    public event OnCheckpointReachedHandler OnCheckpointReached;

    private AccountStatsDataHandler _accountStatsDataHandler;
    private AccountRoomStateDataHandler _accountRoomStateDataHandler;
    private SpawnToCheckpointAfterDeath _checkpointSpawn;

    private void Start()
    {
        _checkpointSpawn = StaticObjects.GetPlayer().GetComponent<SpawnToCheckpointAfterDeath>();
        _accountStatsDataHandler = DontDestroyOnLoadStaticObjects.GetDatabase().GetComponent<AccountStatsDataHandler>();
        _accountRoomStateDataHandler = DontDestroyOnLoadStaticObjects.GetDatabase().GetComponent<AccountRoomStateDataHandler>();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == StaticObjects.GetObjectTags().Player)
        {
            _accountStatsDataHandler.UpdateEntity();
            _accountRoomStateDataHandler.UpdateEntity();
            _checkpointSpawn.SaveCheckpoint(_checkpoint);
            OnCheckpointReached();
        }
    }
}
