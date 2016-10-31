using UnityEngine;
using System.Collections;

public class PlaySound : MonoBehaviour
{

    private ActivateArtefactTrigger _trigger;

    private bool _soundStarted = false;

    private void Start()
    {
        _trigger = GetComponent<ActivateArtefactTrigger>();
        _trigger.OnTrigger += Play;
    }

    private void Play()
    {
        GetComponent<AudioSource>().Play();
        _soundStarted = true;
    }

    public bool SoundIsFinished()
    {
        return _soundStarted && !GetComponent<AudioSource>().isPlaying;
    }

}
