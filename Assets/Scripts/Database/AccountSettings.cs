using System;
using System.Data;
using UnityEngine;

public class AccountSettings : DatabaseConnection
{
    public delegate void OnMusicVolumeReloadedHandler(float volume);
    public event OnMusicVolumeReloadedHandler OnMusicVolumeReloaded;
    public delegate void OnSfxVolumeReloadedHandler(float volume);
    public event OnSfxVolumeReloadedHandler OnSfxVolumeReloaded;
    public delegate void OnKeyboardControlSchemeReloadedHandler(bool scheme);
    public event OnKeyboardControlSchemeReloadedHandler OnKeyboardControlSchemeReloaded;
    public delegate void OnGamepadControlSchemeReloadedHandler(bool scheme);
    public event OnGamepadControlSchemeReloadedHandler OnGamepadControlSchemeReloaded;

    private DatabaseController _controller;
    private PauseMenuAudioSettingsController _audioSettingsController;
    
    private int _keyboardControlScheme = 1;
    private int _gamepadControlScheme = 1;
    private float _musicVolume = 1;
    private float _sfxVolume = 1;

    protected override void Start()
    {
        base.Start();
        _controller = GetComponent<DatabaseController>();
        ControlsSchemeSettings controlsSchemeSettings = GameObject.Find(StaticObjects.GetFindTags().PauseMenuControlsOptionsButtons).GetComponent<ControlsSchemeSettings>();
        PauseMenuAudioSettingsController.OnVolumeChanged += ChangeVolume;
        _audioSettingsController = GameObject.Find("PauseMenuAudioOptionsButtons").GetComponent<PauseMenuAudioSettingsController>();
        controlsSchemeSettings.OnKeyboardControlChanged += ChangeKeyboardControl;
        controlsSchemeSettings.OnGamepadControlChanged += ChangeGamepadControl;
    }

    public void CreateSettings()
    {
        _dbconnection.Open();
        string sqlQuery = String.Format("INSERT INTO SETTINGS (MUSIC_PLAYING, MUSIC_VOLUME, SFX_VOLUME, KEYBOARD_CONTROL_SCHEME, GAMEPAD_CONTROL_SCHEME, ACCOUNT_ID)" +
            " VALUES (0, 1, 1, 1, 1, {0})", _controller.AccountID);
        _dbcommand.CommandText = sqlQuery;
        _dbcommand.ExecuteNonQuery();
        _dbconnection.Close();
    }

    public void SaveSettings()
    {
        _sfxVolume = _audioSettingsController._sfxVolumeBeforeDesactivate;
        _dbconnection.Open();
        string sqlQuery = String.Format("UPDATE SETTINGS SET KEYBOARD_CONTROL_SCHEME = {0}, GAMEPAD_CONTROL_SCHEME = {1}, MUSIC_VOLUME = {2}, SFX_VOLUME = {3}" +
            " WHERE ACCOUNT_ID = {4}", _keyboardControlScheme, _gamepadControlScheme, _musicVolume, _sfxVolume, _controller.AccountID);
        _dbcommand.CommandText = sqlQuery;
        _dbcommand.ExecuteNonQuery();
        _dbconnection.Close();
    }

    public void LoadSettings()
    {
        _dbconnection.Open();
        string sqlQuery = String.Format("SELECT KEYBOARD_CONTROL_SCHEME, GAMEPAD_CONTROL_SCHEME, MUSIC_VOLUME, SFX_VOLUME" +
            " FROM SETTINGS WHERE ACCOUNT_ID = {0}", _controller.AccountID);
        _dbcommand.CommandText = sqlQuery;
        IDataReader reader = _dbcommand.ExecuteReader();
        while (reader.Read())
        {
            _keyboardControlScheme = reader.GetInt32(0);
            _gamepadControlScheme = reader.GetInt32(1);
            _musicVolume = reader.GetFloat(2);
            _sfxVolume = reader.GetFloat(3);
        }
        reader.Close();
        _dbconnection.Close();

        OnKeyboardControlSchemeReloaded(Convert.ToBoolean(_keyboardControlScheme));
        OnGamepadControlSchemeReloaded(Convert.ToBoolean(_gamepadControlScheme));
        OnMusicVolumeReloaded(_musicVolume);
        OnSfxVolumeReloaded(_sfxVolume);
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
}
