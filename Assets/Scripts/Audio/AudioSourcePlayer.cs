using UnityEngine;

public class AudioSourcePlayer : MonoBehaviour
{
    private AudioSource[] _audioSources;

    private void Start()
    {
        _audioSources = GetComponents<AudioSource>();
    }

    public void Play()
    {
        Play(0);
    }

    public void Play(int audioSourceIndex)
    {
        _audioSources[audioSourceIndex].Play();
    }

    public bool IsPlaying()
    {
        return IsPlaying(0);
    }

    public bool IsPlaying(int audioSourceIndex)
    {
        return _audioSources[audioSourceIndex].isPlaying;
    }

    public void Stop()
    {
        Stop(0);
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
        return GetAudioSource(0);
    }

    public AudioSource GetAudioSource(int index)
    {
        return _audioSources[index];
    }

    public int GetAudioSourcesLength()
    {
        return _audioSources.Length;
    }
}
