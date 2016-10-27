using UnityEngine;
using System.Collections;

public class InventoryManager : MonoBehaviour
{
    public delegate void OnThrowableWeaponChangeHandler(WeaponTypes weaponTypes);
    public event OnThrowableWeaponChangeHandler OnThrowableWeaponChange;

    public enum WeaponTypes { Knife, Axe }

    private bool _knifeEnabled = false;
    private bool _axeEnabled = false;
    private bool _knifeActive = false;
    private bool _axeActive = false;
    private bool _ironBootsEnabled = false;
    private bool _ironBootsActive = false;
    private bool _featherEnabled = false;
    private bool _bubbleEnabled = false;
    private bool _fireProofArmorEnabled = false;
    //private bool _earthKeyEnabled = false;
    //private bool _airKeyEnabled = false;
    //private bool _waterKeyEnabled = false;
    //private bool _fireKeyEnabled = false;

    public bool KnifeEnabled { get { return _knifeEnabled; } }
    public bool AxeEnabled { get { return _axeEnabled; } }
    public bool KnifeActive { get { return _knifeActive; } set { _knifeActive = value; } }
    public bool AxeActive { get { return _axeActive; } set { _axeActive = value; } }
    public bool IronBootsEnabled { get { return _ironBootsEnabled; } }
    public bool IronBootsActive { get { return _ironBootsActive; } set { _ironBootsActive = value; } }
    public bool FeatherEnabled { get { return _featherEnabled; } }
    public bool BubbleEnabled { get { return _bubbleEnabled; } }
    public bool FireProofArmorEnabled { get { return _fireProofArmorEnabled; } }
    //public bool EarthKeyEnabled { get { return _earthKeyEnabled; } }
    //public bool AirKeyEnabled { get { return _airKeyEnabled; } }
    //public bool WaterKeyEnabled { get { return _waterKeyEnabled; } }
    //public bool FireKeyEnabled { get { return _fireKeyEnabled; } }


    public void EnableKnife()
    {
        _knifeEnabled = true;

        if (OnThrowableWeaponChange != null)
        {
            OnThrowableWeaponChange(WeaponTypes.Knife);
        }
    }

    public void EnableAxe()
    {
        _axeEnabled = true;

        if (OnThrowableWeaponChange != null)
        {
            OnThrowableWeaponChange(WeaponTypes.Axe);
        }
    }

    public void EnableIronBoots()
    {
        _ironBootsEnabled = true;
    }

    public void EnableFeather()
    {
        _featherEnabled = true;
    }

    public void EnableBubble()
    {
        _bubbleEnabled = true;
    }

    public void EnableFireProofArmor()
    {
        _fireProofArmorEnabled = true;
    }

    //public void EnableEarthKey()
    //{
    //    _earthKeyEnabled = true;
    //}

    //public void EnableAirKey()
    //{
    //    _airKeyEnabled = true;
    //}

    //public void EnableWaterKey()
    //{
    //    _waterKeyEnabled = true;
    //}

    //public void EnableFireKey()
    //{
    //    _fireKeyEnabled = true;
    //}
}
