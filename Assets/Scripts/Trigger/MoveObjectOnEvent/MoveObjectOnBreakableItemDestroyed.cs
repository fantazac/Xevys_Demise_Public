using UnityEngine;
using System.Collections;

public class MoveObjectOnBreakableItemDestroyed : MoveObjectOnEvent
{
    [SerializeField]
    private GameObject _breakableItem;

    protected override void Start()
    {
        _breakableItem.GetComponent<Health>().OnDeath += StartObjectMovement;

        base.Start();
    }
}
