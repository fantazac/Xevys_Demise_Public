using UnityEngine;
using System.Collections;
using System;

public class MoveWalkerSkeltal : SkeltalBehaviour
{
    protected const float WALKER_SPEED = 0.1f;
    private Animator _animator;

    protected override void Start()
    {
        base.Start();
        _animator = GetComponent<Animator>();
    }

    protected override bool UpdateSkeltal()
    {
        transform.position = new Vector2(transform.position.x + (_isFacingRight ? WALKER_SPEED : -WALKER_SPEED), transform.position.y);

        if ((_isFacingRight && transform.position.x >= _initialPosition.x + _rightLimit) || (!_isFacingRight && transform.position.x <= _initialPosition.x - _leftLimit))
        {
            _animator.SetBool("IsMoving", false);
            return true;
        }

        _animator.SetBool("IsMoving", true);
        return false;
    }
}
