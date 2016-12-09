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
        MainMenuAudioSettingsManager.OnVolumeChanged += SetVolume;
        MainMenuAudioSettingsManager.OnMusicStateChanged += SetMusicState;
        ControlSchemesManager.OnKeyboardControlChanged += SetKeyboardControl;
        ControlSchemesManager.OnGamepadControlChanged += SetGamepadControl;
        _repository = new AccountSettingsRepository();

        MainMenuInputs.OnMainMenuLoaded += ConnectToOptionsInterfaceQuitEvent;
    }

    private void ConnectToOptionsInterfaceQuitEvent()
    {
        GameObject.Find(MainMenuStaticObjects.GetFindTags().MainMenuButtons).GetComponent<MainMenuCurrentInterfaceAnimator>().OnOptionsInterfaceQuit += UpdateRepository;
    }

    public void CreateNewEntry(int accountId)
    {
        AccountSettingsEntity entity = new AccountSettingsEntity();
        entity.AccountId = accountId;
        entity.IsMusicPlaying = true;
        entity.MusicVolume = 1;
        entity.SoundEffectsVolume = 1;
        entity.KeyboardControlSchemeId = 1;
        entity.GamepadControlSchemeId = 1;
        _repository.Add(entity);
    }

    public void UpdateRepository()
    {
        _repository.UpdateEntity(_entity);
    }

    public void ChangeEntity(int accountId)
    {
        _entity = _repository.Get(accountId);
        ReloadSettings();
    }

    private void ReloadSettings()
    {
        OnMusicVolumeReloaded(_entity.MusicVolume);
        OnSfxVolumeReloaded(_entity.SoundEffectsVolume);
        OnMusicStateReloaded(_entity.IsMusicPlaying);
        OnKeyboardControlSchemeReloaded(_entity.KeyboardControlSchemeId);
        OnGamepadControlSchemeReloaded(_entity.GamepadControlSchemeId);
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
