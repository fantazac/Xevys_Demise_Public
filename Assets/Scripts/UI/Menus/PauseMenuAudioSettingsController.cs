using UnityEngine;
using UnityEngine.UI;
using System;

public class PauseMenuAudioSettingsController : MonoBehaviour
{
    public delegate void OnVolumeChangedHandler(bool isMusic, float volume);
    public static event OnVolumeChangedHandler OnVolumeChanged;
    public delegate void OnMusicStateChangedHandler(bool state);
    public static event OnMusicStateChangedHandler OnMusicStateChanged;

    private Slider _musicVolumeSlider;
    private Slider _sfxVolumeSlider;
    private Switch _musicSwitch;

    private float _musicVolumeBeforeDesactivate;

    private void Start()
    {
        AccountSettings accountSettings = StaticObjects.GetDatabase().GetComponent<AccountSettings>();
        accountSettings.OnMusicVolumeReloaded += ReloadMusicVolume;
        accountSettings.OnSfxVolumeReloaded += ReloadSfxVolume;
        accountSettings.OnMusicStateReloaded += ReloadMusicState;

        _musicSwitch = GetComponentInChildren<Switch>();
        Slider[] sliders = GetComponentsInChildren<Slider>();
        _sfxVolumeSlider = sliders[0];
        _musicVolumeSlider = sliders[1];
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
        OnMusicStateChanged(activate);
    }

    private void ReloadMusicVolume(float volume)
    {
        _musicVolumeSlider.value = volume;
    }

    private void ReloadSfxVolume(float volume)
    {
        _sfxVolumeSlider.value = volume;
    }

    private void ReloadMusicState(bool state)
    {
        _musicSwitch.isOn = state;
    }
}
