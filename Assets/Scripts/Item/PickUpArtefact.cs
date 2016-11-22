using UnityEngine;
using System.Collections;

public class PickUpArtefact : MonoBehaviour
{
    private InventoryManager _inventoryManager;
    private UnityTags _unityTags;

    private void Start()
    {
        _inventoryManager = StaticObjects.GetPlayer().GetComponent<InventoryManager>();
        GetComponent<ActivateTrigger>().OnTrigger += EnableArtefactsInInventory;
        _unityTags = StaticObjects.GetUnityTags();
    }

    private void EnableArtefactsInInventory()
    {
        if (gameObject.tag == _unityTags.EarthArtefactItem)
        {
            _inventoryManager.EnableEarthArtefact();
        }
        else if (gameObject.tag == _unityTags.AirArtefactItem)
        {
            _inventoryManager.EnableAirArtefact();
        }
        else if (gameObject.tag == _unityTags.WaterArtefactItem)
        {
            _inventoryManager.EnableWaterArtefact();
        }
        else if (gameObject.tag == _unityTags.FireArtefactItem)
        {
            _inventoryManager.EnableFireArtefact();
        }
    }
}
