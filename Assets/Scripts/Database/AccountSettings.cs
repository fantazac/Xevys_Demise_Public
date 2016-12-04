using System;
using System.Data;
using UnityEngine;

public class AccountSettings : DatabaseConnection
{
    public delegate void OnMusicVolumeReloadedHandler(float volume);
    public event OnMusicVolumeReloadedHandler OnMusicVolumeReloaded;
    public delegate void OnSfxVolumeReloadedHandler(float volume);
    public event OnSfxVolumeReloadedHandler OnSfxVolumeReloaded;
    public delegate void OnMusicStateReloadedHandler(bool state);
    public event OnMusicStateReloadedHandler OnMusicStateReloaded;
    public delegate void OnKeyboardControlSchemeReloadedHandler(bool scheme);
    public event OnKeyboardControlSchemeReloadedHandler OnKeyboardControlSchemeReloaded;
    public delegate void OnGamepadControlSchemeReloadedHandler(bool scheme);
    public event OnGamepadControlSchemeReloadedHandler OnGamepadControlSchemeReloaded;

    private DatabaseController _controller;
    private PauseMenuAudioSettingsManager _audioSettingsManager;
    
    private int _keyboardControlScheme = 1;
    private int _gamepadControlScheme = 1;
    private float _musicVolume = 1;
    private float _sfxVolume = 1;
    private int _musicState = 1;

    protected void OnStart()
    {
        //_controller = GetComponent<DatabaseController>();
        //ControlSchemesManager controlSchemesManager = GameObject.Find(StaticObjects.GetFindTags().PauseMenuControlsOptionsButtons).GetComponent<ControlSchemesManager>();
        PauseMenuAudioSettingsManager.OnVolumeChanged += ChangeVolume;
        _audioSettingsManager = GameObject.Find("PauseMenuAudioOptionsButtons").GetComponent<PauseMenuAudioSettingsManager>();
        //controlSchemesManager.OnKeyboardControlChanged += ChangeKeyboardControl;
        //controlSchemesManager.OnGamepadControlChanged += ChangeGamepadControl;
        PauseMenuAudioSettingsManager.OnVolumeChanged += ChangeVolume;
        PauseMenuAudioSettingsManager.OnMusicStateChanged += ChangeMusicState;
    }

    public void CreateSettings()
    {
        _dbconnection.Open();
        string sqlQuery = String.Format("INSERT INTO SETTINGS (MUSIC_PLAYING, MUSIC_VOLUME, SFX_VOLUME, KEYBOARD_CONTROL_SCHEME, GAMEPAD_CONTROL_SCHEME, ACCOUNT_ID)" +
            " VALUES (1, 1, 1, 1, 1, {0})", _controller.AccountID);
        _dbcommand.CommandText = sqlQuery;
        _dbcommand.ExecuteNonQuery();
        _dbconnection.Close();
    }

    public void SaveSettings()
    {
        _sfxVolume = _audioSettingsManager._sfxVolumeBeforeDesactivate;
        _dbconnection.Open();
        string sqlQuery = String.Format("UPDATE SETTINGS SET MUSIC_PLAYING = {0}, KEYBOARD_CONTROL_SCHEME = {1}, GAMEPAD_CONTROL_SCHEME = {2}, MUSIC_VOLUME = {3}, SFX_VOLUME = {4}" +
            " WHERE ACCOUNT_ID = {5}", _musicState, _keyboardControlScheme, _gamepadControlScheme, _musicVolume, _sfxVolume, _controller.AccountID);
        _dbcommand.CommandText = sqlQuery;
        _dbcommand.ExecuteNonQuery();
        _dbconnection.Close();
    }

    public void LoadSettings()
    {
        _dbconnection.Open();
        string sqlQuery = String.Format("SELECT MUSIC_PLAYING, KEYBOARD_CONTROL_SCHEME, GAMEPAD_CONTROL_SCHEME, MUSIC_VOLUME, SFX_VOLUME" +
            " FROM SETTINGS WHERE ACCOUNT_ID = {0}", _controller.AccountID);
        _dbcommand.CommandText = sqlQuery;
        IDataReader reader = _dbcommand.ExecuteReader();
        while (reader.Read())
        {
            _musicState = reader.GetInt32(0);
            _keyboardControlScheme = reader.GetInt32(1);
            _gamepadControlScheme = reader.GetInt32(2);
            _musicVolume = reader.GetFloat(3);
            _sfxVolume = reader.GetFloat(4);
        }
        reader.Close();
        _dbconnection.Close();

        OnKeyboardControlSchemeReloaded(Convert.ToBoolean(_keyboardControlScheme));
        OnGamepadControlSchemeReloaded(Convert.ToBoolean(_gamepadControlScheme));
        OnMusicVolumeReloaded(_musicVolume);
        OnSfxVolumeReloaded(_sfxVolume);
        OnMusicStateReloaded(Convert.ToBoolean(_musicState));
    }

    private void ChangeKeyboardControl(bool scheme)
    {
        _keyboardControlScheme = Convert.ToInt32(scheme);
    }

    private void ChangeGamepadControl(bool scheme)
    {
        _gamepadControlScheme = Convert.ToInt32(scheme);
    }

    private void ChangeVolume(bool isMusic, float volume)
    {
        if (isMusic)
        {
            _musicVolume = volume;
        }
        else
        {
            _sfxVolume = volume;
        }
    }

    private void ChangeMusicState(bool state)
    {
        _musicState = Convert.ToInt32(state);
    }
}
