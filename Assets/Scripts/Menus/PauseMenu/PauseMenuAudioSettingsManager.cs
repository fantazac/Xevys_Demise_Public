using UnityEngine;
using UnityEngine.UI;
using System;

public class PauseMenuAudioSettingsManager : MainMenuAudioSettingsManager
{
    public static float _sfxVolume { get; private set; }
    private PauseMenuAnimationManager _pauseMenuAnimationManager;
    private PauseMenuCurrentInterfaceAnimator _pauseMenuCurrentInterfaceAnimator;

    protected override void Start()
    {
        base.Start();
        _pauseMenuAnimationManager = StaticObjects.GetPauseMenuPanel().GetComponent<PauseMenuAnimationManager>();
        _pauseMenuCurrentInterfaceAnimator = GameObject.Find(StaticObjects.GetMainObjects().PauseMenuButtons).GetComponent<PauseMenuCurrentInterfaceAnimator>();
        _pauseMenuAnimationManager.OnPauseMenuStateChanged += FXVolumeStateInPauseMenu;
        _pauseMenuCurrentInterfaceAnimator.OnAudioInterfaceIsCurrent += FXVolumeInAudioInterface;
        DontDestroyOnLoadStaticObjects.GetDatabase().GetComponent<AccountSettingsDataHandler>().ReloadSettings();
        _sfxVolume = 1f;
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
