using UnityEngine;
using System.Collections;

public class PickUpWeaponAmmo : MonoBehaviour
{
    [SerializeField]
    private int _ammoOnDrop = 2;

    private PlayerWeaponAmmo _munitions;

    private void Start()
    {
        _munitions = StaticObjects.GetPlayer().GetComponent<PlayerWeaponAmmo>();
        GetComponent<ActivateTrigger>().OnTrigger += AddAmmoToMunitionsInventory;
    }

    private void AddAmmoToMunitionsInventory()
    {
        if (gameObject.tag == StaticObjects.GetObjectTags().KnifePickableItem)
        {
            _munitions.AddKnifeAmmo(_ammoOnDrop);
        }
        else if (gameObject.tag == StaticObjects.GetObjectTags().AxePickableItem)
        {
            _munitions.AddAxeAmmo(_ammoOnDrop);
        }
    }
}
