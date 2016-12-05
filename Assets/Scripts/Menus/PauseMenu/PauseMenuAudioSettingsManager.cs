using UnityEngine;
using UnityEngine.UI;
using System;

public class PauseMenuAudioSettingsManager : MainMenuAudioSettingsManager
{
    public delegate void OnVolumeChangedHandler(bool isMusic, float volume);
    public static event OnVolumeChangedHandler OnVolumeChanged;
    public delegate void OnMusicStateChangedHandler(bool state);
    public static event OnMusicStateChangedHandler OnMusicStateChanged;

    private float _musicVolumeBeforeDesactivate;
    public static float _sfxVolume { get; private set; }
    public float _sfxVolumeBeforeDesactivate { get; private set; }
    private bool _sfxVolumeChanged;
    private PauseMenuAnimationManager _pauseMenuAnimationManager;
    private PauseMenuCurrentInterfaceAnimator _pauseMenuCurrentInterfaceAnimator;
    private AccountSettingsDataHandler _accountSettingsDataHandler;

    protected override void Start()
    {
        base.Start();
        _accountSettingsDataHandler = StaticObjects.GetDatabase().GetComponent<AccountSettingsDataHandler>();
        _accountSettingsDataHandler.OnMusicVolumeReloaded += ReloadMusicVolume;
        _accountSettingsDataHandler.OnSfxVolumeReloaded += ReloadSfxVolume;
        _accountSettingsDataHandler.OnMusicStateReloaded += ReloadMusicState;
        _pauseMenuAnimationManager = StaticObjects.GetPauseMenuPanel().GetComponent<PauseMenuAnimationManager>();
        _pauseMenuCurrentInterfaceAnimator = GameObject.Find(StaticObjects.GetMainObjects().PauseMenuButtons).GetComponent<PauseMenuCurrentInterfaceAnimator>();
        _pauseMenuAnimationManager.OnPauseMenuStateChanged += FXVolumeStateInPauseMenu;
        _pauseMenuCurrentInterfaceAnimator.OnAudioInterfaceIsCurrent += FXVolumeInAudioInterface;

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
