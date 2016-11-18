using UnityEngine;
using System.Collections;

public class MovementAnimationManager : MonoBehaviour
{

    private Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();

        PlayerState.OnChangedJumping += AnimateJumping;
        PlayerState.OnChangedFalling += AnimateFalling;
        PlayerState.OnChangedMoving += AnimateMoving;
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

}
