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
            CreateAllAchievements();
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

    private void CreateAllAchievements()
    {
        _dbconnection.Open();
        CreateAchievement("Eye of the Behemoth", "Kill Behemoth in World 1");
        CreateAchievement("The Phoenix", "Kill Phoenix in World 2");
        CreateAchievement("Smoke on the water", "Kill Neptune in World 3");
        CreateAchievement("Highway to Hell", "Kill Vulcan in World 4");
        CreateAchievement("Bimon and the Beast", "Finally kill Xevy");
        CreateAchievement("Skeltals in the closet", "Kill 15 skeltals");
        CreateAchievement("Kill 'Em All", "Kill 15 scarabs");
        CreateAchievement("Bat country", "Kill 30 bats");
        CreateAchievement("In too deep", "Get the boots");
        CreateAchievement("I believe I can fly", "Get the feather");
        CreateAchievement("Bubble Pop!", "Get the bubble");
        CreateAchievement("Through the fire and flames", "Get the fire armor");
        _dbconnection.Close();
    }

    private void CreateAchievement(string name, string description)
    {
        string sqlQuery = String.Format("INSERT INTO ACHIEVEMENT (\"NAME\", \"DESCRIPTION\")" +
            "VALUES ({0}, {1}", name, description);
        _dbcommand.CommandText = sqlQuery;
        _dbcommand.ExecuteNonQuery();
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
