using UnityEngine;

public class CheckpointSave : MonoBehaviour
{
    [SerializeField]
    private int _checkpoint = 0;

    private AccountStatsDataHandler _accountStatsDataHandler;
    private SpawnToCheckpointAfterDeath _checkpointSpawn;

    private void Start()
    {
        _checkpointSpawn = StaticObjects.GetPlayer().GetComponent<SpawnToCheckpointAfterDeath>();
        _accountStatsDataHandler = DontDestroyOnLoadStaticObjects.GetDatabase().GetComponent<AccountStatsDataHandler>();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == StaticObjects.GetObjectTags().Player)
        {
            _accountStatsDataHandler.UpdateEntity();
            _checkpointSpawn.SaveCheckpoint(_checkpoint);
        }
    }
}
