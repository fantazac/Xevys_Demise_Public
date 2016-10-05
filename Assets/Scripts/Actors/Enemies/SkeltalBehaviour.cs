using UnityEngine;
using System.Collections;

public abstract class SkeltalBehaviour : MonoBehaviour
{
    [SerializeField]
    protected float _leftLimit;

    [SerializeField]
    protected float _rightLimit;

    protected const float ATTACK_TIME = 3;
    protected const float SPEED = 5;

    protected float _initialHeight;
    protected bool _isAttacking;
    protected float _attackTimeLeft;

    //In the upcoming development, it would be wise to implement this variable into a component.
    protected bool _isFacingRight;

    // Use this for initialization
    void Start()
    {
        _initialHeight = transform.position.y;
        _isFacingRight = false;
        _isAttacking = false;
        _attackTimeLeft = ATTACK_TIME;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isAttacking)
        {
            _isAttacking = MoveSkeltal();
        }
        else
        {
            _attackTimeLeft -= Time.fixedDeltaTime;
            if (_attackTimeLeft < 2 && _attackTimeLeft > 1)
            {
                //Attack
            }
            else if (_attackTimeLeft <= 0)
            {
                _attackTimeLeft = ATTACK_TIME;
                _isAttacking = false;
                Flip();
            }
        }
    }

    protected abstract bool MoveSkeltal();

    //In upcoming development, it would be wise to implement this method into a Component.
    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
    }
}
