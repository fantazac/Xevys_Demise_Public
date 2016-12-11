using UnityEngine;
using UnityEngine.UI;
using System;

public class PauseMenuAudioSettingsManager : MainMenuAudioSettingsManager
{
    
    private PauseMenuAnimationManager _pauseMenuAnimationManager;
    private PauseMenuCurrentInterfaceAnimator _pauseMenuCurrentInterfaceAnimator;

    protected override void Start()
    {
        base.Start();
        _pauseMenuAnimationManager = StaticObjects.GetPauseMenuPanel().GetComponent<PauseMenuAnimationManager>();
        _pauseMenuCurrentInterfaceAnimator = GameObject.Find(StaticObjects.GetMainObjects().PauseMenuButtons).GetComponent<PauseMenuCurrentInterfaceAnimator>();
        _pauseMenuAnimationManager.OnPauseMenuStateChanged += FXVolumeStateInPauseMenu;
        _pauseMenuCurrentInterfaceAnimator.OnAudioInterfaceIsCurrent += FXVolumeInAudioInterface;
        OnVolumeChanged += ChangeSfxVolume;
        DontDestroyOnLoadStaticObjects.GetDatabase().GetComponent<AccountSettingsDataHandler>().ReloadSettings();
    }

    private void ChangeSfxVolume(bool isMusic, float volume)
    {
        if (!isMusic)
        {
            _sfxVolume = volume;
        }
    }

    private void FXVolumeStateInPauseMenu(bool menuIsActive, bool isDead)
    {
        if (!isDead)
        {
            if (menuIsActive)
            {
                _sfxVolumeBeforeDesactivate = _sfxVolumeSlider.value;
                _sfxVolumeSlider.value = 0f;
            }
            else if (!_sfxVolumeChanged)
            {
                _sfxVolumeSlider.value = _sfxVolumeBeforeDesactivate;
            }
            else
            {
                _sfxVolumeChanged = false;
            }
        }
    }

    private void FXVolumeInAudioInterface(bool isCurrent)
    {
        if (isCurrent)
        {
            _sfxVolumeChanged = true;
            _sfxVolumeSlider.value = _sfxVolumeBeforeDesactivate;
        }
        else
        {
            _sfxVolumeBeforeDesactivate = _sfxVolumeSlider.value;
            _sfxVolumeSlider.value = 0f;
            _sfxVolumeChanged = false;
        }
    }
}
