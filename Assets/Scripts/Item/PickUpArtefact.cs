using UnityEngine;
using System.Collections;

public class PickUpArtefact : MonoBehaviour
{
    private InventoryManager _inventoryManager;

    private void Start()
    {
        _inventoryManager = StaticObjects.GetPlayer().GetComponent<InventoryManager>();
        GetComponent<ActivateTrigger>().OnTrigger += EnableArtefactsInInventory;
    }

    private void EnableArtefactsInInventory()
    {
        if (gameObject.tag == "EarthArtefactItem")
        {
            _inventoryManager.EnableEarthArtefact();
        }
        else if (gameObject.tag == "AirArtefactItem")
        {
            _inventoryManager.EnableAirArtefact();
        }
        else if (gameObject.tag == "WaterArtefactItem")
        {
            _inventoryManager.EnableWaterArtefact();
        }
        else if (gameObject.tag == "FireArtefactItem")
        {
            _inventoryManager.EnableFireArtefact();
        }
    }
}
