using UnityEngine;
using System.Collections;

public class CheckpointSave : MonoBehaviour
{
    bool _showText;
    [SerializeField]
    int _baseShowCounter = 200;
    int _showCounter;

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
            GameObject.FindGameObjectWithTag("Database").GetComponent<Database>().SaveStats();
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
