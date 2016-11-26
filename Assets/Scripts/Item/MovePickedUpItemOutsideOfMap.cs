using UnityEngine;
using System.Collections;

public class MovePickedUpItemOutsideOfMap : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        GetComponent<ActivateTrigger>().OnTrigger += MoveObjectOutside;

        _animator = GetComponent<Animator>();
    }

    private void MoveObjectOutside()
    {
        _animator.SetTrigger(StaticObjects.GetAnimationTags().ItemPickedUp);
    }
}
