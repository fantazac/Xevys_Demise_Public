using UnityEngine;
using System.Collections;

public class OpenDoorTrigger : MonoBehaviour
{

    [SerializeField]
    private GameObject _door;

    [SerializeField]
    private GameObject[] _wallsToActivate;

    private bool _soundPlayed;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_door != null && (collider.gameObject.tag == "AxeBlade" || collider.gameObject.tag == "AxeHandle" || collider.gameObject.tag == "Knife"))
        {
            foreach (GameObject wall in _wallsToActivate)
            {
                wall.GetComponent<BoxCollider2D>().enabled = true;
            }

            GetComponent<AudioSource>().Play();
            _soundPlayed = true;
            gameObject.transform.position = new Vector3(-1000, -1000, 0);

            Destroy(_door);
        }
    }

    void FixedUpdate()
    {
        if (_soundPlayed && !GetComponent<AudioSource>().isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
