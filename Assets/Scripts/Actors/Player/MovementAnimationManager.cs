using UnityEngine;
using System.Collections;

public class MovementAnimationManager : MonoBehaviour
{

    private Animator _anim;
    private PlayerMovement[] _playerMovements;
    private PlayerState _playerState;

    private void Start()
    {
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
        _anim.SetBool("IsJumping", _playerState.IsJumping);
    }

    private void AnimateFalling()
    {
        _anim.SetBool("IsFalling", _playerState.IsFalling);
    }

    private void AnimateMoving()
    {
        _anim.SetBool("IsMoving", _playerState.IsMoving);
    }

    private void AnimateFloating()
    {
        _anim.SetBool("IsFloating", _playerState.IsFloating);
    }

    private void AnimateCroutching()
    {
        _anim.SetBool("IsCrouching", _playerState.IsCroutching);
        if (_playerState.IsAttacking && _playerState.IsCroutching)
        {
            _anim.Play("Character_Crouch_Attack", 0, _anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        }
        else if (_playerState.IsAttacking)
        {
            _anim.Play("Character_Attack", 0, _anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        }
    }

    private void AnimateAttacking(float animSpeed)
    {
        _anim.speed = animSpeed;
        _anim.SetBool("IsAttacking", _playerState.IsAttacking);
    }

    private void FlipAnimation()
    {
        _anim.transform.localScale = new Vector3(-1 * _anim.transform.localScale.x,
                _anim.transform.localScale.y, _anim.transform.localScale.z);
    }

    private void AnimateKnockBack()
    {
        _anim.SetBool("IsDamaged", _playerState.IsKnockedBack);
    }

}
