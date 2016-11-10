﻿using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class Database : MonoBehaviour
{
    private IDbConnection _dbconn;
    private IDbCommand _dbcmd;

    //TEMP
    private int _accountID = 0;

    private int _nbScarabsKilled = 0;
    private int _nbBatsKilled = 0;
    private int _nbSkeltalsKilled = 0;

    private int _knifeEnabled = 0;
    private int _axeEnabled = 0;
    private int _featherEnabled = 0;
    private int _bootsEnabled = 0;
    private int _bubbleEnabled = 0;
    private int _armorEnabled = 0;
    private int _earthArtefactEnabled = 0;
    private int _airArtefactEnabled = 0;
    private int _waterArtefactEnabled = 0;
    private int _fireArtefactEnabled = 0;

    private void Start()
    {
        //UnityEngine.Object[] objects = Resources.LoadAll("");
        //File.WriteAllBytes(Application.persistentDataPath + "/Database.db", ObjectToByteArray(objects));
        File.Copy(Path.Combine(Application.streamingAssetsPath, "Database.db"), Path.Combine(Application.persistentDataPath, "Database.db"), true);
        string conn = "URI=file:" + Path.Combine(Application.persistentDataPath, "Database.db");
        _dbconn = (IDbConnection)new SqliteConnection(conn);
        _dbcmd = _dbconn.CreateCommand();

        InventoryManager inventoryManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManager>();
        inventoryManager.OnEnableKnife += EnableKnife;
        inventoryManager.OnEnableAxe += EnableAxe;
        inventoryManager.OnEnableFeather += EnableFeather;
        inventoryManager.OnEnableIronBoots += EnableBoots;
        inventoryManager.OnEnableBubble += EnableBubble;
        inventoryManager.OnEnableFireProofArmor += EnableArmor;
        inventoryManager.OnEnableEarthArtefact += EnableEarthArtefact;
        inventoryManager.OnEnableAirArtefact += EnableAirArtefact;
        inventoryManager.OnEnableWaterArtefact += EnableWaterArtefact;
        inventoryManager.OnEnableFireArtefact += EnableFireArtefact;

        DestroyEnemyOnDeath.OnEnemyDeath += EnemyKilled;

        CreateAccount("Test");
        CreateStatsRecord();
    }

    private void OnApplicationQuit()
    {
        _dbcmd.Dispose();
        _dbcmd = null;
        _dbconn = null;
    }

    private void CreateAccount(string username)
    {
        _dbconn.Open();
        string sqlQuery = String.Format("INSERT INTO ACCOUNT (ACCOUNT_ID, USERNAME)" +
            "VALUES (0, \"{0}\")", username);
        _dbcmd.CommandText = sqlQuery;
        _dbcmd.ExecuteNonQuery();

        _dbconn.Close();
    }

    private void CreateStatsRecord()
    {
        _dbconn.Open();
        string sqlQuery = String.Format("INSERT INTO STATS (STATS_ID, SECONDS_PLAYED, NB_KILLED_SCARABS, NB_KILLED_BATS, NB_KILLED_SKELTALS, NB_DEATHS, KNIFE_PICKED, AXE_PICKED, FEATHER_PICKED, BOOTS_PICKED, BUBBLE_PICKED, ARMOR_PICKED, ARTEFACT1_PICKED, ARTEFACT2_PICKED, ARTEFACT3_PICKED, ARTEFACT4_PICKED, GAME_COMPLETED, ACCOUNT_ID)" + 
            "VALUES (0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, {0})", _accountID);
        _dbcmd.CommandText = sqlQuery;
        _dbcmd.ExecuteNonQuery();
        _dbconn.Close();
    }

    public void SaveStats()
    {
        _dbconn.Open();
        string sqlQuery = String.Format("UPDATE STATS SET NB_KILLED_SCARABS = {0}, NB_KILLED_BATS = {1}, NB_KILLED_SKELTALS = {2}, KNIFE_PICKED = {3}, AXE_PICKED = {4}, FEATHER_PICKED = {5}, BOOTS_PICKED = {6}, BUBBLE_PICKED = {7}, ARMOR_PICKED = {8}, ARTEFACT1_PICKED = {9}, ARTEFACT2_PICKED = {10}, ARTEFACT3_PICKED = {11}, ARTEFACT4_PICKED = {12} WHERE ACCOUNT_ID = {13}",
            _nbScarabsKilled, _nbBatsKilled, _nbSkeltalsKilled, _knifeEnabled, _axeEnabled, _featherEnabled, _bootsEnabled, _bubbleEnabled, _armorEnabled, _earthArtefactEnabled, _airArtefactEnabled, _waterArtefactEnabled, _fireArtefactEnabled, _accountID);
        _dbcmd.CommandText = sqlQuery;
        _dbcmd.ExecuteNonQuery();
        _dbconn.Close();
    }

    private void EnemyKilled(string tag)
    {
        if(tag == "Scarab")
        {
            _nbScarabsKilled++;
        }
        else if(tag == "Bat")
        {
            _nbBatsKilled++;
        }
        else if(tag == "Skeltal")
        {
            _nbSkeltalsKilled++;
        }
    }

    private void EnableKnife()
    {
        _knifeEnabled = 1;
    }

    private void EnableAxe()
    {
        _knifeEnabled = 1;
    }

    private void EnableFeather()
    {
        _featherEnabled = 1;
    }

    private void EnableBoots()
    {
        _bootsEnabled = 1;
    }

    private void EnableBubble()
    {
        _bubbleEnabled = 1;
    }

    private void EnableArmor()
    {
        _armorEnabled = 1;
    }

    private void EnableEarthArtefact()
    {
        _earthArtefactEnabled = 1;
        SaveStats();
    }

    private void EnableAirArtefact()
    {
        _airArtefactEnabled = 1;
        SaveStats();
    }

    private void EnableWaterArtefact()
    {
        _waterArtefactEnabled = 1;
        SaveStats();
    }

    private void EnableFireArtefact()
    {
        _fireArtefactEnabled = 1;
        SaveStats();
    }

    private byte[] ObjectToByteArray(object obj)
    {
        if (obj == null)
            return null;
        BinaryFormatter bf = new BinaryFormatter();
        using (MemoryStream ms = new MemoryStream())
        {
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }
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