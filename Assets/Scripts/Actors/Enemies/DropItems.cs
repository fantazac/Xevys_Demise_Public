using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class DropItems : MonoBehaviour
{

    [SerializeField]
    private GameObject[] _items;

    [SerializeField]
    private List<int> _dropRates;

    private List<GameObject> _itemsToDrop;

    private int itemToDropCount = 0;

    private float _timeBeforeDrop = 0.2f;
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
        _itemsToDrop = new List<GameObject>();

        foreach (GameObject item in _items)
        {
            if (Random.Range(0, 100) < _dropRates[0])
            {
                _itemsToDrop.Add(item);
            }
            _dropRates.RemoveAt(0);
        }
    }

    private void SetupDrop()
    {
        StartCoroutine("Drop");
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
        return _inventoryManager.KnifeEnabled && item.tag == "KnifeDrop";
    }

    private bool CanDropAxe(GameObject item)
    {
        return _inventoryManager.AxeEnabled && item.tag == "AxeDrop";
    }
}
