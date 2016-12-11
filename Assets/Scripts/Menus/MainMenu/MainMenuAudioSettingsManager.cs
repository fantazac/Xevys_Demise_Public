using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuAudioSettingsManager : MonoBehaviour
{
    public delegate void OnVolumeChangedHandler(bool isMusic, float volume);
    public static event OnVolumeChangedHandler OnVolumeChanged;
    public delegate void OnMusicStateChangedHandler(bool state);
    public static event OnMusicStateChangedHandler OnMusicStateChanged;

    protected Slider _musicVolumeSlider;
    protected Slider _sfxVolumeSlider;
    protected Switch _musicSwitch;

    protected float _musicVolumeBeforeDesactivate;
    public static float _sfxVolume { get; protected set; }
    public float _sfxVolumeBeforeDesactivate { get; protected set; }
    protected bool _sfxVolumeChanged;
    protected AccountSettingsDataHandler _accountSettingsDataHandler;

    protected virtual void Start()
    {
        _accountSettingsDataHandler = DontDestroyOnLoadStaticObjects.GetDatabase().GetComponent<AccountSettingsDataHandler>();
        _accountSettingsDataHandler.OnMusicVolumeReloaded += ReloadMusicVolume;
        _accountSettingsDataHandler.OnSfxVolumeReloaded += ReloadSfxVolume;
        _accountSettingsDataHandler.OnMusicStateReloaded += ReloadMusicState;
        
        _musicSwitch = GetComponentInChildren<Switch>();
        Slider[] sliders = GetComponentsInChildren<Slider>();
        _sfxVolumeSlider = sliders[0];
        _musicVolumeSlider = sliders[1];
        _sfxVolumeChanged = false;

        _sfxVolume = 1f;
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

    protected void ReloadMusicVolume(float volume)
    {
        _musicVolumeSlider.value = volume;
    }

    protected void ReloadSfxVolume(float volume)
    {
        _sfxVolumeSlider.value = volume;
    }

    protected void ReloadMusicState(bool state)
    {
        _musicSwitch.isOn = state;
    }
}
