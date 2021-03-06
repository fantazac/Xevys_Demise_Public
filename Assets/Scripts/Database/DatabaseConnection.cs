﻿using UnityEngine;
using System.Data;
using System.IO;
using Mono.Data.Sqlite;

public class DatabaseConnection
{
    protected IDbConnection _dbconnection;
    protected IDbCommand _dbcommand;

    public DatabaseConnection()
    {
        string connection = "URI=file:" + Path.Combine(Application.persistentDataPath, "Database.db");
        _dbconnection = (IDbConnection)new SqliteConnection(connection);
        _dbcommand = _dbconnection.CreateCommand();
    }

    protected void OnApplicationQuit()
    {
        _dbcommand.Dispose();
        _dbcommand = null;
        _dbconnection = null;
    }
}
