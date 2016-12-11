using UnityEngine;
using System.Collections;

public class MovementAnimationManager : MonoBehaviour
{

    private Animator _anim;
    private PlayerMovement[] _playerMovements;
    private PlayerState _playerState;
    private AnimationTags _animationTags;

    private void Start()
    {
        _animationTags = StaticObjects.GetAnimationTags();

        _anim = GetComponent<Animator>();

        _playerState = StaticObjects.GetPlayerState();

        _playerState.OnChangedJumping += AnimateJumping;
        _playerState.OnChangedFalling += AnimateFalling;
        _playerState.OnChangedMoving += AnimateMoving;
        _playerState.OnChangedFloating += AnimateFloating;
        _playerState.OnChangedCroutching += AnimateCroutching;
        _playerState.OnChangedAttacking += AnimateAttacking;
        _playerState.OnChangedKnockback += AnimateKnockBack;

        _playerMovements = StaticObjects.GetPlayer().GetComponents<PlayerMovement>();
        foreach(PlayerMovement playerMovement in _playerMovements)
        {
            playerMovement.OnPlayerFlipped += FlipAnimation;
        }
    }

    private void AnimateJumping()
    {
        _anim.SetBool(_animationTags.IsJumping, _playerState.IsJumping);
    }

    private void AnimateFalling()
    {
        _anim.SetBool(_animationTags.IsFalling, _playerState.IsFalling);
    }

    private void AnimateMoving()
    {
        _anim.SetBool(_animationTags.IsMoving, _playerState.IsMoving);
    }

    private void AnimateFloating()
    {
        _anim.SetBool(_animationTags.IsFloating, _playerState.IsFloating);
    }

    private void AnimateCroutching()
    {
        _anim.SetBool(_animationTags.IsCrouching, _playerState.IsCroutching);
        if (_playerState.IsAttacking && _playerState.IsCroutching)
        {
            if (_anim.GetBool("HasSword"))
            {
                _anim.Play(_animationTags.Character_Crouch_Attack, 0, _anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
            }
            else
            {
                _anim.Play(_animationTags.Character_Crouch_Attack_NoSword, 0, _anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
            }
            
        }
        else if (_playerState.IsAttacking)
        {
            if (_anim.GetBool("HasSword"))
            {
                _anim.Play(_animationTags.Character_Attack, 0, _anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
            }
            else
            {
                _anim.Play(_animationTags.Character_Attack_NoSword, 0, _anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
            }                
        }
    }

    private void AnimateAttacking(float animSpeed)
    {
        _anim.speed = animSpeed;
        _anim.SetBool(_animationTags.IsAttacking, _playerState.IsAttacking);
    }

    private void FlipAnimation()
    {
        _anim.transform.localScale += Vector3.left * _anim.transform.localScale.x * 2;
    }

    private void AnimateKnockBack()
    {
        _anim.SetBool(_animationTags.IsDamaged, _playerState.IsKnockedBack);
    }

}
