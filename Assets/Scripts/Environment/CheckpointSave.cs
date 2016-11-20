using UnityEngine;
using System.Collections;

public class CheckpointSave : MonoBehaviour
{
    private bool _showText;
    [SerializeField]
    private int _baseShowCounter = 200;
    [SerializeField]
    private int _checkpoint = 0;
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
            StaticObjects.GetPlayer().GetComponent<SpawnToCheckpointAfterDeath>().SaveCheckpoint(_checkpoint);
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
