using UnityEngine;
using UnityEngine.UI;
using System;

public class PauseMenuAudioSettingsController : MonoBehaviour
{
    public delegate void OnVolumeChangedHandler(bool isMusic, float volume);
    public static event OnVolumeChangedHandler OnVolumeChanged;

    private Slider _musicVolumeSlider;
    private Slider _sfxVolumeSlider;
    private Switch _musicSwitch;

    private float _musicVolumeBeforeDesactivate;
    private float _sfxVolumeBeforeDesactivate;
    private float _sfxVolume;
    private PauseMenuAnimationManager _pauseMenuAnimationManager;
    private PauseMenuCurrentInterfaceAnimator _pauseMenuCurrentInterfaceAnimator;

    private void Start()
    {
        AccountSettings accountSettings = StaticObjects.GetDatabase().GetComponent<AccountSettings>();
        _pauseMenuAnimationManager = StaticObjects.GetPauseMenuPanel().GetComponent<PauseMenuAnimationManager>();
        _pauseMenuCurrentInterfaceAnimator = GameObject.Find(StaticObjects.GetFindTags().PauseMenuButtons).GetComponent<PauseMenuCurrentInterfaceAnimator>();
        accountSettings.OnMusicVolumeReloaded += ReloadMusicVolume;
        accountSettings.OnSfxVolumeReloaded += ReloadSfxVolume;
        _pauseMenuAnimationManager.OnPauseMenuStateChanged += FXVolumeStateInPauseMenu;
        _pauseMenuCurrentInterfaceAnimator.OnAudioInterfaceIsCurrent += FXVolumeInAudioInterface;

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
        _sfxVolume = _sfxVolumeSlider.value;
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

    private void ReloadMusicVolume(float volume)
    {
        _musicVolumeSlider.value = volume;
    }

    private void ReloadSfxVolume(float volume)
    {
        _sfxVolumeSlider.value = volume;
    }

    private void FXVolumeStateInPauseMenu(bool menuIsActive)
    {
        if (menuIsActive)
        {
            _sfxVolumeBeforeDesactivate = _sfxVolumeSlider.value;
            _sfxVolumeSlider.value = 0f;
        }
        else
        {
            _sfxVolumeSlider.value = _sfxVolume;
        }
    }

    private void FXVolumeInAudioInterface(bool isCurrent)
    {
        if (isCurrent)
        {
            _sfxVolumeSlider.value = _sfxVolumeBeforeDesactivate;
        }
        else
        {
            _sfxVolumeBeforeDesactivate = _sfxVolumeSlider.value;
            _sfxVolumeSlider.value = 0f;
        }
    }
}
