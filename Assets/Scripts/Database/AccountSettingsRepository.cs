using System;
using System.Data;

public class AccountSettingsRepository : DatabaseConnection
{
    public AccountSettingsEntity Get(int accountId)
    {
        AccountSettingsEntity entity = new AccountSettingsEntity();
        entity.AccountId = accountId;
        _dbconnection.Open();
        string sqlQuery = String.Format("SELECT MUSIC_PLAYING, KEYBOARD_CONTROL_SCHEME, GAMEPAD_CONTROL_SCHEME, MUSIC_VOLUME, SFX_VOLUME" +
            " FROM SETTINGS WHERE ACCOUNT_ID = {0}", accountId);
        _dbcommand.CommandText = sqlQuery;
        IDataReader reader = _dbcommand.ExecuteReader();
        while (reader.Read())
        {
            entity.IsMusicPlaying = Convert.ToBoolean(reader.GetInt32(0));
            entity.KeyboardControlSchemeId = reader.GetInt32(1);
            entity.GamepadControlSchemeId = reader.GetInt32(2);
            entity.MusicVolume = reader.GetFloat(3);
            entity.SoundEffectsVolume = reader.GetFloat(4);
        }
        reader.Close();
        _dbconnection.Close();
        return entity;
    }

    public void Add(AccountSettingsEntity entity)
    {
        _dbconnection.Open();
        string sqlQuery = String.Format("INSERT INTO SETTINGS (MUSIC_PLAYING, MUSIC_VOLUME, SFX_VOLUME, KEYBOARD_CONTROL_SCHEME, GAMEPAD_CONTROL_SCHEME, ACCOUNT_ID)" +
            " VALUES ({0}, {1}, {2}, {3}, {4}, {5})", Convert.ToInt32(entity.IsMusicPlaying), entity.MusicVolume, entity.SoundEffectsVolume, entity.KeyboardControlSchemeId, entity.GamepadControlSchemeId, entity.AccountId);
        _dbcommand.CommandText = sqlQuery;
        _dbcommand.ExecuteNonQuery();
        _dbconnection.Close();
    }

    public void UpdateEntity(AccountSettingsEntity entity)
    {
        _dbconnection.Open();
        string sqlQuery = String.Format("UPDATE SETTINGS SET MUSIC_PLAYING = {0}, KEYBOARD_CONTROL_SCHEME = {1}, GAMEPAD_CONTROL_SCHEME = {2}, MUSIC_VOLUME = {3}, SFX_VOLUME = {4}" +
            " WHERE ACCOUNT_ID = {5}", Convert.ToInt32(entity.IsMusicPlaying), entity.KeyboardControlSchemeId, entity.GamepadControlSchemeId, entity.MusicVolume, entity.SoundEffectsVolume, entity.AccountId);
        _dbcommand.CommandText = sqlQuery;
        _dbcommand.ExecuteNonQuery();
        _dbconnection.Close();
    }
}
