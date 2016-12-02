using UnityEngine;

public class CheckpointSave : MonoBehaviour
{
    [SerializeField]
    private int _baseShowCounter = 200;

    [SerializeField]
    private int _checkpoint = 0;

    private AccountStats _accountStats;
    private SpawnToCheckpointAfterDeath _checkpointSpawn;

    private void Start()
    {
        _checkpointSpawn = StaticObjects.GetPlayer().GetComponent<SpawnToCheckpointAfterDeath>();
        _accountStats = StaticObjects.GetDatabase().GetComponent<AccountStats>();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == StaticObjects.GetUnityTags().Player)
        {
            _accountStats.SaveTemporaryStats();
            _checkpointSpawn.SaveCheckpoint(_checkpoint);
        }
    }
}
