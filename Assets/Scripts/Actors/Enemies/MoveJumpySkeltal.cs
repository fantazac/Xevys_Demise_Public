using UnityEngine;
using System.Collections;

public class MoveJumpySkeltal : SkeltalBehaviour
{
    [SerializeField]
    protected float _jumpForce = 10;

    protected float _currentJumpForce;

    protected override void Start()
    {
        base.Start();
        _currentJumpForce = _jumpForce;
    }

    protected override bool UpdateSkeltal()
    {
        transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), new Vector2(transform.position.x, 9001), _currentJumpForce * Time.deltaTime);
        transform.position = new Vector2(transform.position.x, Mathf.Max(_initialHeight, transform.position.y));

        if (_currentJumpForce > 0.1f)
        {
            _currentJumpForce *= 0.9f;
        }
        else if (_currentJumpForce <= 0.1f && _currentJumpForce > 0)
        {
            _currentJumpForce *= -1;
        }
        else
        {
            _currentJumpForce *= 1.1f;
        }

        if (transform.position.y <= _initialHeight)
        {
            _currentJumpForce = _jumpForce;
            return true;
        }

        return false;
    }
}
