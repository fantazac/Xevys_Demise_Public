using UnityEngine;
using System.Collections;

public class MoveJumpySkeltal : SkeltalBehaviour
{
    [SerializeField]
    protected float _initialSpeed = 10;
    protected float _speed;

    void Start()
    {
        _initialSpeed = _speed;
    }
	protected override bool MoveSkeltal()
    {
        transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), new Vector2(transform.position.x, 9001), _speed * Time.deltaTime);
        transform.position = new Vector2(transform.position.x, Mathf.Max(_initialHeight, transform.position.y));
        if (_speed > 0.1f)
        {
            _speed *= 0.9f;
        }
        else if (_speed <= 0.1f && _speed > 0)
        {
            _speed *= -1;
        }
        else
        {
            _speed *= 1.1f;
        }
        if (transform.position.y <= _initialHeight)
        {
            _speed = _initialSpeed;
            return true;
        }
        return false;
    }
}
