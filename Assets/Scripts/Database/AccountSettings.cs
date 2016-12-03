using System;
using System.Data;
using UnityEngine;

/* BEN_CORRECTION
 * 
 * OMG..........
 * 
 * 
 * Ok, par où commencer..... Je vais commecner par vous dire que vous êtes la seule équipe qui n'a pas effectuée de séparation entre les components et la base de données.
 * Je ne dit pas que vous aurez pas de component qui prend en charge la base de données, je dis seulement que vous avez tout mis dans les components Unity et que
 * cela vous donne les monstres que j'ai devant moi actuellement.
 * 
 * Voilà ce que je vous propose : utiliser le "Repository Patern". Pour chaque table, créez vous un classe permettant d'effectuer des actions de lecture et d'écriture
 * sur cette table dans la base de données. Par exemple, pour vos settings, vous auriez un "AccountSettingsRepository" ayant les méthodes suivantes :
 *
 *  - public AccountSettingsEntity Get(int accountId)
 *  - public void Add(AccountSettingsEntity entity)
 *  - public void Update(AccountSettingsEntity entity)
 *  
 *  Aussi, pour chaque table, cela va vous prendre aussi une classe représentant un enregistrement. Elle ne fait que servir de conteneur de données. Par exemple, vous auriez
 *  une classe "AccountSettingsEntity" ressemblant à cela :
 *  
 *  public class AccountSettingsEntity {
 *  
 *      public int AccountID { get; set; }
 *      public bool IsMusicPlaying { get; set; }
 *      public int MusicVolume { get; set; }
 *      public int SoundEffectsVolume { get; set; }
 *      public int KeyboardControlSchemeId { get; set; }
 *      public int GamepadControlSchemeId { get; set; }
 *  
 *  }
 *  
 *  Une fois que vous aurez tous vos repositories et leurs entités, vous pourrez vous créer des components qui les utilisent. Actuellement, vous avez un espèce
 *  de mélange entre components et évènements statiques qui fait vraiment pitié. Ce qu'il vous faut, ce sont des components accessibles à partir de "StaticObjects"
 *  et qui permetent d'effectuer divers manipulations sur les données. Par exemple, vous pourriez avoir un component nommé "SoundVolumeManager" (dans ce cas, le
 *  mot "Manager" ne me dérange pas du tout) sur lequel vous pouvez modifier le volume de la musique et le volume des SFX. Ce component s'occupe ensuite
 *  de modifier le volume comme il se doit. "SoundVolumeManager" expose aussi deux évènement : "OnMusicVolumeChanged" et "OnSFXVolumeChanged" (je vais y revenir).
 *  
 *  Enfin, la dernière chose à ajouter, ce sont des components qui déclanchent la sauvegarde et le chargement des données à partir des "repositories". Par exemple,
 *  vous auriez un component "SoundVolumeDataHandler" qui, au "OnStart", charge les données à partir de la base de données et les place dans "SoundVolumeManager".
 *  Il conserve l'entité en mémoire. Il s'enregistre aussi auprès des évènements "OnMusicVolumeChanged" et "OnSFXVolumeChanged" de "SoundVolumeManager" et modifie
 *  alors l'entité qu'il possède en mémoire pour réfléter les changements de l'utilisateur. 
 *  
 *  Il reste alors à effectuer la sauvegarde des données. Rendu là, c'est vous qui voyez. Cela peut se faire dès que les données changent, ou cela peut se faire
 *  uniquement lorsque le joueur atteint un checkpoint. Je vous laisse décider.
 *  
 *  Ouf....gros commentaire....je reprend ma correction.
 *  
 */
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
    private PauseMenuAudioSettingsController _audioSettingsController;
    
    private int _keyboardControlScheme = 1;
    private int _gamepadControlScheme = 1;
    private float _musicVolume = 1;
    private float _sfxVolume = 1;
    private int _musicState = 1;

    protected override void Start()
    {
        base.Start();
        _controller = GetComponent<DatabaseController>();
        ControlsSchemeSettings controlsSchemeSettings = GameObject.Find(StaticObjects.GetFindTags().PauseMenuControlsOptionsButtons).GetComponent<ControlsSchemeSettings>();
        PauseMenuAudioSettingsController.OnVolumeChanged += ChangeVolume;
        _audioSettingsController = GameObject.Find("PauseMenuAudioOptionsButtons").GetComponent<PauseMenuAudioSettingsController>();
        controlsSchemeSettings.OnKeyboardControlChanged += ChangeKeyboardControl;
        controlsSchemeSettings.OnGamepadControlChanged += ChangeGamepadControl;
        PauseMenuAudioSettingsController.OnVolumeChanged += ChangeVolume;
        PauseMenuAudioSettingsController.OnMusicStateChanged += ChangeMusicState;
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
        _sfxVolume = _audioSettingsController._sfxVolumeBeforeDesactivate;
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
