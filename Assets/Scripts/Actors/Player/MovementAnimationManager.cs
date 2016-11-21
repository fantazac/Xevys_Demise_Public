using UnityEngine;
using System.Collections;

public class MovementAnimationManager : MonoBehaviour
{

    private Animator _anim;
    private PlayerMovement[] _playerMovements;

    private void Start()
    {
        _anim = GetComponent<Animator>();

        PlayerState.OnChangedJumping += AnimateJumping;
        PlayerState.OnChangedFalling += AnimateFalling;
        PlayerState.OnChangedMoving += AnimateMoving;
        PlayerState.OnChangedFloating += AnimateFloating;
        PlayerState.OnChangedCroutching += AnimateCroutching;
        PlayerState.OnChangedAttacking += AnimateAttacking;
        PlayerState.OnChangedKnockback += AnimateKnockBack;

        _playerMovements = StaticObjects.GetPlayer().GetComponents<PlayerMovement>();
        foreach(PlayerMovement playerMovement in _playerMovements)
        {
            playerMovement.OnPlayerFlipped += FlipAnimation;
        }
    }

    private void AnimateJumping()
    {
        _anim.SetBool("IsJumping", PlayerState.IsJumping);
    }

    private void AnimateFalling()
    {
        _anim.SetBool("IsFalling", PlayerState.IsFalling);
    }

    private void AnimateMoving()
    {
        _anim.SetBool("IsMoving", PlayerState.IsMoving);
    }

    private void AnimateFloating()
    {
        _anim.SetBool("IsFloating", PlayerState.IsFloating);
    }

    private void AnimateCroutching()
    {
        _anim.SetBool("IsCrouching", PlayerState.IsCroutching);
        if (PlayerState.IsAttacking && PlayerState.IsCroutching)
        {
            _anim.Play("Character_Crouch_Attack", 0, _anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        }
        else if (PlayerState.IsAttacking)
        {
            _anim.Play("Character_Attack", 0, _anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        }
    }

    private void AnimateAttacking(float animSpeed)
    {
        _anim.speed = animSpeed;
        _anim.SetBool("IsAttacking", PlayerState.IsAttacking);
    }

    private void FlipAnimation()
    {
        _anim.transform.localScale = new Vector3(-1 * _anim.transform.localScale.x,
                _anim.transform.localScale.y, _anim.transform.localScale.z);
    }

    private void AnimateKnockBack()
    {
        _anim.SetBool("IsDamaged", PlayerState.IsKnockedBack);
    }

}
