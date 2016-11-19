using UnityEngine;
using System.Collections;

public class PickUpEquip : MonoBehaviour
{
    private InventoryManager _inventoryManager;

    private void Start()
    {
        _inventoryManager = StaticObjects.GetPlayer().GetComponent<InventoryManager>();
        GetComponent<ActivateTrigger>().OnTrigger += EnableEquipsInInventory;
    }

    private void EnableEquipsInInventory()
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
