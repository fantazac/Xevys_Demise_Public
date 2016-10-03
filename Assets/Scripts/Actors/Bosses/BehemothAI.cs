using UnityEngine;
using System.Collections;

public class BehemothAI : MonoBehaviour
{
    private enum Status
    {
        wait,
        charge,
        struck,
        stun,
    }

    private Rigidbody2D _rigidbody;
    private GameObject _aimedWall;
    private Animator _animator;

    [SerializeField]
    private float SPEED = 25;
    private System.Random _rng = new System.Random();
    private Status _status = Status.wait;
    private float _timeLeft = 5;
    private bool _isCharging;
    //In upcoming development, it would be wise to implement this variable and property into a Component.
    private bool _isFacingLeft = false;
    private int Orientation
    {
        get
        {
            return (_isFacingLeft ? 1 : -1);
        }
    }

    // Use this for initialization
    void Start ()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (_status == Status.wait)
        {
            if (GameObject.Find("Character").transform.position.x > transform.position.x)
            {
                if (_isFacingLeft)
                {
                    Flip();
                }
            }
            else if (GameObject.Find("Character").transform.position.x < transform.position.x)
            {
                if(!_isFacingLeft)
                {
                    Flip();
                }

            }

            if (_timeLeft > 0)
            {
                _timeLeft -= Time.fixedDeltaTime;
                if (_timeLeft < 2)
                {
                    GetComponent<SpriteRenderer>().color = Color.red;
                    _animator.SetInteger("State", 1);
                }
            }
            else
            {
                _isCharging = (_rng.Next() % 2 == 0 ? true : false);
                _timeLeft = 0.33f + (_isCharging ? 5 : 0);
                _status = Status.charge;
                _aimedWall = (_isFacingLeft? GameObject.Find("Left Wall"): GameObject.Find("Right Wall"));
            }
        }
        else if (_status == Status.charge)
        {
            _timeLeft -= Time.fixedDeltaTime;
            if (_timeLeft > 0)
            {
                _rigidbody.velocity = new Vector2(-SPEED * Orientation, _rigidbody.velocity.y);
                if (_isFacingLeft ? _aimedWall.transform.position.x + _aimedWall.GetComponent<SpriteRenderer>().bounds.size.x/2 >= transform.position.x - GetComponent<SpriteRenderer>().bounds.size.x/2 : _aimedWall.transform.position.x - _aimedWall.GetComponent<SpriteRenderer>().bounds.size.x / 2 <= transform.position.x + GetComponent<SpriteRenderer>().bounds.size.x/2)
                {
                    _timeLeft = 1;
                    _status = Status.struck;
                }
            }
            else
            {
                SetWaitStatus();
            }
        }
        else if (_status == Status.struck)
        {
            if (_timeLeft > 0)
            {
                _timeLeft -= Time.fixedDeltaTime;
                GetComponent<SpriteRenderer>().color = Color.yellow;
                _rigidbody.velocity = new Vector2(SPEED / 10 * Orientation, _rigidbody.velocity.y);
                _animator.SetInteger("State", 2);
            }
            else
            {
                _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
                _timeLeft = 3;
                _status = Status.stun;
            }
        }
        else if (_status == Status.stun)
        {
            GetComponent<SpriteRenderer>().color = Color.blue;
            _timeLeft -= Time.fixedDeltaTime;
            _animator.SetInteger("State", 3);
            if (_timeLeft < 0)
            {
                SetWaitStatus();
            }
        }
        
    }

    private void SetWaitStatus()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        _animator.SetInteger("State", 0);
        _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
        _status = Status.wait;
        _timeLeft = _rng.Next(5,10);
    }

    //In upcoming development, it would be wise to implement this method into a Component.
    private void Flip()
    {
        _isFacingLeft = !_isFacingLeft;
        transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
    }
}
