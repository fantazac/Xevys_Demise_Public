using UnityEngine;
using System.Collections;

public class ActivateAirPlatform : MonoBehaviour
{

    [SerializeField]
    private GameObject _flyingPlatform;

    [SerializeField]
    private GameObject _player;

    private bool _soundPlayed = false;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_flyingPlatform != null && _player.GetComponent<InventoryManager>().AirEnabled)
        {
            GetComponent<AudioSource>().Play();
            _soundPlayed = true;

            gameObject.transform.position = new Vector3(-1000, -1000, 0);

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
