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

    public WeaponType SelectedThrowWeapon { get; private set; }

    public bool HasInfiniteKnives { get; private set; }
    public bool HasInfiniteAxes { get; private set; }

    public bool AirArtefactEnabled { get; private set; }
    public bool EarthArtefactEnabled { get; private set; }
    public bool WaterArtefactEnabled { get; private set; }
    public bool FireArtefactEnabled { get; private set; }

    private UIInventoryView _inventoryView;
    private ThrowAxe _throwAxeAttack;
    private ThrowKnife _throwKnifeAttack;

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
        AccountStats.OnInventoryReloaded += ReloadInventory;

        _inventoryView = StaticObjects.GetItemCanvas().GetComponent<UIInventoryView>();
        GetComponentInChildren<InputManager>().OnThrowAttackChangeButtonPressed += OnSwitchWeapon;

        _throwKnifeAttack = GetComponent<ThrowKnife>();
        _throwAxeAttack = GetComponent<ThrowAxe>();

        SelectedThrowWeapon = WeaponType.None;
    }

    public void ReloadInventory(bool knifeEnabled, bool axeEnabled, bool featherEnabled, bool bootsEnabled, bool bubbleEnabled, bool armorEnabled, bool earthArtefactEnabled, bool airArtefactEnabled, bool waterArtefactEnabled, bool fireArtefactEnabled)
    {
        if (knifeEnabled)
        {
            EnableKnife();
        }
        if (axeEnabled)
        {
            EnableAxe();
        }
        if (featherEnabled)
        {
            EnableFeather();
        }
        if (bootsEnabled)
        {
            EnableIronBoots();
        }
        if (bubbleEnabled)
        {
            EnableBubble();
        }
        if (armorEnabled)
        {
            EnableFireProofArmor();
        }
        if (earthArtefactEnabled)
        {
            EnableEarthArtefact();
        }
        if (airArtefactEnabled)
        {
            EnableAirArtefact();
        }
        if (waterArtefactEnabled)
        {
            EnableWaterArtefact();
        }
        if (fireArtefactEnabled)
        {
            EnableFireArtefact();
        }
    }

    public void SetInfiniteAmmoEvent(GameObject ammoObject)
    {
        ammoObject.GetComponent<InfiniteAmmoWhileInPickupRoom>().OnSetInfiniteAmmo += SetInfiniteAmmo;
    }

    private void OnSwitchWeapon()
    {
        switch (SelectedThrowWeapon)
        {
            case WeaponType.Axe:
                {
                    if (KnifeEnabled)
                    {
                        SelectKnife();
                    }
                    break;
                }
            case WeaponType.Knife:
                {
                    if (AxeEnabled)
                    {
                        SelectAxe();
                    }
                    break;
                }
        }
    }

    private void SelectAxe()
    {
        SelectedThrowWeapon = WeaponType.Axe;
        _inventoryView.SelectAxe();
        _throwKnifeAttack.enabled = false;
        _throwAxeAttack.enabled = true;
    }

    private void SelectKnife()
    {
        SelectedThrowWeapon = WeaponType.Knife;
        _inventoryView.SelectKnife();
        _throwAxeAttack.enabled = false;
        _throwKnifeAttack.enabled = true;
    }

    private void SetInfiniteAmmo(GameObject item, bool enable)
    {
        if (item.tag == StaticObjects.GetUnityTags().Knife)
        {
            HasInfiniteKnives = enable;
        }
        else if (item.tag == StaticObjects.GetUnityTags().Axe)
        {
            HasInfiniteAxes = enable;
        }
    }

    public void EnableKnife()
    {
        KnifeEnabled = true;
        SelectKnife();
        OnEnableKnife();
    }

    public void EnableAxe()
    {
        AxeEnabled = true;
        SelectAxe();
        OnEnableAxe();
    }

    public void EnableIronBoots()
    {
        IronBootsEnabled = true;
        OnEnableIronBoots();
    }

    public void EnableFeather()
    {
        FeatherEnabled = true;
        OnEnableFeather();
    }

    public void EnableBubble()
    {
        BubbleEnabled = true;
        OnEnableBubble();
    }

    public void EnableFireProofArmor()
    {
        FireProofArmorEnabled = true;
        OnEnableFireProofArmor();
    }

    public void EnableEarthArtefact()
    {
        EarthArtefactEnabled = true;
        OnEnableEarthArtefact();
    }

    public void EnableAirArtefact()
    {
        AirArtefactEnabled = true;
        OnEnableAirArtefact();
    }

    public void EnableWaterArtefact()
    {
        WaterArtefactEnabled = true;
        OnEnableWaterArtefact();
    }

    public void EnableFireArtefact()
    {
        FireArtefactEnabled = true;
        OnEnableFireArtefact();
    }
}
