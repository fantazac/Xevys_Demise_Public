using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicVolume : MonoBehaviour
{
    private List<AudioSource> _musicZonesAudio;

    private void Start()
    {
        _musicZonesAudio = new List<AudioSource>();
        GameObject[] musicZones = GameObject.FindGameObjectsWithTag("MusicZone");
        foreach(GameObject musicZone in musicZones)
        {
            _musicZonesAudio.Add(musicZone.GetComponent<AudioSource>());
        }
    }

    public void SetVolume(float volume)
    {
        foreach(AudioSource music in _musicZonesAudio)
        {
            music.volume = volume;
        }
    }
}
