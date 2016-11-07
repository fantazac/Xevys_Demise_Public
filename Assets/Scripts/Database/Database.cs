using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class Database : MonoBehaviour
{
    private IDbCommand dbcmd;

    private void Start()
    {
        string conn = "URI=file:" + Application.dataPath + "/Database/Database.db"; //Path to database.
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        dbcmd = dbconn.CreateCommand();

        GetStaff();
        AddStaff();

        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }
    
    private void GetStaff()
    {
        string sqlQuery = "SELECT name, role, number " + "FROM Staff";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();

        while (reader.Read())
        {
            string name = reader.GetString(0);
            string role = reader.GetString(1);
            int number = reader.GetInt32(2);

            Debug.Log("name = " + name + "; role = " + role + "; number = " + number + ";");
        }
        reader.Close();
        reader = null;
    }

    private void AddStaff()
    {
        string sqlQuery = "INSERT INTO Staff (name, role, number)" + "VALUES (\"Vince\", \"character\", 21)";
        dbcmd.CommandText = sqlQuery;
        dbcmd.ExecuteNonQuery();
    }
}
