using UnityEngine;
using System.Collections;

public class PlaySoundOnTrigger : MonoBehaviour
{

    private ActivateTrigger _trigger;

    private AudioSourcePlayer _audioSourcePlayer;
    
    public delegate void OnSoundFinishedHandler();
    public event OnSoundFinishedHandler OnSoundFinished;

    private void Start()
    {
        _trigger = GetComponent<ActivateTrigger>();
        _trigger.OnTrigger += Play;

        _audioSourcePlayer = GetComponent<AudioSourcePlayer>();
    }

    private void Play()
    {
        _audioSourcePlayer.Play();
        Invoke("SoundIsFinished", _audioSourcePlayer.GetAudioSource().clip.length);
    }

    private void SoundIsFinished()
    {
        OnSoundFinished();
    }

}
