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

    public delegate void OnSetInfiniteAmmoHandler(GameObject item, bool enable);
    public event OnSetInfiniteAmmoHandler OnSetInfiniteAmmo;

    private void Start()
    {
        _triggerToEnableInfiniteAmmo.GetComponent<ActivateTrigger>().OnTrigger += EnableInfiniteAmmo;
        _triggerToDisableInfiniteAmmo.GetComponent<ActivateTrigger>().OnTrigger += DisableInfiniteAmmo;

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
