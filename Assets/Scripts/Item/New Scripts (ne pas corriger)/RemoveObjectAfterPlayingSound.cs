using UnityEngine;
using System.Collections;

public class RemoveObjectAfterPlayingSound : MonoBehaviour
{

    private PlaySoundOnTrigger _sound;

    private void Start()
    {
        _sound = GetComponent<PlaySoundOnTrigger>();
        _sound.OnSoundFinished += RemoveObject;
    }

    private void RemoveObject()
    {
        Destroy(gameObject);
    }

}
