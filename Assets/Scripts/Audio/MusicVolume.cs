using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MusicVolume : MonoBehaviour
{
    private List<AudioSource> _musicZonesAudio;
    private Slider _musicVolumeSlider;
    private Switch _musicSwitch;

    private const float INITIAL_MUSIC_VOLUME = 1f;
    private float _musicVolumeBeforeDesactivate;

    private void Start()
    {
        _musicZonesAudio = new List<AudioSource>();
        _musicVolumeSlider = GetComponent<Slider>();
        _musicSwitch = GameObject.Find("MusicSwitch").GetComponent<Switch>();

        _musicVolumeSlider.value = INITIAL_MUSIC_VOLUME;
        SetVolume(INITIAL_MUSIC_VOLUME);

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

        if (!_musicSwitch.isOn && _musicVolumeSlider.value > 0f)
        {
            _musicVolumeBeforeDesactivate = _musicVolumeSlider.value;
            _musicSwitch.isOn = true;
        }
    }

    public void MusicState(bool activate)
    {
        if (activate)
        {
            _musicVolumeSlider.value = _musicVolumeBeforeDesactivate;
        }
        else
        {
            _musicVolumeBeforeDesactivate = _musicVolumeSlider.value;
            _musicVolumeSlider.value = 0f;
        }
    }
}
