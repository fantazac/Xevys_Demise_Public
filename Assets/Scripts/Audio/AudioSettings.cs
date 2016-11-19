using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class AudioSettings : MonoBehaviour
{
    public delegate void OnVolumeChangedHandler(bool isMusic, float volume);
    public static event OnVolumeChangedHandler OnVolumeChanged;

    private Slider _musicVolumeSlider;
    private Slider _fxVolumeSlider;
    private Switch _musicSwitch;

    private float _musicVolumeBeforeDesactivate;

    private void Start()
    {
        _musicSwitch = GetComponentInChildren<Switch>();
        Slider[] sliders = GetComponentsInChildren<Slider>();
        _musicVolumeSlider = sliders[0];
        _fxVolumeSlider = sliders[1];
    }

    public void SetMusicVolume(Single volume)
    {
        if (!_musicSwitch.isOn && _musicVolumeSlider.value > 0f)
        {
            _musicVolumeBeforeDesactivate = _musicVolumeSlider.value;
            _musicSwitch.isOn = true;
        }
        OnVolumeChanged(true, volume);
    }

    public void SetSoundVolume(Single volume)
    {
        OnVolumeChanged(false, volume);
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
