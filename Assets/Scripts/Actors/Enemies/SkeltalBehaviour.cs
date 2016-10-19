using UnityEngine;
using System.Collections;

public abstract class SkeltalBehaviour : MonoBehaviour
{
    [SerializeField]
    protected float _leftLimit;

    [SerializeField]
    protected float _rightLimit;

    protected const float ATTACK_TIME = 3;

    private GameObject _skeltalSword;

    protected System.Random rng = new System.Random();
    protected Vector2 _initialPosition;
    protected bool _isAttacking;
    protected float _attackTimeLeft;

    //In the upcoming development, it would be wise to implement this variable into a component.
    protected bool _isFacingRight;

    // Use this for initialization
    virtual protected void Start()
    {
        _initialPosition = new Vector2(transform.position.x, transform.position.y);
        _leftLimit = Mathf.Abs(_leftLimit);
        _rightLimit = Mathf.Abs(_rightLimit);       
        _skeltalSword = transform.FindChild("SkeltalSword").gameObject;
        _isFacingRight = false;
        _isAttacking = false;
        _attackTimeLeft = ATTACK_TIME;
        _skeltalSword.GetComponent<BoxCollider2D>().offset = new Vector2(_skeltalSword.GetComponent<BoxCollider2D>().offset.x * -1, _skeltalSword.GetComponent<BoxCollider2D>().offset.y);
        transform.position = new Vector2(rng.Next((int)(_leftLimit + _rightLimit)) + transform.position.x - _leftLimit, transform.position.y);
    }
    
    // Update is called once per frame
    private void Update()
    {
        if (!_isAttacking)
        {
            _isAttacking = UpdateSkeltal();
        }
        else
        {
            _attackTimeLeft -= Time.fixedDeltaTime;
            if (_attackTimeLeft < 2 && _attackTimeLeft > 1)
            {
                _skeltalSword.GetComponent<BoxCollider2D>().enabled = true;
            }
            else if (_attackTimeLeft <= 0)
            {
                _attackTimeLeft = ATTACK_TIME;
                _isAttacking = false;
                Flip();
            }
            else
            {
                _skeltalSword.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

    protected abstract bool UpdateSkeltal();

    //In upcoming development, it would be wise to implement this method into a Component.
    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
    }
}
