using UnityEngine;
using System.Collections;

public class InventoryManager : MonoBehaviour
{
    private bool _knifeEnabled = false;
    private bool _axeEnabled = false;
    private bool _knifeActive = false;
    private bool _axeActive = false;

    private bool _featherEnabled = false;

    public bool KnifeEnabled { get { return _knifeEnabled; } }
    public bool AxeEnabled { get { return _axeEnabled; } }
    public bool KnifeActive { get { return _knifeActive; } set { _knifeActive = value; } }
    public bool AxeActive { get { return _axeActive; } set { _knifeActive = value; } }

    public bool FeatherEnabled { get { return _featherEnabled; } }

    public void EnableKnife()
    {
        _knifeEnabled = true;
        _knifeActive = true;
    }

    public void EnableAxe()
    {
        _axeEnabled = true;
        _axeActive = true;
    }

    public void EnableFeather()
    {
        _featherEnabled = true;
    }
}
