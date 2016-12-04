using UnityEngine;
using System.Data;
using System.IO;
using System;

public class DatabaseController : DatabaseConnection
{
    [SerializeField]
    private bool _loadDB;
    public int AccountID { get; private set; }

    protected override void Start()
    {
        base.Start();
        if(!_loadDB || !File.Exists(Path.Combine(Application.persistentDataPath, "Database.db")))
        {
            File.Copy(Path.Combine(Application.streamingAssetsPath, "Database.db"), Path.Combine(Application.persistentDataPath, "Database.db"), true);
        }
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
        //Dummie fun facts.
        CreateFunFact("All achievement names are actually parodies of song titles.");
        CreateFunFact("Xevy is actually a very sensitive guy.");
        CreateFunFact("The Game. You lost it.");
        CreateFunFact("No skeltals were hurt in the making of this game.");
        CreateFunFact("Spam Games' original name was Pawn V.");
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
