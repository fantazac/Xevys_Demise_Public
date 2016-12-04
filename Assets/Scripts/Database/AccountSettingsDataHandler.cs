using UnityEngine;

public class AccountSettingsDataHandler : MonoBehaviour
{
    public delegate void OnMusicVolumeReloadedHandler(float volume);
    public event OnMusicVolumeReloadedHandler OnMusicVolumeReloaded;
    public delegate void OnSfxVolumeReloadedHandler(float volume);
    public event OnSfxVolumeReloadedHandler OnSfxVolumeReloaded;
    public delegate void OnMusicStateReloadedHandler(bool state);
    public event OnMusicStateReloadedHandler OnMusicStateReloaded;
    public delegate void OnKeyboardControlSchemeReloadedHandler(int scheme);
    public event OnKeyboardControlSchemeReloadedHandler OnKeyboardControlSchemeReloaded;
    public delegate void OnGamepadControlSchemeReloadedHandler(int scheme);
    public event OnGamepadControlSchemeReloadedHandler OnGamepadControlSchemeReloaded;

    private AccountSettingsEntity _entity;
    private AccountSettingsRepository _repository;
    private void Start()
    {
        PauseMenuAudioSettingsManager.OnVolumeChanged += SetVolume;
        PauseMenuAudioSettingsManager.OnMusicStateChanged += SetMusicState;
        _repository = new AccountSettingsRepository();
        _entity = _repository.Get(StaticObjects.AccountId);
    }

    public void UpdateRepository()
    {
        _repository.UpdateEntity(_entity);
    }

    private void SetVolume(bool isMusic, float volume)
    {
        if (isMusic)
        {
            _entity.MusicVolume = volume;
        }
        else
        {
            _entity.SoundEffectsVolume = volume;
        }
    }

    private void SetMusicState(bool activate)
    {
        _entity.IsMusicPlaying = activate;
    }

    private void SetKeyboardControl(int scheme)
    {
        _entity.KeyboardControlSchemeId = scheme;
    }

    private void SetGamepadControl(int scheme)
    {
        _entity.GamepadControlSchemeId = scheme;
    }
}
