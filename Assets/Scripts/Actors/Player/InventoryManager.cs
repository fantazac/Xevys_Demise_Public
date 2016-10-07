using UnityEngine;
using System.Collections;

public class InventoryManager : MonoBehaviour
{

    private bool _knifeEnabled = false;
    private bool _axeEnabled = false;

    public bool KnifeEnabled { get { return _knifeEnabled; } }
    public bool AxeEnabled { get { return _axeEnabled; } }

    public void EnableKnife()
    {
        _knifeEnabled = true;
    }

    public void EnableAxe()
    {
        _axeEnabled = true;
    }
}
