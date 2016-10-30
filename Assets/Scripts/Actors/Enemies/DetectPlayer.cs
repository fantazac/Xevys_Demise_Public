using UnityEngine;
using System.Collections;

public class DetectPlayer : MonoBehaviour
{
    public bool DetectedPlayer { get; private set; }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            DetectedPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            DetectedPlayer = false;
        }
    }
}
