using UnityEngine;
using System.Collections;
using System;

public class MoveWalkerSkeltal : SkeltalBehaviour
{
    protected override bool MoveSkeltal()
    {
        transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y),
            new Vector2((_isFacingRight ? _rightLimit : _leftLimit),
            transform.position.y), SPEED * Time.deltaTime);

        if (_isFacingRight && transform.position.x == _rightLimit || !_isFacingRight && transform.position.x == _leftLimit)
        {
            return true;
        }

        return false;
    }
}
