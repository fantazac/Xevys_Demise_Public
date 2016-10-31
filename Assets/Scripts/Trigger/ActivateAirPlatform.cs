using UnityEngine;
using System.Collections;

public class ActivateAirPlatform : MonoBehaviour
{
    [SerializeField]
    private float _distanceToMovePlatform = -1000f;
    [SerializeField]
    private GameObject _flyingPlatform;

    [SerializeField]
    private GameObject _player;

    private bool _soundPlayed = false;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_flyingPlatform != null && _player.GetComponent<InventoryManager>().AirArtefactEnabled)
        {
            GetComponent<AudioSource>().Play();
            _soundPlayed = true;

            gameObject.transform.position = new Vector3(_distanceToMovePlatform, _distanceToMovePlatform, 0);

            _flyingPlatform.GetComponent<EnablePlatform>().Move = true;
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
