using UnityEngine;
using System.Collections;

public class DropItems : MonoBehaviour
{

    [SerializeField]
    private GameObject[] _items;

    [SerializeField]
    private int[] _dropRates;

    private GameObject[] _itemsToDrop;
    private int _itemsDroppedCount = 0;

    public void Drop()
    {
        _itemsToDrop = new GameObject[_items.Length];

        for (int i = 0; i < _items.Length; i++)
        {
            if (Random.Range(0, 100) < _dropRates[i])
            {
                if ((_items[i].gameObject.tag != "AxeDrop" && _items[i].gameObject.tag != "KnifeDrop") ||
                    (_items[i].gameObject.tag == "AxeDrop" && GameObject.Find("Character").GetComponent<InventoryManager>().AxeEnabled) ||
                    (_items[i].gameObject.tag == "KnifeDrop" && GameObject.Find("Character").GetComponent<InventoryManager>().KnifeEnabled))
                {
                    _itemsToDrop[_itemsDroppedCount++] = _items[i];
                }
            }
        }

        for (int j = 0; j < _itemsToDrop.Length; j++)
        {
            if (_itemsToDrop[j] == null)
            {
                break;
            }
            GameObject item = (GameObject)Instantiate(_itemsToDrop[j], transform.position, new Quaternion());
            item.GetComponent<OnItemDrop>().Initialise(_itemsDroppedCount, j, GetComponent<Collider2D>());
        }
    }
}
