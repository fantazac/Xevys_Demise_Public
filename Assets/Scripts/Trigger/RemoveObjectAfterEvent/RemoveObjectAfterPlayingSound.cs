using UnityEngine;
using System.Collections;

public class RemoveObjectAfterPlayingSound : RemoveObjectAfterEvent
{
    protected void Start()
    {
        GetComponent<PlaySoundOnTrigger>().OnSoundFinished += RemoveObject;
    }
}
