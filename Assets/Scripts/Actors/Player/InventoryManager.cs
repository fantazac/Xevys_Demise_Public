﻿using UnityEngine;
using System.Collections;

public class InventoryManager : MonoBehaviour
{
    public delegate void OnThrowableWeaponChangeHandler(WeaponTypes weaponTypes);
    public event OnThrowableWeaponChangeHandler OnThrowableWeaponChange;

    public enum WeaponTypes { Knife, Axe }

    public bool KnifeEnabled { get; private set; }
    public bool AxeEnabled { get; private set; }
    public bool KnifeActive { get; set; }
    public bool AxeActive { get; set; }
    public bool IronBootsEnabled { get; private set; }
    public bool IronBootsActive { get; set; }
    public bool FeatherEnabled { get; private set; }
    public bool BubbleEnabled { get; private set; }
    public bool FireProofArmorEnabled { get; private set; }

    public bool AirArtefactEnabled { get; private set; }
    public bool EarthArtefactEnabled { get; private set; }
    public bool WaterArtefactEnabled { get; private set; }
    public bool FireArtefactEnabled { get; private set; }

    public void EnableKnife()
    {
        KnifeEnabled = true;

        if (OnThrowableWeaponChange != null)
        {
            OnThrowableWeaponChange(WeaponTypes.Knife);
        }
    }

    public void EnableAxe()
    {
        AxeEnabled = true;

        if (OnThrowableWeaponChange != null)
        {
            OnThrowableWeaponChange(WeaponTypes.Axe);
        }
    }

    public void EnableIronBoots()
    {
        IronBootsEnabled = true;
    }

    public void EnableFeather()
    {
        FeatherEnabled = true;
    }

    public void EnableBubble()
    {
        BubbleEnabled = true;
    }

    public void EnableFireProofArmor()
    {
        FireProofArmorEnabled = true;
    }

    public void EnableEarthArtefact()
    {
        EarthArtefactEnabled = true;
    }

    public void EnableAirArtefact()
    {
        AirArtefactEnabled = true;
    }

    public void EnableWaterArtefact()
    {
        WaterArtefactEnabled = true;
    }

    public void EnableFireArtefact()
    {
        FireArtefactEnabled = true;
    }
}
