using System;
using UnityEngine;
using System.Collections;

public class RemoveObjectAfterPlayingSound : MonoBehaviour
{
    [SerializeField]
    private bool _destroyParent = false;

    private void Start()
    {
        GetComponent<PlaySoundOnTrigger>().OnSoundFinished += RemoveObject;
    }

    private void RemoveObject()
    {
        Destroy(gameObject);
        if (_destroyParent)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
