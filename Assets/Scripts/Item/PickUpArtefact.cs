using UnityEngine;
using System.Collections;

public class PickUpArtefact : MonoBehaviour
{
    private InventoryManager _inventoryManager;
    private GameObjectTags _unityTags;

    public delegate void OnGameEndedHandler();
    public event OnGameEndedHandler OnGameEnded;

    private void Start()
    {
        _inventoryManager = StaticObjects.GetPlayer().GetComponent<InventoryManager>();
        GetComponent<ActivateTrigger>().OnTrigger += EnableArtefactsInInventory;
        _unityTags = StaticObjects.GetObjectTags();
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
        else if (gameObject.tag == _unityTags.VoidArtefactItem)
        {
            OnGameEnded();
        }
    }
}
