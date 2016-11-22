using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class DropItems : MonoBehaviour
{
    [SerializeField]
    private float _timeBeforeDrop = 0.2f;

    private DropableItem[] _items;

    private List<GameObject> _itemsToDrop;

    private int itemToDropCount = 0;

    private WaitForSeconds _dropsDelay;

    private InventoryManager _inventoryManager;

    private void Start()
    {
        GetComponent<Health>().OnDeath += SetupDrop;

        _inventoryManager = StaticObjects.GetPlayer().GetComponent<InventoryManager>();

        _dropsDelay = new WaitForSeconds(_timeBeforeDrop);

        InitializeDrops();
    }

    private void InitializeDrops()
    {
        _items = GetComponents<DropableItem>();
        _itemsToDrop = new List<GameObject>();

        foreach (DropableItem item in _items)
        {
            if (Random.Range(0, 100) < item.DropRate)
            {
                _itemsToDrop.Add(item.Item);
            }
        }
    }

    private void SetupDrop()
    {
        StartCoroutine(Drop());
    }

    private IEnumerator Drop()
    {
        yield return _dropsDelay;

        foreach (GameObject itemToDrop in _itemsToDrop)
        {
            if (CanDropItem(itemToDrop))
            {
                GameObject item = (GameObject)Instantiate(itemToDrop, transform.position, new Quaternion());
                item.GetComponent<OnItemDrop>().Initialise(_itemsToDrop.Count, itemToDropCount++, GetComponent<Collider2D>());
            }
        }
    }

    private bool CanDropItem(GameObject item)
    {
        return CanDropAxe(item) || CanDropKnife(item);
    }

    private bool CanDropKnife(GameObject item)
    {
        return _inventoryManager.KnifeEnabled && item.tag == StaticObjects.GetUnityTags().KnifeDrop;
    }

    private bool CanDropAxe(GameObject item)
    {
        return _inventoryManager.AxeEnabled && item.tag == StaticObjects.GetUnityTags().AxeDrop;
    }
}
