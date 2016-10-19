using UnityEngine;
using System.Collections;
using System;

public class MoveWalkerSkeltal : SkeltalBehaviour
{
    protected const float WALKER_SPEED = 5;
    private Animator _animator;

    protected override void Start()
    {
        base.Start();
        _animator = GetComponent<Animator>();
    }

    protected override bool UpdateSkeltal()
    {
        transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y),
            new Vector2(_initialPosition.x + (_isFacingRight ? _rightLimit : -_leftLimit),
            transform.position.y), WALKER_SPEED * Time.deltaTime);

        if ((_isFacingRight && transform.position.x >= _initialPosition.x + _rightLimit) || (!_isFacingRight && transform.position.x <= _initialPosition.x - _leftLimit))
        {
            _animator.SetBool("IsMoving", false);
            return true;
        }

        _animator.SetBool("IsMoving", true);
        return false;
    }
}
