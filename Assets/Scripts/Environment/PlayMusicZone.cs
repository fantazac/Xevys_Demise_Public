using UnityEngine;
using System.Collections;

public class PlayMusicZone : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == StaticObjects.GetUnityTags().Player)
        {
            _audioSource.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == StaticObjects.GetUnityTags().Player)
        {
            _audioSource.Stop();
        }
    }
}
