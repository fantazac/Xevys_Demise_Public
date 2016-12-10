using UnityEngine;
using System.Collections;

public class ActivateArtefactOnPortal : MonoBehaviour
{
    [SerializeField]
    private int _index;

    private InventoryManager _inventoryManager;

	private void Start ()
	{
	    _inventoryManager = StaticObjects.GetPlayer().GetComponent<InventoryManager>();

	    switch (_index)
	    {
            case 0:
	            _inventoryManager.OnEnableEarthArtefact += OnArtefactPickedUp;
	            break;
            case 1:
                _inventoryManager.OnEnableAirArtefact += OnArtefactPickedUp;
                break;
            case 2:
                _inventoryManager.OnEnableWaterArtefact += OnArtefactPickedUp;
                break;
            case 3:
                _inventoryManager.OnEnableFireArtefact += OnArtefactPickedUp;
                break;
        }
	}

    private void OnArtefactPickedUp()
    {
        GetComponentInParent<SpriteRenderer>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = true;
    }
}
