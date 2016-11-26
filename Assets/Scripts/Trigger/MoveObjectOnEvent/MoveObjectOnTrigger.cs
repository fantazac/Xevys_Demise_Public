using UnityEngine;
using System.Collections;

public class MoveObjectOnTrigger : MoveObjectOnEvent
{
    [SerializeField]
    private GameObject _triggerActivationObject;

    protected override void Start()
    {
        _triggerActivationObject.GetComponent<ActivateTrigger>().OnTrigger += StartObjectMovement;

        base.Start();
    }
}
