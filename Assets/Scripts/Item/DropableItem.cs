using UnityEngine;
using System.Collections;

public class DropableItem : MonoBehaviour
{
    [SerializeField]
    private GameObject _item;

    [SerializeField]
    private int _dropRate;

    public GameObject Item { get { return _item; } }
    public int DropRate { get { return _dropRate; } }
}
