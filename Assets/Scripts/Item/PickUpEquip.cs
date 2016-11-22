using UnityEngine;
using System.Collections;

public class PickUpEquip : MonoBehaviour
{
    private InventoryManager _inventoryManager;
    private UnityTags _unityTags;

    private void Start()
    {
        _inventoryManager = StaticObjects.GetPlayer().GetComponent<InventoryManager>();
        GetComponent<ActivateTrigger>().OnTrigger += EnableEquipsInInventory;
        _unityTags = StaticObjects.GetUnityTags();
    }

    private void EnableEquipsInInventory()
    {
        if (gameObject.tag == _unityTags.FeatherItem)
        {
            _inventoryManager.EnableFeather();
        }
        else if (gameObject.tag == _unityTags.IronBootsItem)
        {
            _inventoryManager.EnableIronBoots();
        }
        else if (gameObject.tag == _unityTags.BubbleItem)
        {
            _inventoryManager.EnableBubble();
        }
        else if (gameObject.tag == _unityTags.FireProofArmorItem)
        {
            _inventoryManager.EnableFireProofArmor();
        }
    }
}
