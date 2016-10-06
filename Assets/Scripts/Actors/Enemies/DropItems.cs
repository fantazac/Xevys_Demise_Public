using UnityEngine;
using System.Collections;

public class DropItems : MonoBehaviour
{

    [SerializeField]
    private GameObject[] _items;

    [SerializeField]
    private int[] _dropRates;

    public void Drop()
    {
        for(int i = 0; i < _items.Length; i++)
        {
            if (Random.Range(0, 100) < _dropRates[i])
            {
                GameObject newDrop2 = (GameObject)Instantiate(_items[i], transform.position, new Quaternion());
            }
        }
    }
}
