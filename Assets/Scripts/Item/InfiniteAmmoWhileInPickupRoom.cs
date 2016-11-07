using UnityEngine;
using System.Collections;

public class InfiniteAmmoWhileInPickupRoom : MonoBehaviour
{

    [SerializeField]
    private GameObject _itemToSetInfiniteAmmo;

    [SerializeField]
    private GameObject _triggerToEnableInfiniteAmmo;

    [SerializeField]
    private GameObject _triggerToDisableInfiniteAmmo;

    private ActivateTrigger _triggerToEnable;
    private ActivateTrigger _triggerToDisable;

    public delegate void OnSetInfiniteAmmoHandler(GameObject item, bool enable);
    public event OnSetInfiniteAmmoHandler OnSetInfiniteAmmo;

    private void Start()
    {
        _triggerToEnable = _triggerToEnableInfiniteAmmo.GetComponent<ActivateTrigger>();
        _triggerToEnable.OnTrigger += EnableInfiniteAmmo;

        _triggerToDisable = _triggerToDisableInfiniteAmmo.GetComponent<ActivateTrigger>();
        _triggerToDisable.OnTrigger += DisableInfiniteAmmo;

        StaticObjects.GetPlayer().GetComponent<InventoryManager>().SetInfiniteAmmoEvent(gameObject);
    }

    private void EnableInfiniteAmmo()
    {
        OnSetInfiniteAmmo(_itemToSetInfiniteAmmo, true);
    }

    private void DisableInfiniteAmmo()
    {
        OnSetInfiniteAmmo(_itemToSetInfiniteAmmo, false);
    }

}
