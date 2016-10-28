using UnityEngine;
using System.Collections;

public class ActivateWaterPlatform : MonoBehaviour {

    [SerializeField]
    private GameObject _doorToDestroy;

    [SerializeField]
    private GameObject _doorToRetract;

    [SerializeField]
    private GameObject[] _wallsToActivate;

    [SerializeField]
    private GameObject _player;

    private bool _soundPlayed = false;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_doorToDestroy != null && _doorToRetract != null 
            && _player.GetComponent<InventoryManager>().WaterArtefactEnabled)
        {
            foreach (GameObject wall in _wallsToActivate)
            {
                wall.GetComponent<BoxCollider2D>().enabled = true;
            }

            GetComponent<AudioSource>().Play();
            _soundPlayed = true;
            gameObject.transform.position = new Vector3(-1000, -1000, 0);

            Destroy(_doorToDestroy);

            _doorToRetract.GetComponent<RetractDoor>().Retract = true;
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
