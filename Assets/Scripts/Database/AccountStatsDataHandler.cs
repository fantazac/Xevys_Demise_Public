﻿using System.Collections;
using UnityEngine;

public class AccountStatsDataHandler : MonoBehaviour
{
    public delegate void OnInventoryReloadedHandler(bool knifeEnabled, bool axeEnabled, bool featherEnabled, bool bootsEnabled, bool bubbleEnabled, bool armorEnabled, bool earthArtefactEnabled, bool airArtefactEnabled, bool waterArtefactEnabled, bool fireArtefactEnabled);
    public event OnInventoryReloadedHandler OnInventoryReloaded;
    public delegate void OnAmmoReloadedHandler(int knifeAmmo, int axeAmmo);
    public event OnAmmoReloadedHandler OnAmmoReloaded;
    public delegate void OnHealthReloadedHandler(int health);
    public event OnHealthReloadedHandler OnHealthReloaded;

    private AccountStatsEntity _temporaryEntity;
    private AccountStatsEntity _entity;
    private AccountStatsRepository _repository;

    private void Start()
    {
        _repository = new AccountStatsRepository();
        _temporaryEntity = _repository.Get(StaticObjects.AccountId);
        DestroyEnemyOnDeath.OnEnemyDeath += EnemyKilled;
    }

    public void CreateNewEntry(int accountId)
    {
        AccountStatsEntity entity = new AccountStatsEntity();
        entity.AccountId = accountId;
        entity.LifeRemaining = 1000;
        _repository.Add(entity);
    }

    public void UpdateEntity()
    {
        _entity = _temporaryEntity;
    }

    public void ChangeEntity(int accountId)
    {
        _temporaryEntity = _repository.Get(accountId);
        UpdateEntity();
    }

    public void UpdateRepository()
    {
        _repository.UpdateEntity(_entity);
    }

    private void HealthChanged(int heal)
    {
        _temporaryEntity.LifeRemaining += heal;
    }

    private void EnemyKilled(string tag)
    {
        if (tag == StaticObjects.GetObjectTags().Scarab)
        {
            _temporaryEntity.NbScarabsKilled++;
        }
        else if (tag == StaticObjects.GetObjectTags().Bat)
        {
            _temporaryEntity.NbBatsKilled++;
        }
        else if (tag == StaticObjects.GetObjectTags().Skeltal)
        {
            _temporaryEntity.NbSkeltalsKilled++;
        }
    }

    public void ActivateInventory()
    {
        GameObject player = StaticObjects.GetPlayer();
        InventoryManager inventory = player.GetComponent<InventoryManager>();
        inventory.OnEnableKnife += EnableKnife;
        inventory.OnEnableAxe += EnableAxe;
        inventory.OnEnableIronBoots += EnableIronBoots;
        inventory.OnEnableFeather += EnableFeather;
        inventory.OnEnableBubble += EnableBubble;
        inventory.OnEnableFireProofArmor += EnableFireProofArmor;
        inventory.OnEnableEarthArtefact += EnableEarthArtefact;
        inventory.OnEnableAirArtefact += EnableAirArtefact;
        inventory.OnEnableWaterArtefact += EnableWaterArtefact;
        inventory.OnEnableFireArtefact += EnableFireArtefact;

        player.GetComponent<Health>().OnHealthChanged += HealthChanged;

        PlayerWeaponAmmo playerWeaponAmmo = player.GetComponent<PlayerWeaponAmmo>();
        playerWeaponAmmo.OnKnifeAmmoChanged += ChangeKnifeAmmo;
        playerWeaponAmmo.OnAxeAmmoChanged += ChangeAxeAmmo;

        OnInventoryReloaded(_entity.KnifeEnabled, _entity.AxeEnabled, _entity.FeatherEnabled, _entity.BootsEnabled, _entity.BubbleEnabled, _entity.ArmorEnabled, _entity.EarthArtefactEnabled, _entity.AirArtefactEnabled, _entity.WaterArtefactEnabled, _entity.FireArtefactEnabled);
        OnAmmoReloaded(_entity.KnifeAmmo, _entity.AxeAmmo);
        OnHealthReloaded(_entity.LifeRemaining);
    }

    private void StartSecondsPlayedCounter()
    {
        StartCoroutine(SecondsPlayedCounter());
    }

    private IEnumerator SecondsPlayedCounter()
    {
        while (true)
        {
            _temporaryEntity.SecondsPlayed++;
            yield return new WaitForSeconds(1);
        }
    }

    private void EnableKnife()
    {
        _temporaryEntity.KnifeEnabled = true;
    }

    private void ChangeKnifeAmmo(int ammo)
    {
        _temporaryEntity.KnifeAmmo = ammo;
    }

    private void EnableAxe()
    {
        _temporaryEntity.AxeEnabled = true;
    }

    private void ChangeAxeAmmo(int ammo)
    {
        _temporaryEntity.AxeAmmo = ammo;
    }

    private void EnableIronBoots()
    {
        _temporaryEntity.BootsEnabled = true;
    }

    private void EnableFeather()
    {
        _temporaryEntity.FeatherEnabled = true;
    }

    private void EnableBubble()
    {
        _temporaryEntity.BubbleEnabled = true;
    }

    private void EnableFireProofArmor()
    {
        _temporaryEntity.ArmorEnabled = true;
    }

    private void EnableEarthArtefact()
    {
        _temporaryEntity.EarthArtefactEnabled = true;
    }

    private void EnableAirArtefact()
    {
        _temporaryEntity.AirArtefactEnabled = true;
    }

    private void EnableWaterArtefact()
    {
        _temporaryEntity.WaterArtefactEnabled = true;
    }

    private void EnableFireArtefact()
    {
        _temporaryEntity.FireArtefactEnabled = true;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
