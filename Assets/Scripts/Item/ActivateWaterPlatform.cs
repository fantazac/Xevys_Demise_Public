using UnityEngine;
using System.Collections;

public class ActivateWaterPlatform : MonoBehaviour {

    [SerializeField]
    private GameObject _door;

    [SerializeField]
    private GameObject _player;

    private bool _soundPlayed = false;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_door != null && _player.GetComponent<InventoryManager>().WaterEnabled)
        {
            GetComponent<AudioSource>().Play();
            _soundPlayed = true;
            gameObject.transform.position = new Vector3(-1000, -1000, 0);

            _door.GetComponent<RetractDoor>().Retract = true;
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
