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
        _animator.SetBool(StaticObjects.GetAnimationTags().IsMoving, true);
    }

    private void StopSkeltalMovementAnimation()
    {
        _animator.SetBool(StaticObjects.GetAnimationTags().IsMoving, false);
    }

    private void BeginSkeltalAttackAnimation()
    {
        _animator.SetBool(StaticObjects.GetAnimationTags().IsAttacking, true);
    }

    private void StopSkeltalAttackAnimation()
    {
        _animator.SetBool(StaticObjects.GetAnimationTags().IsAttacking, false);
    }
}
