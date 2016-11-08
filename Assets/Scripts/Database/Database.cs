using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class Database : MonoBehaviour
{
    private IDbConnection _dbconn;
    private IDbCommand _dbcmd;

    private InventoryManager _inventoryManager;

    private int _accountID = 0;

    private void Start()
    {
        string conn = "URI=file:" + Application.dataPath + "/Database/Database.db";
        _dbconn = (IDbConnection)new SqliteConnection(conn);
        _dbcmd = _dbconn.CreateCommand();

        _inventoryManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManager>();
        _inventoryManager.OnEnableKnife += EnableKnife;
        _inventoryManager.OnEnableAxe += EnableAxe;
        _inventoryManager.OnEnableFeather += EnableFeather;
        _inventoryManager.OnEnableIronBoots += EnableBoots;
        _inventoryManager.OnEnableBubble += EnableBubble;
        _inventoryManager.OnEnableFireProofArmor += EnableArmor;
        _inventoryManager.OnEnableEarthArtefact += EnableEarthArtefact;
        _inventoryManager.OnEnableAirArtefact += EnableAirArtefact;
        _inventoryManager.OnEnableWaterArtefact += EnableWaterArtefact;
        _inventoryManager.OnEnableFireArtefact += EnableFireArtefact;

        CreateAccount("Test");
        CreateStatsRecord();
    }

    private void OnApplicationQuit()
    {
        //TEMP
        _dbconn.Open();
        string sqlQuery = "DELETE FROM STATS";
        _dbcmd.CommandText = sqlQuery;
        _dbcmd.ExecuteNonQuery();
        sqlQuery = "DELETE FROM ACCOUNT";
        _dbcmd.CommandText = sqlQuery;
        _dbcmd.ExecuteNonQuery();
        _dbconn.Close();

        _dbcmd.Dispose();
        _dbcmd = null;
        _dbconn = null;
    }

    private void CreateAccount(string username)
    {
        _dbconn.Open();
        string sqlQuery = "INSERT INTO ACCOUNT (ACCOUNT_ID, USERNAME)" +
            "VALUES (0, \"" + username + "\")";
        _dbcmd.CommandText = sqlQuery;
        _dbcmd.ExecuteNonQuery();

        _dbconn.Close();
    }

    private void CreateStatsRecord()
    {
        _dbconn.Open();
        string sqlQuery = "INSERT INTO STATS (STATS_ID, SECONDS_PLAYED, NB_KILLED_SCARABS, NB_KILLED_BATS, NB_KILLED_SKELTALS, NB_DEATHS, KNIFE_PICKED, AXE_PICKED, FEATHER_PICKED, BOOTS_PICKED, BUBBLE_PICKED, ARMOR_PICKED, ARTEFACT1_PICKED, ARTEFACT2_PICKED, ARTEFACT3_PICKED, ARTEFACT4_PICKED, GAME_COMPLETED, ACCOUNT_ID)" + 
            "VALUES (0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, " + _accountID + ")";
        _dbcmd.CommandText = sqlQuery;
        _dbcmd.ExecuteNonQuery();
        _dbconn.Close();
    }

    private void EnableKnife()
    {
        _dbconn.Open();
        string sqlQuery = "UPDATE STATS SET KNIFE_PICKED = 1 WHERE ACCOUNT_ID = " + _accountID;
        _dbcmd.CommandText = sqlQuery;
        _dbcmd.ExecuteNonQuery();
        _dbconn.Close();
    }

    private void EnableAxe()
    {
        _dbconn.Open();
        string sqlQuery = "UPDATE STATS SET AXE_PICKED = 1 WHERE ACCOUNT_ID = " + _accountID;
        _dbcmd.CommandText = sqlQuery;
        _dbcmd.ExecuteNonQuery();
        _dbconn.Close();
    }

    private void EnableFeather()
    {
        _dbconn.Open();
        string sqlQuery = "UPDATE STATS SET FEATHER_PICKED = 1 WHERE ACCOUNT_ID = " + _accountID;
        _dbcmd.CommandText = sqlQuery;
        _dbcmd.ExecuteNonQuery();
        _dbconn.Close();
    }

    private void EnableBoots()
    {
        _dbconn.Open();
        string sqlQuery = "UPDATE STATS SET BOOTS_PICKED = 1 WHERE ACCOUNT_ID = " + _accountID;
        _dbcmd.CommandText = sqlQuery;
        _dbcmd.ExecuteNonQuery();
        _dbconn.Close();
    }

    private void EnableBubble()
    {
        _dbconn.Open();
        string sqlQuery = "UPDATE STATS SET BUBBLE_PICKED = 1 WHERE ACCOUNT_ID = " + _accountID;
        _dbcmd.CommandText = sqlQuery;
        _dbcmd.ExecuteNonQuery();
        _dbconn.Close();
    }

    private void EnableArmor()
    {
        _dbconn.Open();
        string sqlQuery = "UPDATE STATS SET ARMOR_PICKED = 1 WHERE ACCOUNT_ID = " + _accountID;
        _dbcmd.CommandText = sqlQuery;
        _dbcmd.ExecuteNonQuery();
        _dbconn.Close();
    }

    private void EnableEarthArtefact()
    {
        _dbconn.Open();
        string sqlQuery = "UPDATE STATS SET ARTEFACT1_PICKED = 1 WHERE ACCOUNT_ID = " + _accountID;
        _dbcmd.CommandText = sqlQuery;
        _dbcmd.ExecuteNonQuery();
        _dbconn.Close();
    }

    private void EnableAirArtefact()
    {
        _dbconn.Open();
        string sqlQuery = "UPDATE STATS SET ARTEFACT2_PICKED = 1 WHERE ACCOUNT_ID = " + _accountID;
        _dbcmd.CommandText = sqlQuery;
        _dbcmd.ExecuteNonQuery();
        _dbconn.Close();
    }

    private void EnableWaterArtefact()
    {
        _dbconn.Open();
        string sqlQuery = "UPDATE STATS SET ARTEFACT3_PICKED = 1 WHERE ACCOUNT_ID = " + _accountID;
        _dbcmd.CommandText = sqlQuery;
        _dbcmd.ExecuteNonQuery();
        _dbconn.Close();
    }

    private void EnableFireArtefact()
    {
        _dbconn.Open();
        string sqlQuery = "UPDATE STATS SET ARTEFACT4_PICKED = 1 WHERE ACCOUNT_ID = " + _accountID;
        _dbcmd.CommandText = sqlQuery;
        _dbcmd.ExecuteNonQuery();
        _dbconn.Close();
    }


    //SELECT example
    private void GetStaff()
    {
        string sqlQuery = "SELECT name, role, number " + "FROM Staff";
        _dbcmd.CommandText = sqlQuery;
        IDataReader reader = _dbcmd.ExecuteReader();

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

    //INSERT INTO example
    private void AddStaff()
    {
        string sqlQuery = "INSERT INTO Staff (name, role, number)" + "VALUES (\"Vince\", \"character\", 21)";
        _dbcmd.CommandText = sqlQuery;
        _dbcmd.ExecuteNonQuery();
    }
}
