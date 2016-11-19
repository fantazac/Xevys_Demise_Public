using System;
using UnityEngine;
using System.Collections;

public class RemoveObjectAfterPlayingSound : MonoBehaviour
{

    private void Start()
    {
        GetComponent<PlaySoundOnTrigger>().OnSoundFinished += RemoveObject;
    }

    private void RemoveObject()
    {
        Destroy(gameObject);
    }
}
