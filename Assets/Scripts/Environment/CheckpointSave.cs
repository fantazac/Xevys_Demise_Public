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
            StaticObjects.GetDatabase().GetComponent<AccountStats>().SaveTemporaryStats();
            StaticObjects.GetPlayer().GetComponent<SpawnToCheckpointAfterDeath>().SaveCheckpoint(_checkpoint);
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
