using UnityEngine;
using System.Collections;

public class ActivatePlatformTranslation : MonoBehaviour
{
    [SerializeField]
    private float _distanceToMovePlatform = -1000f;

    [SerializeField]
    private GameObject _flyingPlatform;

    private bool _soundPlayed = false;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_flyingPlatform != null)
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
