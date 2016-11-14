﻿using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;
using System;

public class Database : MonoBehaviour
{
    [SerializeField]
    private bool _loadStats = false;
    public delegate void OnInventoryReloadedHandler(bool knifeEnabled, bool axeEnabled, bool featherEnabled, bool bootsEnabled, bool bubbleEnabled, bool armorEnabled, bool earthArtefactEnabled, bool airArtefactEnabled, bool waterArtefactEnabled, bool fireArtefactEnabled);
    public static event OnInventoryReloadedHandler OnInventoryReloaded;
    public delegate void OnAmmoReloadedHandler(int knifeAmmo, int axeAmmo);
    public static event OnAmmoReloadedHandler OnAmmoReloaded;

    private IDbConnection _dbconn;
    private IDbCommand _dbcmd;
    InventoryManager _inventoryManager;
    PlayerThrowingWeaponsMunitions _munitions;

    //TEMP
    private int _accountID = 0;

    private int _tempNbScarabsKilled;
    private int _tempNbBatsKilled;
    private int _tempNbSkeltalsKilled;
    private int _nbScarabsKilled;
    private int _nbBatsKilled;
    private int _nbSkeltalsKilled;

    private int _knifeEnabled;
    private int _knifeAmmo;
    private int _axeEnabled;
    private int _axeAmmo;
    private int _featherEnabled;
    private int _bootsEnabled;
    private int _bubbleEnabled;
    private int _armorEnabled;
    private int _earthArtefactEnabled;
    private int _airArtefactEnabled;
    private int _waterArtefactEnabled;
    private int _fireArtefactEnabled;

    private void Start()
    {
        if(!_loadStats)
        {
            File.Copy(Path.Combine(Application.streamingAssetsPath, "Database.db"), Path.Combine(Application.persistentDataPath, "Database.db"), true);
        }

        string conn = "URI=file:" + Path.Combine(Application.persistentDataPath, "Database.db");
        _dbconn = (IDbConnection)new SqliteConnection(conn);
        _dbcmd = _dbconn.CreateCommand();

        _inventoryManager = StaticObjects.GetPlayer().GetComponent<InventoryManager>();
        _munitions = StaticObjects.GetPlayer().GetComponent<PlayerThrowingWeaponsMunitions>();
        DestroyEnemyOnDeath.OnEnemyDeath += EnemyKilled;

        if (_loadStats)
        {
            LoadStats();
        }
        else
        {
            CreateAccount("Test");
            CreateStatsRecord();
            CreateSettings();
        }
    }

    private void OnApplicationQuit()
    {
        SaveStats();

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

    private void CreateSettings()
    {
        _dbconn.Open();
        string sqlQuery = String.Format("INSERT INTO SETTINGS (SETTINGS_ID, MUSIC_PLAYING, MUSIC_VOLUME, SFX_VOLUME, CONTROL_SCHEME, ACCOUNT_ID)" +
            "VALUES (0, 0, 0, 0, 0, {0})", _accountID);
        _dbcmd.CommandText = sqlQuery;
        _dbcmd.ExecuteNonQuery();
        _dbconn.Close();
    }

    private void CreateAchievement(string name, string description)
    {
        _dbconn.Open();
        string sqlQuery = String.Format("INSERT INTO ACHIEVEMENT (ACHIEVEMENT_ID, NAME, DESCRIPTION)" +
            "VALUES (0, {0}, {1}", name, description);
        _dbcmd.CommandText = sqlQuery;
        _dbcmd.ExecuteNonQuery();
        _dbconn.Close();
    }

    private void CreateAccountAchievement(int accountID, int achievementID)
    {
        _dbconn.Open();
        string sqlQuery = String.Format("INSERT INTO ACCOUNT_ACHIEVEMENT (ACCOUNT_ID, ACHIEVEMENT_ID)" +
            "VALUES ({0}, {1}", accountID, achievementID);
        _dbcmd.CommandText = sqlQuery;
        _dbcmd.ExecuteNonQuery();
        _dbconn.Close();
    }

    private void CreateFunFact(string description)
    {
        _dbconn.Open();
        string sqlQuery = String.Format("INSERT INTO FUN_FACT (FUN_FACT_ID, DESCRIPTION)" +
            "VALUES (0, {0})", description);
        _dbcmd.CommandText = sqlQuery;
        _dbcmd.ExecuteNonQuery();
        _dbconn.Close();
    }

    private void CreateStatsRecord()
    {
        _dbconn.Open();
        string sqlQuery = String.Format("INSERT INTO STATS (STATS_ID, SECONDS_PLAYED, NB_KILLED_SCARABS, NB_KILLED_BATS, NB_KILLED_SKELTALS, NB_DEATHS, KNIFE_PICKED, KNIFE_AMMO, AXE_PICKED, AXE_AMMO, FEATHER_PICKED, BOOTS_PICKED, BUBBLE_PICKED, ARMOR_PICKED, ARTEFACT1_PICKED, ARTEFACT2_PICKED, ARTEFACT3_PICKED, ARTEFACT4_PICKED, GAME_COMPLETED, ACCOUNT_ID)" + 
            "VALUES (0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, {0})", _accountID);
        _dbcmd.CommandText = sqlQuery;
        _dbcmd.ExecuteNonQuery();
        _dbconn.Close();
    }

    public void SaveTemporaryStats()
    {
        _nbScarabsKilled = _tempNbScarabsKilled;
        _nbBatsKilled = _tempNbBatsKilled;
        _nbSkeltalsKilled = _tempNbSkeltalsKilled;
        _knifeEnabled = Convert.ToInt32(_inventoryManager.KnifeEnabled);
        _knifeAmmo = _munitions.KnifeAmmo;
        _axeAmmo = _munitions.AxeAmmo;
        _axeEnabled = Convert.ToInt32(_inventoryManager.AxeEnabled);
        _featherEnabled = Convert.ToInt32(_inventoryManager.FeatherEnabled);
        _bootsEnabled = Convert.ToInt32(_inventoryManager.IronBootsEnabled);
        _bubbleEnabled = Convert.ToInt32(_inventoryManager.BubbleEnabled);
        _armorEnabled = Convert.ToInt32(_inventoryManager.FireProofArmorEnabled);
        _earthArtefactEnabled = Convert.ToInt32(_inventoryManager.EarthArtefactEnabled);
        _airArtefactEnabled = Convert.ToInt32(_inventoryManager.AirArtefactEnabled);
        _waterArtefactEnabled = Convert.ToInt32(_inventoryManager.WaterArtefactEnabled);
        _fireArtefactEnabled = Convert.ToInt32(_inventoryManager.FireArtefactEnabled);
    }

    private void SaveStats()
    {
        _dbconn.Open();
        string sqlQuery = String.Format("UPDATE STATS SET NB_KILLED_SCARABS = {0}, NB_KILLED_BATS = {1}, NB_KILLED_SKELTALS = {2}, KNIFE_PICKED = {3}, KNIFE_AMMO = {4}, AXE_PICKED = {5}, AXE_AMMO = {6}, FEATHER_PICKED = {7}, BOOTS_PICKED = {8}, BUBBLE_PICKED = {9}, ARMOR_PICKED = {10}, ARTEFACT1_PICKED = {11}, ARTEFACT2_PICKED = {12}, ARTEFACT3_PICKED = {13}, ARTEFACT4_PICKED = {14} " +
            "WHERE ACCOUNT_ID = {13}", _nbScarabsKilled, _nbBatsKilled, _nbSkeltalsKilled, _knifeEnabled, _knifeAmmo, _axeEnabled, _axeAmmo, _featherEnabled, _bootsEnabled, _bubbleEnabled, _armorEnabled, _earthArtefactEnabled, _airArtefactEnabled, _waterArtefactEnabled, _fireArtefactEnabled, _accountID);
        _dbcmd.CommandText = sqlQuery;
        _dbcmd.ExecuteNonQuery();
        _dbconn.Close();
    }

    private void LoadStats()
    {
        _dbconn.Open();
        string sqlQuery = String.Format("SELECT NB_KILLED_SCARABS, NB_KILLED_BATS, NB_KILLED_SKELTALS, KNIFE_PICKED, KNIFE_AMMO, AXE_PICKED, AXE_AMMO, FEATHER_PICKED, BOOTS_PICKED, BUBBLE_PICKED, ARMOR_PICKED, ARTEFACT1_PICKED, ARTEFACT2_PICKED, ARTEFACT3_PICKED, ARTEFACT4_PICKED"  +
            " FROM STATS WHERE ACCOUNT_ID = {0}", _accountID);
        _dbcmd.CommandText = sqlQuery;
        IDataReader reader = _dbcmd.ExecuteReader();
        while (reader.Read())
        {
            _tempNbScarabsKilled = reader.GetInt32(0);
            _tempNbBatsKilled = reader.GetInt32(1);
            _tempNbSkeltalsKilled = reader.GetInt32(2);
            _knifeEnabled = reader.GetInt32(3);
            _knifeAmmo = reader.GetInt32(4);
            _axeEnabled = reader.GetInt32(5);
            _axeAmmo = reader.GetInt32(6);
            _featherEnabled = reader.GetInt32(7);
            _bootsEnabled = reader.GetInt32(8);
            _bubbleEnabled = reader.GetInt32(9);
            _armorEnabled = reader.GetInt32(10);
            _earthArtefactEnabled = reader.GetInt32(11);
            _airArtefactEnabled = reader.GetInt32(12);
            _waterArtefactEnabled = reader.GetInt32(13);
            _fireArtefactEnabled = reader.GetInt32(14);
        }
        reader.Close();
        reader = null;
        _dbconn.Close();

        OnInventoryReloaded(Convert.ToBoolean(_knifeEnabled), Convert.ToBoolean(_axeEnabled), Convert.ToBoolean(_featherEnabled), Convert.ToBoolean(_bootsEnabled), Convert.ToBoolean(_bubbleEnabled), Convert.ToBoolean(_armorEnabled), Convert.ToBoolean(_earthArtefactEnabled), Convert.ToBoolean(_airArtefactEnabled), Convert.ToBoolean(_waterArtefactEnabled), Convert.ToBoolean(_fireArtefactEnabled));
        OnAmmoReloaded(_knifeAmmo, _axeAmmo);
    }

    private void EnemyKilled(string tag)
    {
        if(tag == "Scarab")
        {
            _tempNbScarabsKilled++;
        }
        else if(tag == "Bat")
        {
            _tempNbBatsKilled++;
        }
        else if(tag == "Skeltal")
        {
            _tempNbSkeltalsKilled++;
        }
    }
}
