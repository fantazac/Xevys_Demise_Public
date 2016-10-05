using UnityEngine;
using System.Collections;

public class DetectPlayer : MonoBehaviour
{

    private bool _detectedPlayer;

    public bool DetectedPlayer { get { return _detectedPlayer; } set { _detectedPlayer = value; } }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            _detectedPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            _detectedPlayer = false;
        }
    }
}
