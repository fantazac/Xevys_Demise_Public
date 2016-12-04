using UnityEngine;
using System.Data;
using System;
using System.Collections;

public class AccountStats : DatabaseConnection
{
    public delegate void OnInventoryReloadedHandler(bool knifeEnabled, bool axeEnabled, bool featherEnabled, bool bootsEnabled, bool bubbleEnabled, bool armorEnabled, bool earthArtefactEnabled, bool airArtefactEnabled, bool waterArtefactEnabled, bool fireArtefactEnabled);
    public static event OnInventoryReloadedHandler OnInventoryReloaded;
    public delegate void OnAmmoReloadedHandler(int knifeAmmo, int axeAmmo);
    public static event OnAmmoReloadedHandler OnAmmoReloaded;
    public delegate void OnHealthReloadedHandler(int health);
    public static event OnHealthReloadedHandler OnHealthReloaded;

    private DatabaseController _controller;
    private InventoryManager _inventoryManager;
    private PlayerWeaponAmmo _munitions;
    
    private int _secondsPlayed = 0;
    private int _lifeRemaining = 1000;
    private int _tempNbScarabsKilled = 0;
    private int _tempNbBatsKilled = 0;
    private int _tempNbSkeltalsKilled = 0;
    private int _nbScarabsKilled = 0;
    private int _nbBatsKilled = 0;
    private int _nbSkeltalsKilled = 0;

    private int _knifeEnabled = 0;
    private int _knifeAmmo = 0;
    private int _axeEnabled = 0;
    private int _axeAmmo = 0;
    private int _featherEnabled = 0;
    private int _bootsEnabled = 0;
    private int _bubbleEnabled = 0;
    private int _armorEnabled = 0;
    private int _earthArtefactEnabled = 0;
    private int _airArtefactEnabled = 0;
    private int _waterArtefactEnabled = 0;
    private int _fireArtefactEnabled = 0;

    protected void Start()
    {
        //_controller = GetComponent<DatabaseController>();

        _inventoryManager = StaticObjects.GetPlayer().GetComponent<InventoryManager>();
        _munitions = StaticObjects.GetPlayer().GetComponent<PlayerWeaponAmmo>();
        DestroyEnemyOnDeath.OnEnemyDeath += EnemyKilled;
    }

    public void CreateStatsRecord()
    {
        _dbconnection.Open();
        string sqlQuery = String.Format("INSERT INTO STATS (SECONDS_PLAYED, NB_KILLED_SCARABS, NB_KILLED_BATS, NB_KILLED_SKELTALS, LIFE_REMAINING, NB_DEATHS, KNIFE_PICKED, KNIFE_AMMO, AXE_PICKED, AXE_AMMO, FEATHER_PICKED, BOOTS_PICKED, BUBBLE_PICKED, ARMOR_PICKED, ARTEFACT1_PICKED, ARTEFACT2_PICKED, ARTEFACT3_PICKED, ARTEFACT4_PICKED, GAME_COMPLETED, ACCOUNT_ID)" +
            "VALUES (0, 0, 0, 0, 1000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, {0})", _controller.AccountID);
        _dbcommand.CommandText = sqlQuery;
        _dbcommand.ExecuteNonQuery();
        _dbconnection.Close();

        StartCountingSecondsPlayed();
    }

    public void SaveTemporaryStats()
    {
        _nbScarabsKilled = _tempNbScarabsKilled;
        _nbBatsKilled = _tempNbBatsKilled;
        _nbSkeltalsKilled = _tempNbSkeltalsKilled;
        _lifeRemaining = StaticObjects.GetPlayer().GetComponent<Health>().HealthPoint;
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

    public void SaveStats()
    {
        _dbconnection.Open();
        string sqlQuery = String.Format("UPDATE STATS SET SECONDS_PLAYED = {0}, NB_KILLED_SCARABS = {1}, NB_KILLED_BATS = {2}, NB_KILLED_SKELTALS = {3}, LIFE_REMAINING = {4}, KNIFE_PICKED = {5}, KNIFE_AMMO = {6}, AXE_PICKED = {7}, AXE_AMMO = {8}, FEATHER_PICKED = {9}, BOOTS_PICKED = {10}, BUBBLE_PICKED = {11}, ARMOR_PICKED = {12}, ARTEFACT1_PICKED = {13}, ARTEFACT2_PICKED = {14}, ARTEFACT3_PICKED = {15}, ARTEFACT4_PICKED = {16}" +
            " WHERE ACCOUNT_ID = {17}", _secondsPlayed, _nbScarabsKilled, _nbBatsKilled, _nbSkeltalsKilled, _lifeRemaining, _knifeEnabled, _knifeAmmo, _axeEnabled, _axeAmmo, _featherEnabled, _bootsEnabled, _bubbleEnabled, _armorEnabled, _earthArtefactEnabled, _airArtefactEnabled, _waterArtefactEnabled, _fireArtefactEnabled, _controller.AccountID);
        _dbcommand.CommandText = sqlQuery;
        _dbcommand.ExecuteNonQuery();
        _dbconnection.Close();
    }

    public void LoadStats()
    {
        _dbconnection.Open();
        string sqlQuery = String.Format("SELECT SECONDS_PLAYED, NB_KILLED_SCARABS, NB_KILLED_BATS, NB_KILLED_SKELTALS, LIFE_REMAINING, KNIFE_PICKED, KNIFE_AMMO, AXE_PICKED, AXE_AMMO, FEATHER_PICKED, BOOTS_PICKED, BUBBLE_PICKED, ARMOR_PICKED, ARTEFACT1_PICKED, ARTEFACT2_PICKED, ARTEFACT3_PICKED, ARTEFACT4_PICKED" +
            " FROM STATS WHERE ACCOUNT_ID = {0}", _controller.AccountID);
        _dbcommand.CommandText = sqlQuery;
        IDataReader reader = _dbcommand.ExecuteReader();
        while (reader.Read())
        {
            _secondsPlayed = reader.GetInt32(0);
            _tempNbScarabsKilled = reader.GetInt32(1);
            _tempNbBatsKilled = reader.GetInt32(2);
            _tempNbSkeltalsKilled = reader.GetInt32(3);
            _lifeRemaining = reader.GetInt32(4);
            _knifeEnabled = reader.GetInt32(5);
            _knifeAmmo = reader.GetInt32(6);
            _axeEnabled = reader.GetInt32(7);
            _axeAmmo = reader.GetInt32(8);
            _featherEnabled = reader.GetInt32(9);
            _bootsEnabled = reader.GetInt32(10);
            _bubbleEnabled = reader.GetInt32(11);
            _armorEnabled = reader.GetInt32(12);
            _earthArtefactEnabled = reader.GetInt32(13);
            _airArtefactEnabled = reader.GetInt32(14);
            _waterArtefactEnabled = reader.GetInt32(15);
            _fireArtefactEnabled = reader.GetInt32(16);
        }
        reader.Close();
        _dbconnection.Close();

        OnInventoryReloaded(Convert.ToBoolean(_knifeEnabled), Convert.ToBoolean(_axeEnabled), Convert.ToBoolean(_featherEnabled), Convert.ToBoolean(_bootsEnabled), Convert.ToBoolean(_bubbleEnabled), Convert.ToBoolean(_armorEnabled), Convert.ToBoolean(_earthArtefactEnabled), Convert.ToBoolean(_airArtefactEnabled), Convert.ToBoolean(_waterArtefactEnabled), Convert.ToBoolean(_fireArtefactEnabled));
        OnAmmoReloaded(_knifeAmmo, _axeAmmo);
        OnHealthReloaded(_lifeRemaining);

        StartCountingSecondsPlayed();
    }

    private void EnemyKilled(string tag)
    {
        if (tag == StaticObjects.GetUnityTags().Scarab)
        {
            _tempNbScarabsKilled++;
        }
        else if (tag == StaticObjects.GetUnityTags().Bat)
        {
            _tempNbBatsKilled++;
        }
        else if (tag == StaticObjects.GetUnityTags().Skeltal)
        {
            _tempNbSkeltalsKilled++;
        }
    }

    private void StartCountingSecondsPlayed()
    {
        //StartCoroutine(CountSecondsPlayed());
    }

    private IEnumerator CountSecondsPlayed()
    {
        while (true)
        {
            _secondsPlayed++;
            yield return new WaitForSeconds(1);
        }
    }
}
