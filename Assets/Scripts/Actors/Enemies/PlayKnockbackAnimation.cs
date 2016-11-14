using UnityEngine;
using System.Collections;

public class PlayKnockbackAnimation : MonoBehaviour
{

    private Animator _animator;
    private KnockbackOnDamageTaken _knockback;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();

        _knockback = GetComponent<KnockbackOnDamageTaken>();
        _knockback.OnKnockbackStarted += OnKnockbackStarted;
        _knockback.OnKnockbackFinished += OnKnockbackFinished;
    }

    private void OnKnockbackStarted()
    {
        _animator.SetBool("IsDamaged", true);
    }

    private void OnKnockbackFinished()
    {
        _animator.SetBool("IsDamaged", false);
    }

}
