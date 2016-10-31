using UnityEngine;
using System.Collections;

public class RemoveObjectAfterPlayingSound : MonoBehaviour
{

    private PlaySound _sound;

    private ActivateArtefactTrigger _trigger;

    private bool _deleteOnSoundFinished = false;

    private void Start()
    {
        _trigger = GetComponent<ActivateArtefactTrigger>();
        _trigger.OnTrigger += PrepareRemovalOfObject;

        _sound = GetComponent<PlaySound>();
    }

    private void PrepareRemovalOfObject()
    {
        _deleteOnSoundFinished = true;
    }

    private void FixedUpdate()
    {
        if (_deleteOnSoundFinished && _sound.SoundIsFinished())
        {
            Destroy(gameObject);
        }
    }

}
