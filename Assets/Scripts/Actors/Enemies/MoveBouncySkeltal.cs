using UnityEngine;
using System.Collections;

public class MoveBouncySkeltal : SkeltalBehaviour
{
    float _newHeight;

    protected override bool UpdateSkeltal()
    {
        _newHeight = -((transform.position.x - _leftLimit) * (transform.position.x - _rightLimit)) + _initialPosition.y; //Check this line
        transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y),
            new Vector2(_initialPosition.x + (_isFacingRight ? _rightLimit : -_leftLimit),
            _newHeight), SPEED * Time.deltaTime);

        if ((_isFacingRight && transform.position.x == _initialPosition.x + _rightLimit) || (!_isFacingRight && transform.position.x == _initialPosition.x - _leftLimit))
        {
            return true;
        }

        return false;
    }
}
