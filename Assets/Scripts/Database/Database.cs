using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;
using System;

public class Database : DatabaseConnection
{
    private AccountStats _accountStats;
    public int AccountID { get; private set; }

    private void Start()
    {
        base.Start();
        _accountStats = GetComponent<AccountStats>();
        if(!_accountStats._loadStats)
        {
            File.Copy(Path.Combine(Application.streamingAssetsPath, "Database.db"), Path.Combine(Application.persistentDataPath, "Database.db"), true);
            CreateAccount("Test");
            _accountStats.CreateStatsRecord();
            CreateSettings();
        }
        else
        {
            _accountStats.LoadStats();
        }
    }

    private void CreateAccount(string username)
    {
        _dbconnection.Open();
        string sqlQuery = String.Format("INSERT INTO ACCOUNT (USERNAME)" +
            "VALUES (\"{0}\")", username);
        _dbcommand.CommandText = sqlQuery;
        _dbcommand.ExecuteNonQuery();
        _dbconnection.Close();
        ChangeAccount(username);
    }

    private void ChangeAccount(string username)
    {
        _dbconnection.Open();
        string sqlQuery = String.Format("SELECT ACCOUNT_ID" +
             " FROM ACCOUNT WHERE USERNAME = \"{0}\"", username);
        _dbcommand.CommandText = sqlQuery;
        IDataReader reader = _dbcommand.ExecuteReader();
        while (reader.Read())
        {
            AccountID = reader.GetInt32(0);
        }
        reader.Close();
        _dbconnection.Close();
    }

    private void CreateSettings()
    {
        _dbconnection.Open();
        string sqlQuery = String.Format("INSERT INTO SETTINGS (MUSIC_PLAYING, MUSIC_VOLUME, SFX_VOLUME, CONTROL_SCHEME, ACCOUNT_ID)" +
            "VALUES (0, 0, 0, 0, {0})", AccountID);
        _dbcommand.CommandText = sqlQuery;
        _dbcommand.ExecuteNonQuery();
        _dbconnection.Close();
    }

    private void CreateAchievement(string name, string description)
    {
        _dbconnection.Open();
        string sqlQuery = String.Format("INSERT INTO ACHIEVEMENT (NAME, DESCRIPTION)" +
            "VALUES ({0}, {1}", name, description);
        _dbcommand.CommandText = sqlQuery;
        _dbcommand.ExecuteNonQuery();
        _dbconnection.Close();
    }

    private void CreateAccountAchievement(int accountID, int achievementID)
    {
        _dbconnection.Open();
        string sqlQuery = String.Format("INSERT INTO ACCOUNT_ACHIEVEMENT (ACCOUNT_ID, ACHIEVEMENT_ID)" +
            "VALUES ({0}, {1}", accountID, achievementID);
        _dbcommand.CommandText = sqlQuery;
        _dbcommand.ExecuteNonQuery();
        _dbconnection.Close();
    }

    private void CreateFunFact(string description)
    {
        _dbconnection.Open();
        string sqlQuery = String.Format("INSERT INTO FUN_FACT (DESCRIPTION)" +
            "VALUES ({0})", description);
        _dbcommand.CommandText = sqlQuery;
        _dbcommand.ExecuteNonQuery();
        _dbconnection.Close();
    }
}
