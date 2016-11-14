using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerThrowingWeaponsMunitions : MonoBehaviour
{

    public int AxeAmmo { get; private set; }
    public int KnifeAmmo { get; private set; }

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
        KnifeAmmo = _inventoryManager.HasInfiniteKnives && KnifeAmmo <= ammoUsedOnThrow ?
            KnifeAmmo = 1 : KnifeAmmo -= ammoUsedOnThrow;
        OnKnifeAmmoChanged(KnifeAmmo);
    }

    private void AxeAmmoUsed(int ammoUsedOnThrow)
    {
        AxeAmmo = _inventoryManager.HasInfiniteAxes && AxeAmmo <= ammoUsedOnThrow ?
            AxeAmmo = 1 : AxeAmmo -= ammoUsedOnThrow;
        OnAxeAmmoChanged(AxeAmmo);
    }

    public void AddKnifeAmmo(int ammoToAdd)
    {
        KnifeAmmo += ammoToAdd;
        OnKnifeAmmoChanged(KnifeAmmo);
    }

    public void AddAxeAmmo(int ammoToAdd)
    {
        AxeAmmo += ammoToAdd;
        OnAxeAmmoChanged(AxeAmmo);
    }

    public void ReloadAmmo(int knifeAmmo, int axeAmmo)
    {
        KnifeAmmo = knifeAmmo;
        OnKnifeAmmoChanged(KnifeAmmo);
        AxeAmmo = axeAmmo;
        OnAxeAmmoChanged(AxeAmmo);
    }
}