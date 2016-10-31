using UnityEngine;
using System.Collections;

public class PlaySound : MonoBehaviour
{

    private ActivateTrigger _trigger;

    private bool _soundStarted = false;

    private void Start()
    {
        _trigger = GetComponent<ActivateTrigger>();
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
