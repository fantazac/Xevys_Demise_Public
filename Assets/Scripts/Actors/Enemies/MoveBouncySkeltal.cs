using UnityEngine;
using System.Collections;

public class MoveBouncySkeltal : SkeltalBehaviour
{
    [SerializeField]
    private float _rightHeightLimit;
    [SerializeField]
    private float _leftHeightLimit;

    private float _newHeight;
    private const float BOUNCY_SPEED = 0.1f;
    private float _bounceApex;

    [SerializeField]
    private float _bounceHeight;

    protected override void Start()
    {
        base.Start();
        if (_bounceHeight == 0)
        {
            _bounceHeight = 1;
        }
        _bounceHeight = Mathf.Abs(_bounceHeight);
        _bounceApex = (_initialPosition.x - _leftLimit + _initialPosition.x + _rightLimit) / 2;
    }

    protected override bool UpdateSkeltal()
    {
        _newHeight = -((transform.position.x + (_isFacingRight ? BOUNCY_SPEED : -BOUNCY_SPEED) - (_initialPosition.x - _leftLimit)) 
                    * (transform.position.x + (_isFacingRight ? BOUNCY_SPEED : -BOUNCY_SPEED) - (_initialPosition.x + _rightLimit)))/_bounceHeight + _initialPosition.y;
        transform.position = new Vector2(transform.position.x + (_isFacingRight ? BOUNCY_SPEED : -BOUNCY_SPEED), _newHeight);

        if (_isFacingRight && transform.position.x > _bounceApex && transform.position.y <= _initialPosition.y + _rightHeightLimit)
        {
            return true;
        }
        if (!_isFacingRight && transform.position.x < _bounceApex && transform.position.y <= _initialPosition.y + _leftHeightLimit)
        {
            return true;
        }
        if ((_isFacingRight && transform.position.x >= _initialPosition.x + _rightLimit) || (!_isFacingRight && transform.position.x <= _initialPosition.x - _leftLimit))
        {
            return true;
        }

        return false;
    }
}
