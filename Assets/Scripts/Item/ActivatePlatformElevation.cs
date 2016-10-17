﻿using UnityEngine;
using System.Collections;

public class ActivatePlatformElevation : MonoBehaviour
{

    [SerializeField]
    private GameObject _flyingPlatform;

    private bool _soundPlayed = false;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_flyingPlatform != null)
        {
            GetComponent<AudioSource>().Play();
            _soundPlayed = true;

            gameObject.transform.position = new Vector3(-1000, -1000, 0);

            _flyingPlatform.GetComponent<ElevateFlyingPlatform>().Elevate = true;
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
