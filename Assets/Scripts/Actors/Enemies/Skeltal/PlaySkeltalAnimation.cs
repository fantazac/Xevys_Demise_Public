using UnityEngine;
using System.Collections;

public class PlaySkeltalAnimation : MonoBehaviour
{
    private Animator _animator;
    private SkeltalBehaviour _skeltalBehavior;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        _skeltalBehavior = GetComponent<SkeltalBehaviour>();
        _skeltalBehavior.OnSkeltalMovementStart += BeginSkeltalMovementAnimation;
        _skeltalBehavior.OnSkeltalMovementFinished += StopSkeltalMovementAnimation;
        _skeltalBehavior.OnSkeltalAttackStart += BeginSkeltalAttackAnimation;
        _skeltalBehavior.OnSkeltalAttackFinished += StopSkeltalAttackAnimation;
    }

    private void BeginSkeltalMovementAnimation()
    {
        _animator.SetBool("IsMoving", true);
    }

    private void StopSkeltalMovementAnimation()
    {
        _animator.SetBool("IsMoving", false);
    }

    private void BeginSkeltalAttackAnimation()
    {
        _animator.SetBool("IsAttacking", true);
    }

    private void StopSkeltalAttackAnimation()
    {
        _animator.SetBool("IsAttacking", false);
    }
}
