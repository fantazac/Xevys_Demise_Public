using UnityEngine;
using System.Collections;

public class OpenPortalWhenFireArtefactPickedUp : MonoBehaviour
{
    private InventoryManager _inventoryManager;

	private void Start ()
	{
	    _inventoryManager = StaticObjects.GetPlayer().GetComponent<InventoryManager>();
	    _inventoryManager.OnEnableFireArtefact += OnArtefactPickedUp;
	}

    private void OnArtefactPickedUp()
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }
}
