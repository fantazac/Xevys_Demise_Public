using System;
using System.Data;

public class AccountSettings : DatabaseConnection
{
    private DatabaseController _controller;
    
    private int _keyboardControlScheme = 1;
    private int _gamepadControlScheme = 1;
    private void Start()
    {
        base.Start();
        _controller = GetComponent<DatabaseController>();
        GetComponent<ControlsSchemeSettings>().OnKeyboardControlChanged += ChangeKeyboardControl;
        GetComponent<ControlsSchemeSettings>().OnGamepadControlChanged += ChangeGamepadControl;
    }

    public void CreateSettings()
    {
        _dbconnection.Open();
        string sqlQuery = String.Format("INSERT INTO SETTINGS (MUSIC_PLAYING, MUSIC_VOLUME, SFX_VOLUME, KEYBOARD_CONTROL_SCHEME, GAMEPAD_CONTROL_SCHEME, ACCOUNT_ID)" +
            " VALUES (0, 0, 0, 1, 1, {0})", _controller.AccountID);
        _dbcommand.CommandText = sqlQuery;
        _dbcommand.ExecuteNonQuery();
        _dbconnection.Close();
    }

    public void SaveSettings()
    {
        _dbconnection.Open();
        string sqlQuery = String.Format("UPDATE SETTINGS SET KEYBOARD_CONTROL_SCHEME = {0}, GAMEPAD_CONTROL_SCHEME = {1} " +
            "WHERE ACCOUNT_ID = {2}", _keyboardControlScheme, _gamepadControlScheme, _controller.AccountID);
        _dbcommand.CommandText = sqlQuery;
        _dbcommand.ExecuteNonQuery();
        _dbconnection.Close();
    }

    public void LoadSettings()
    {
        _dbconnection.Open();
        string sqlQuery = String.Format("SELECT KEYBOARD_CONTROL_SCHEME, GAMEPAD_CONTROL_SCHEME" +
            " FROM SETTINGS WHERE ACCOUNT_ID = {0}", _controller.AccountID);
        _dbcommand.CommandText = sqlQuery;
        IDataReader reader = _dbcommand.ExecuteReader();
        while (reader.Read())
        {
            _keyboardControlScheme = reader.GetInt32(0);
            _gamepadControlScheme = reader.GetInt32(1);
        }
        reader.Close();
        _dbconnection.Close();
    }

    private void ChangeKeyboardControl(bool control)
    {
        _keyboardControlScheme = Convert.ToInt32(control);
    }

    private void ChangeGamepadControl(bool control)
    {
        _gamepadControlScheme = Convert.ToInt32(control);
    }
}
