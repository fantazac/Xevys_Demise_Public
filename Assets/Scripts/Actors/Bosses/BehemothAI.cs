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
        dead,
    }

    [SerializeField]
    private GameObject _leftWall;

    [SerializeField]
    private GameObject _rightWall;

    [SerializeField]
    private float _speed = 25;

    private const int STUN_TIME = 3;
    private const float FEIGN_TIME = 0.33f;
    private const int CHARGE_TIME = 5;

    private Rigidbody2D _rigidbody;
    private GameObject _aimedWall;
    private Animator _animator;
    //These components should eventually be placed into a script for all bosses (think heritage) as they are only used for a death status.
    private Health _health;
    private BoxCollider2D _boxCollider;
    private SpriteRenderer _spriteRenderer;

    private System.Random _rng = new System.Random();
    private Status _status = Status.wait;
    private float _timeLeft = CHARGE_TIME;
    private bool _isCharging;
    //Death status also include this boolean.
    private bool _isDead;

    //In upcoming development, it would be wise to implement this variable and property into a Component.
    private bool _isFacingLeft;
    private int Orientation { get { return (_isFacingLeft ? 1 : -1); } }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (!_isDead && _health.HealthPoint <= 0)
        {
            _isDead = true;
            _animator.SetBool("IsDead", true);
            Destroy(_rigidbody);
            _boxCollider.enabled = false;
        }
        else
        {
            //Wait allows Behemoth to face the player and prepare to charge.
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
                    if (!_isFacingLeft)
                    {
                        Flip();
                    }
                }
                if (_timeLeft > 0)
                {
                    _timeLeft -= Time.fixedDeltaTime;
                    if (_timeLeft < 2)
                    {
                        _animator.SetInteger("State", 1);
                    }
                }
                //Upon countdown expired, Behemoth either feigns charging or charges.
                //The time charging depends from seconds specified in FEIGN_TIME or this amount of time plus the amount specified in CHARGE_TIME;
                else
                {
                    _animator.SetInteger("State", 2);
                    _isCharging = (_rng.Next() % 2 == 0 ? true : false);
                    _timeLeft = FEIGN_TIME + (_isCharging ? CHARGE_TIME : 0);
                    _status = Status.charge;
                    _aimedWall = (_isFacingLeft ? _leftWall : _rightWall);
                }
            }
            //Charge status makes Behemoth aims for the wall for the amount of time in seconds decided above.
            //Notice that Behemoth feigning really close to the wall makes him directly crash into it instead.
            else if (_status == Status.charge)
            {
                _timeLeft -= Time.fixedDeltaTime;
                if (_timeLeft > 0)
                {
                    _rigidbody.velocity = new Vector2(-_speed * Orientation, _rigidbody.velocity.y);
                    if (_isFacingLeft ?
                        _aimedWall.transform.position.x + _aimedWall.GetComponent<SpriteRenderer>().bounds.size.x / 2 >= transform.position.x - GetComponent<SpriteRenderer>().bounds.size.x / 2 :
                        _aimedWall.transform.position.x - _aimedWall.GetComponent<SpriteRenderer>().bounds.size.x / 2 <= transform.position.x + GetComponent<SpriteRenderer>().bounds.size.x / 2)
                    {
                        _timeLeft = 1;
                        _animator.SetInteger("State", 3);
                        _status = Status.struck;
                    }
                }
                else
                {
                    SetWaitStatus();
                }
            }
            //Stuck is a status of one second during which Behemoth backs off and then goes into the Stun status.
            else if (_status == Status.struck)
            {
                if (_timeLeft > 0)
                {
                    _timeLeft -= Time.fixedDeltaTime;
                    _rigidbody.velocity = new Vector2(_speed / 10 * Orientation, _rigidbody.velocity.y);
                }
                else
                {
                    _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
                    _timeLeft = STUN_TIME;
                    _animator.SetInteger("State", 4);
                    _status = Status.stun;
                }
            }
            //Stun status allows the player to attack Behemoth for the amount of seconds specified in STUN_TIME.
            else if (_status == Status.stun)
            {
                _timeLeft -= Time.fixedDeltaTime;
                if (_timeLeft <= 0)
                {
                    SetWaitStatus();
                }
            }
        }
    }

    private void SetWaitStatus()
    {
        _animator.SetInteger("State", 0);
        _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
        _status = Status.wait;
        _timeLeft = _rng.Next(5, 10);
    }

    //In upcoming development, it would be wise to implement this method into a Component.
    private void Flip()
    {
        _isFacingLeft = !_isFacingLeft;
        transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
    }
}
