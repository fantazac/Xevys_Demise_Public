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

    private bool _featherEnabled = false;

    public bool KnifeEnabled { get { return _knifeEnabled; } }
    public bool AxeEnabled { get { return _axeEnabled; } }
    public bool KnifeActive { get { return _knifeActive; } set { _knifeActive = value; } }
    public bool AxeActive { get { return _axeActive; } set { _axeActive = value; } }

    public bool FeatherEnabled { get { return _featherEnabled; } }

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

    public void EnableFeather()
    {
        _featherEnabled = true;
    }
}
