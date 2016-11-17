using UnityEngine;
using System.Collections;

public class PickUpWeaponAmmo : MonoBehaviour
{
    [SerializeField]
    private int _ammoOnDrop = 2;

    private PlayerThrowingWeaponsMunitions _munitions;
    private ActivateTrigger _trigger;

    private void Start()
    {
        _munitions = StaticObjects.GetPlayer().GetComponent<PlayerThrowingWeaponsMunitions>();
        _trigger = GetComponent<ActivateTrigger>();
        _trigger.OnTrigger += AddAmmoToInventory;
    }

    private void AddAmmoToInventory()
    {
        if (gameObject.tag == "KnifePickableItem")
        {
            _munitions.AddKnifeAmmo(_ammoOnDrop);
        }
        else if (gameObject.tag == "AxePickableItem")
        {
            _munitions.AddAxeAmmo(_ammoOnDrop);
        }
    }
}
