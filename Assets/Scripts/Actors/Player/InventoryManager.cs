using UnityEngine;
using System.Collections;

public class InventoryManager : MonoBehaviour
{

    private bool _knifeEnabled = false;
    private bool _axeEnabled = false;
    private bool _featherEnabled = false;

    public bool KnifeEnabled { get { return _knifeEnabled; } }
    public bool AxeEnabled { get { return _axeEnabled; } }
    public bool FeatherEnabled { get { return _featherEnabled; } }

    public void EnableKnife()
    {
        _knifeEnabled = true;
    }

    public void EnableAxe()
    {
        _axeEnabled = true;
    }

    public void EnableFeather()
    {
        _featherEnabled = true;
    }
}
