using UnityEngine;
using System.Collections;

public class MoveObjectOnEntityDestroyed : MoveObjectOnEvent
{
    [SerializeField]
    private GameObject _entity;

    protected override void Start()
    {
        _entity.GetComponent<Health>().OnDeath += StartObjectMovement;

        base.Start();
    }
}
