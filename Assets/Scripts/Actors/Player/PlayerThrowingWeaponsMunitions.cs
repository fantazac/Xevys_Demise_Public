using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerThrowingWeaponsMunitions : MonoBehaviour
{
    private int _axeAmmo = 0;
    private int _knifeAmmo = 0;

    public int AxeAmmo { get { return _axeAmmo; } }
    public int KnifeAmmo { get { return _knifeAmmo; } }

    private InventoryManager _inventoryManager;
    private ActorThrowAttack _throwAttack;

    public delegate void OnKnifeAmmoChangedHandler(int knifeAmmo);
    public event OnKnifeAmmoChangedHandler OnKnifeAmmoChanged;

    public delegate void OnAxeAmmoChangedHandler(int axeAmmo);
    public event OnAxeAmmoChangedHandler OnAxeAmmoChanged;

    private void Start()
    {
        Database.OnAmmoReloaded += ReloadAmmo;
        _inventoryManager = GetComponent<InventoryManager>();

        _throwAttack = GetComponent<ActorThrowAttack>();
        _throwAttack.OnAxeAmmoUsed += AxeAmmoUsed;
        _throwAttack.OnKnifeAmmoUsed += KnifeAmmoUsed;
    }

    private void KnifeAmmoUsed(int ammoUsedOnThrow)
    {
        _knifeAmmo -= ammoUsedOnThrow;
        if (_inventoryManager.HasInfiniteKnives && KnifeAmmo == 0)
        {
            _knifeAmmo += 1;
        }
        OnKnifeAmmoChanged(_knifeAmmo);
    }

    private void AxeAmmoUsed(int ammoUsedOnThrow)
    {
        _axeAmmo -= ammoUsedOnThrow;
        if (_inventoryManager.HasInfiniteAxes && AxeAmmo == 0)
        {
            _axeAmmo += 1;
        }
        OnAxeAmmoChanged(_axeAmmo);
    }

    public void AddKnifeAmmo(int ammoToAdd)
    {
        _knifeAmmo += ammoToAdd;
        OnKnifeAmmoChanged(_knifeAmmo);
    }

    public void AddAxeAmmo(int ammoToAdd)
    {
        _axeAmmo += ammoToAdd;
        OnAxeAmmoChanged(_axeAmmo);
    }

    public void ReloadAmmo(int knifeAmmo, int axeAmmo)
    {
        _knifeAmmo = knifeAmmo;
        OnKnifeAmmoChanged(_knifeAmmo);
        _axeAmmo = axeAmmo;
        OnAxeAmmoChanged(_axeAmmo);
    }
}