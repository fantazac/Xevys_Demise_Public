using UnityEngine;
using System.Collections;

public enum Checkpoints
{
    HubCheckpoint,
    EarthCheckpoint,
    AirCheckpoint,
    WaterCheckpoint,
    FireCheckpoint
}

public class CheckpointSave : MonoBehaviour
{
    private bool _showText;
    [SerializeField]
    private int _baseShowCounter = 200;
    [SerializeField]
    private Checkpoints _checkpointIdentifier = Checkpoints.HubCheckpoint;
    private int _showCounter;

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
        if (coll.gameObject.tag == "Player")
        {
            _showText = true;
            _showCounter = _baseShowCounter;
            StaticObjects.GetDatabase().GetComponent<Database>().SaveTemporaryStats();
            StaticObjects.GetPlayer().GetComponent<SpawnToCheckpointAfterDeath>().SaveCheckpoint(_checkpointIdentifier);
        }
    }

    private void OnGUI()
    {
        if(_showText)
        {
            GUI.Label(new Rect(30, 40, 100, 20), "Game saved");
        }
    }
}
