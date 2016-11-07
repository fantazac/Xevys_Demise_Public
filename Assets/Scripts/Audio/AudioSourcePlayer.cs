using UnityEngine;
using System.Collections;

public class AudioSourcePlayer : MonoBehaviour
{
    private AudioSource[] _audioSources;

    private void Start()
    {
        _audioSources = GetComponents<AudioSource>();
    }

    public void Play()
    {
        _audioSources[0].Play();
    }

    public void Play(int audioSourceIndex)
    {
        _audioSources[audioSourceIndex].Play();
    }

    public bool IsPlaying()
    {
        return _audioSources[0].isPlaying;
    }

    public void Stop(int audioSourceIndex)
    {
        _audioSources[audioSourceIndex].Stop();
    }

    public void StopAll()
    {
        foreach(AudioSource audioSource in _audioSources)
        {
            audioSource.Stop();
        }
    }

    public AudioSource GetAudioSource()
    {
        return _audioSources[0];
    }

    public AudioSource GetAudioSource(int index)
    {
        return _audioSources[index];
    }
}
