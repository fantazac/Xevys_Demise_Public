using UnityEngine;
using System.Collections;

public class PickUpWeapon : MonoBehaviour
{
    [SerializeField]
    private int _ammoOnUnlockingWeapon = 10;

    private InventoryManager _inventoryManager;
    private PlayerWeaponAmmo _munitions;

    private void Start()
    {
        _inventoryManager = StaticObjects.GetPlayer().GetComponent<InventoryManager>();
        _munitions = StaticObjects.GetPlayer().GetComponent<PlayerWeaponAmmo>();
        GetComponent<ActivateTrigger>().OnTrigger += EnableWeaponInInventory;
    }

    private void EnableWeaponInInventory()
    {
        if (gameObject.tag == StaticObjects.GetUnityTags().BaseKnifeItem)
        {
            _munitions.AddKnifeAmmo(_ammoOnUnlockingWeapon);
            _inventoryManager.EnableKnife();
        }
        else if (gameObject.tag == StaticObjects.GetUnityTags().BaseAxeItem)
        {
            _munitions.AddAxeAmmo(_ammoOnUnlockingWeapon);
            _inventoryManager.EnableAxe();
        }
    }
}
