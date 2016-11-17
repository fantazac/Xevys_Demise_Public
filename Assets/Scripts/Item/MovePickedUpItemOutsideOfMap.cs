using UnityEngine;
using System.Collections;

public class MovePickedUpItemOutsideOfMap : MonoBehaviour {

    private ActivateTrigger _trigger;
    private Animator _animator;

    private void Start()
    {
        _trigger = GetComponent<ActivateTrigger>();
        _trigger.OnTrigger += MoveObjectOutside;

        _animator = GetComponent<Animator>();
    }

    private void MoveObjectOutside()
    {
        _animator.SetTrigger("ItemPickedUp");
    }
}
