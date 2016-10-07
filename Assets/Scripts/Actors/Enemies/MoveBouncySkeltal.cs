using UnityEngine;
using System.Collections;

public class MoveBouncySkeltal : SkeltalBehaviour
{
    private float _newHeight;
    private const float BOUNCY_SPEED = 0.1f;

    [SerializeField]
    private float _bounceHeight;

    protected override void Start()
    {
        base.Start();
        if (_bounceHeight == 0)
        {
            _bounceHeight = 1;
        }
    }

    protected override bool UpdateSkeltal()
    {
        _newHeight = -((transform.position.x - (_initialPosition.x - _leftLimit)) * (transform.position.x - (_initialPosition.x + _rightLimit)))/_bounceHeight + _initialPosition.y;
        transform.position = new Vector2(transform.position.x + (_isFacingRight ? BOUNCY_SPEED : -BOUNCY_SPEED), _newHeight);

        if ((_isFacingRight && transform.position.x >= _initialPosition.x + _rightLimit) || (!_isFacingRight && transform.position.x <= _initialPosition.x - _leftLimit))
        {
            return true;
        }

        return false;
    }
}
