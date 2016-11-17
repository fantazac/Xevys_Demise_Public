using UnityEngine;
using System.Collections;

public class PickUpEquip : MonoBehaviour
{
    private InventoryManager _inventoryManager;
    private ActivateTrigger _trigger;

    private void Start()
    {
        _inventoryManager = StaticObjects.GetPlayer().GetComponent<InventoryManager>();
        _trigger = GetComponent<ActivateTrigger>();
        _trigger.OnTrigger += AddAmmoToInventory;
    }

    private void AddAmmoToInventory()
    {
        if (gameObject.tag == "FeatherItem")
        {
            _inventoryManager.EnableFeather();
        }
        else if (gameObject.tag == "IronBootsItem")
        {
            _inventoryManager.EnableIronBoots();
        }
        else if (gameObject.tag == "BubbleItem")
        {
            _inventoryManager.EnableBubble();
        }
        else if (gameObject.tag == "FireProofArmorItem")
        {
            _inventoryManager.EnableFireProofArmor();
        }
    }
}
