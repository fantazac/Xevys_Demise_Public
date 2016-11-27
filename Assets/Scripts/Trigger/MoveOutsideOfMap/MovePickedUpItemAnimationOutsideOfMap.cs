using UnityEngine;
using System.Collections;

public class MovePickedUpItemAnimationOutsideOfMap : MoveOutsideOfMap
{
    private Animator _animator;

    protected void Start()
    {
        GetComponent<ActivateTrigger>().OnTrigger += MoveObjectOutside;

        _animator = GetComponent<Animator>();
    }

    protected override void MoveObjectOutside()
    {
        _animator.SetTrigger(StaticObjects.GetAnimationTags().ItemPickedUp);
    }
}
