using UnityEngine;
using System.Collections;

public class InventoryManager : MonoBehaviour
{

    public bool KnifeEnabled { get; private set; }
    public bool AxeEnabled { get; private set; }
    public bool IronBootsEnabled { get; private set; }
    public bool IronBootsActive { get; set; }
    public bool FeatherEnabled { get; private set; }
    public bool BubbleEnabled { get; private set; }
    public bool FireProofArmorEnabled { get; private set; }

    public bool HasInfiniteKnives { get; private set; }
    public bool HasInfiniteAxes { get; private set; }

    public bool AirArtefactEnabled { get; private set; }
    public bool EarthArtefactEnabled { get; private set; }
    public bool WaterArtefactEnabled { get; private set; }
    public bool FireArtefactEnabled { get; private set; }

    public delegate void OnEnableWeaponHandler(WeaponType weaponTypes);
    public event OnEnableWeaponHandler OnEnableWeapon;

    public void SetInfiniteAmmoEvent(GameObject ammoObject)
    {
        ammoObject.GetComponent<InfiniteAmmoWhileInPickupRoom>().OnSetInfiniteAmmo += SetInfiniteAmmo;
    }

    private void SetInfiniteAmmo(GameObject item, bool enable)
    {
        if(item.tag == "Knife")
        {
            HasInfiniteKnives = enable;
        }
        else if (item.tag == "Axe")
        {
            HasInfiniteAxes = enable;
        }
    }

    public void EnableKnife()
    {
        KnifeEnabled = true;

        if (OnEnableWeapon != null)
        {
            OnEnableWeapon(WeaponType.Knife);
        }
    }

    public void EnableAxe()
    {
        AxeEnabled = true;

        if (OnEnableWeapon != null)
        {
            OnEnableWeapon(WeaponType.Axe);
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
