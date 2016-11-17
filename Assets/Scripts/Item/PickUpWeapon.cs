using UnityEngine;
using System.Collections;

public class PickUpWeapon : MonoBehaviour
{

    [SerializeField]
    private int _ammoOnUnlockingWeapon = 10;

    private InventoryManager _inventoryManager;
    private PlayerThrowingWeaponsMunitions _munitions;
    private ActivateTrigger _trigger;

    private void Start()
    {
        _inventoryManager = StaticObjects.GetPlayer().GetComponent<InventoryManager>();
        _munitions = StaticObjects.GetPlayer().GetComponent<PlayerThrowingWeaponsMunitions>();
        _trigger = GetComponent<ActivateTrigger>();
        _trigger.OnTrigger += AddAmmoToInventory;
    }

    private void AddAmmoToInventory()
    {
        if (gameObject.tag == "BaseKnifeItem")
        {
            _munitions.AddKnifeAmmo(_ammoOnUnlockingWeapon);
            _inventoryManager.EnableKnife();
        }
        else if (gameObject.tag == "BaseAxeItem")
        {
            _munitions.AddAxeAmmo(_ammoOnUnlockingWeapon);
            _inventoryManager.EnableAxe();
        }
    }

}
