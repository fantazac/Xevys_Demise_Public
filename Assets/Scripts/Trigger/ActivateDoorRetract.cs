using UnityEngine;
using System.Collections;

public class ActivateDoorRetract : MonoBehaviour
{
    [SerializeField]
    private float _distanceToMoveDoor = -1000f;

    [SerializeField]
    private string[] _weaponTags = { "AxeBlade", "AxeHandle", "Knife" };

    [SerializeField]
    private GameObject _door;

    [SerializeField]
    private GameObject[] _wallsToActivate;

    private bool _soundPlayed = false;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        for (int tag = 0; tag < _weaponTags.Length; tag++)
        {
            if (_door != null && collider.gameObject.tag == _weaponTags[tag])
        {
                foreach (GameObject wall in _wallsToActivate)
                {
                    wall.GetComponent<BoxCollider2D>().enabled = true;
                }

                GetComponent<AudioSource>().Play();
                _soundPlayed = true;
                gameObject.transform.position = new Vector3(_distanceToMoveDoor, _distanceToMoveDoor, 0);

                _door.GetComponent<RetractDoor>().Retract = true;
            }
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
