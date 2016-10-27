﻿using UnityEngine;
using System.Collections;

public class AudioSourcePlayer : MonoBehaviour
{
    private AudioSource[] _audioSources;

    private void Start()
    {
        _audioSources = GetComponents<AudioSource>();
    }
    
    public void Play(int audioSourceIndex)
    {
        _audioSources[audioSourceIndex].Play();
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
}