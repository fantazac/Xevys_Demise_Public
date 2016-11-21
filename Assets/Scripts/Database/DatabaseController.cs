using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;
using System;

public class DatabaseController : DatabaseConnection
{
    private AccountStats _accountStats;
    private AccountSettings _accountSettings;
    public int AccountID { get; private set; }

    private void Start()
    {
        base.Start();
        _accountStats = GetComponent<AccountStats>();
        _accountSettings = GetComponent<AccountSettings>();
        if(!_accountStats._loadStats)
        {
            File.Copy(Path.Combine(Application.streamingAssetsPath, "Database.db"), Path.Combine(Application.persistentDataPath, "Database.db"), true);
            CreateAll();
        }
        else
        {
            LoadAll();
        }
    }

    private void CreateAll()
    {
        CreateAccount("Test");
        _accountStats.CreateStatsRecord();
        _accountSettings.CreateSettings();
        CreateAllAchievements();
        CreateAllFunFacts();
    }

    public void SaveAll()
    {
        _accountStats.SaveStats();
        _accountSettings.SaveSettings();
    }

    private void LoadAll()
    {
        ChangeAccount("Test");
        _accountStats.LoadStats();
        _accountSettings.LoadSettings();
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
        string sqlQuery = String.Format("INSERT INTO ACHIEVEMENT (NAME, DESCRIPTION)" +
            " VALUES (\"{0}\", \"{1}\")", name, description);
        _dbcommand.CommandText = sqlQuery;
        _dbcommand.ExecuteNonQuery();
    }

    private void CreateAllFunFacts()
    {
        _dbconnection.Open();
        CreateFunFact("All achievement names are actually parody of song titles.");
        CreateFunFact("Xevy is actually a very sensitive guy.");
        CreateFunFact("The Game. You lost it.");
        CreateFunFact("No skeltals were hurt in the making of this game.");
        CreateFunFact("Spam Games original name was Pawn V.");
        _dbconnection.Close();
    }

    private void CreateFunFact(string description)
    {
        string sqlQuery = String.Format("INSERT INTO FUN_FACT (DESCRIPTION)" +
            "VALUES (\"{0}\")", description);
        _dbcommand.CommandText = sqlQuery;
        _dbcommand.ExecuteNonQuery();
    }
}
