using UnityEngine;
using System.Collections;

public class PlaySoundOnTrigger : MonoBehaviour
{
    private AudioSourcePlayer _audioSourcePlayer;
    
    public delegate void OnSoundFinishedHandler();
    public event OnSoundFinishedHandler OnSoundFinished;

    private float _soundClipLength;

    private WaitForSeconds _finishedSoundDelay;

    private void Start()
    {
        GetComponent<ActivateTrigger>().OnTrigger += Play;

        _audioSourcePlayer = GetComponent<AudioSourcePlayer>();

        _soundClipLength = _audioSourcePlayer.GetAudioSource().clip.length;
        _finishedSoundDelay = new WaitForSeconds(_soundClipLength);
    }

    private void Play()
    {
        _audioSourcePlayer.Play();
        StartCoroutine(SoundIsFinished());
    }

    private IEnumerator SoundIsFinished()
    {
        yield return _finishedSoundDelay;

        OnSoundFinished();
    }

}
