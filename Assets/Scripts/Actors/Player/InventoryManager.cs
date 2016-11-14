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

    public delegate void OnEnableKnifeHandler();
    public event OnEnableKnifeHandler OnEnableKnife;

    public delegate void OnEnableAxeHandler();
    public event OnEnableAxeHandler OnEnableAxe;

    public delegate void OnEnableFeatherHandler();
    public event OnEnableFeatherHandler OnEnableFeather;

    public delegate void OnEnableIronBootsHandler();
    public event OnEnableIronBootsHandler OnEnableIronBoots;

    public delegate void OnEnableBubbleHandler();
    public event OnEnableBubbleHandler OnEnableBubble;

    public delegate void OnEnableFireProofArmorHandler();
    public event OnEnableFireProofArmorHandler OnEnableFireProofArmor;

    public delegate void OnEnableAirArtefactHandler();
    public event OnEnableAirArtefactHandler OnEnableAirArtefact;

    public delegate void OnEnableEarthArtefactHandler();
    public event OnEnableEarthArtefactHandler OnEnableEarthArtefact;

    public delegate void OnEnableWaterArtefactHandler();
    public event OnEnableWaterArtefactHandler OnEnableWaterArtefact;

    public delegate void OnEnableFireArtefactHandler();
    public event OnEnableFireArtefactHandler OnEnableFireArtefact;

    public void Start()
    {
        Database.OnInventoryReloaded += ReloadInventory;
    }

    public void ReloadInventory(bool knifeEnabled, bool axeEnabled, bool featherEnabled, bool bootsEnabled, bool bubbleEnabled, bool armorEnabled, bool earthArtefactEnabled, bool airArtefactEnabled, bool waterArtefactEnabled, bool fireArtefactEnabled)
    {
        if(knifeEnabled)
        {
            EnableKnife();
        }
        if(axeEnabled)
        {
            EnableAxe();
        }
        if(featherEnabled)
        {
            EnableFeather();
        }
        if(bootsEnabled)
        {
            EnableIronBoots();
        }
        if(bubbleEnabled)
        {
            EnableBubble();
        }
        if(armorEnabled)
        {
            EnableFireProofArmor();
        }
        if(earthArtefactEnabled)
        {
            EnableEarthArtefact();
        }
        if(airArtefactEnabled)
        {
            EnableAirArtefact();
        }
        if(waterArtefactEnabled)
        {
            EnableWaterArtefact();
        }
        if(fireArtefactEnabled)
        {
            EnableFireArtefact();
        }
    }

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
        if (OnEnableKnife != null)
        {
            KnifeEnabled = true;
            OnEnableKnife();
        }      
    }

    public void EnableAxe()
    {
        if (OnEnableAxe != null)
        {
            AxeEnabled = true;
            OnEnableAxe();
        }       
    }

    public void EnableIronBoots()
    {
        if (OnEnableIronBoots != null)
        {
            IronBootsEnabled = true;
            OnEnableIronBoots();
        }
    }

    public void EnableFeather()
    {
        if (OnEnableFeather != null)
        {
            FeatherEnabled = true;
            OnEnableFeather();
        }
    }

    public void EnableBubble()
    {
        if (OnEnableBubble != null)
        {
            BubbleEnabled = true;
            OnEnableBubble();
        }
    }

    public void EnableFireProofArmor()
    {
        if (OnEnableFireProofArmor != null)
        {
            FireProofArmorEnabled = true;
            OnEnableFireProofArmor();
        }
    }

    public void EnableEarthArtefact()
    {
        if (OnEnableEarthArtefact != null)
        {
            EarthArtefactEnabled = true;
            OnEnableEarthArtefact();
        }
    }

    public void EnableAirArtefact()
    {
        if (OnEnableAirArtefact != null)
        {
            AirArtefactEnabled = true;
            OnEnableAirArtefact();
        }
    }

    public void EnableWaterArtefact()
    {
        if (OnEnableWaterArtefact != null)
        {
            WaterArtefactEnabled = true;
            OnEnableWaterArtefact();
        }
    }

    public void EnableFireArtefact()
    {
        if (OnEnableFireArtefact != null)
        {
            FireArtefactEnabled = true;
            OnEnableFireArtefact();
        }
    }
}
