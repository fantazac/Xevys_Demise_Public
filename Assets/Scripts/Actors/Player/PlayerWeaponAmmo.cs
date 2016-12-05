using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerWeaponAmmo : MonoBehaviour
{

    public int AxeAmmo { get; private set; }
    public int KnifeAmmo { get; private set; }

    private InventoryManager _inventoryManager;
    private ThrowKnife _throwKnifeAttack;
    private ThrowAxe _throwAxeAttack;

    public delegate void OnKnifeAmmoChangedHandler(int knifeAmmo);
    public event OnKnifeAmmoChangedHandler OnKnifeAmmoChanged;

    public delegate void OnAxeAmmoChangedHandler(int axeAmmo);
    public event OnAxeAmmoChangedHandler OnAxeAmmoChanged;

    private void Start()
    {
        DontDestroyOnLoadStaticObjects.GetDatabase().GetComponent<AccountStatsDataHandler>().OnAmmoReloaded += ReloadAmmo;
        _inventoryManager = GetComponent<InventoryManager>();

        _throwKnifeAttack = GetComponent<ThrowKnife>();
        _throwAxeAttack = GetComponent<ThrowAxe>();

        _throwAxeAttack.OnAxeAmmoUsed += AxeAmmoUsed;
        _throwKnifeAttack.OnKnifeAmmoUsed += KnifeAmmoUsed;
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
        KnifeAmmo = 0;
        AxeAmmo = 0;
        AddKnifeAmmo(knifeAmmo);
        AddAxeAmmo(axeAmmo);
    }
}