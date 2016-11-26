using UnityEngine;

public class CheckpointSave : MonoBehaviour
{
    [SerializeField]
    private int _baseShowCounter = 200;

    [SerializeField]
    private int _checkpoint = 0;

    private bool _showText;
    private int _showCounter;
    private string _gameSaved = "Game saved";

    private AccountStats _accountStats;
    private SpawnToCheckpointAfterDeath _checkpointSpawn;

    private void Start()
    {
        _checkpointSpawn = StaticObjects.GetPlayer().GetComponent<SpawnToCheckpointAfterDeath>();
        _accountStats = StaticObjects.GetDatabase().GetComponent<AccountStats>();
    }

    private void Update()
    {
        if(_showText)
        {
            _showCounter--;
            if(_showCounter <= 0)
            {
                _showText = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == StaticObjects.GetUnityTags().Player)
        {
            _showText = true;
            _showCounter = _baseShowCounter;
            _accountStats.SaveTemporaryStats();
            _checkpointSpawn.SaveCheckpoint(_checkpoint);
        }
    }

    private void OnGUI()
    {
        if(_showText)
        {
            GUI.Label(new Rect(30, 40, 100, 20), _gameSaved);
        }
    }
}
