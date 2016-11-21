using UnityEngine;
using System.Collections;

public class BehemothAI : MonoBehaviour
{
    private enum BehemothStatus
    {
        WAIT,
        CHARGE,
        STRUCK,
        STUN,
        DEAD,
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

    private OnAttackHit _attack;

    private Health _health;
    private GameObject _aimedWall;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private BossOrientation _bossOrientation;
    private PolygonCollider2D _polygonHitbox;

    private System.Random _rng = new System.Random();
    private BehemothStatus _status = BehemothStatus.WAIT;
    private float _timeLeft = CHARGE_TIME;
    private bool _isCharging;

    private void Start()
    {
        _health = GetComponent<Health>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _bossOrientation = GetComponent<BossOrientation>();
        _animator = GetComponent<Animator>();
        _polygonHitbox = GetComponent<PolygonCollider2D>();
 
        _attack = GetComponent<OnBehemothAttackHit>();

        _health.OnDeath += OnBehemothDefeated;
    }

    private void OnDestroy()
    {
        _health.OnDeath -= OnBehemothDefeated;
    }

    private void FixedUpdate()
    {
        //Wait allows Behemoth to face the player and prepare to charge.
        if (_status == BehemothStatus.WAIT)
        {
            UpdateWhenWaiting();
        }
        //Charge status makes Behemoth aims for the wall for the amount of time in seconds decided above.
        //Notice that Behemoth feigning really close to the wall makes him directly crash into it instead.
        else if (_status == BehemothStatus.CHARGE)
        {
            UpdateWhenCharging();
        }
        //Stuck is a status of one second during which Behemoth backs off and then goes into the Stun status.
        else if (_status == BehemothStatus.STRUCK)
        {
            UpdateWhenStruckWall();
        }
        //Stun status allows the player to attack Behemoth for the amount of seconds specified in STUN_TIME.
        else if (_status == BehemothStatus.STUN)
        {
            UpdateWhenStunned();
        }
    }

    private void UpdateWhenWaiting()
    {
        _bossOrientation.FlipTowardsPlayer();
        if (_timeLeft > 0)
        {
            _timeLeft -= Time.fixedDeltaTime;
            if (_timeLeft < 2)
            {
                _attack.enabled = true;
                _animator.SetInteger("State", 1);
            }
        }
        //Upon countdown expired, Behemoth either feigns charging or charges.
        //The time charging depends from seconds specified in FEIGN_TIME or this amount of time plus the amount specified in CHARGE_TIME;
        else
        {
            SetChargeStatus();
        }
    }

    public void SetChargeStatus()
    {
        if(_status == BehemothStatus.WAIT)
        {
            _animator.SetInteger("State", 2);
            _isCharging = (_rng.Next() % 2 == 0 ? true : false);
            _timeLeft = FEIGN_TIME + (_isCharging ? CHARGE_TIME : 0);
            _status = BehemothStatus.CHARGE;
            _aimedWall = (_bossOrientation.IsFacingRight ? _rightWall : _leftWall);
        }
    }

    private void UpdateWhenCharging()
    {
        _timeLeft -= Time.fixedDeltaTime;
        if (_timeLeft > 0)
        {
            _rigidbody.velocity = new Vector2(_speed * _bossOrientation.Orientation, _rigidbody.velocity.y);
            if (_bossOrientation.IsFacingRight ?
                _aimedWall.transform.position.x - _aimedWall.transform.localScale.x / 2 <= transform.position.x + GetComponent<SpriteRenderer>().bounds.size.x / 2:
                _aimedWall.transform.position.x + _aimedWall.transform.localScale.x / 2 >= transform.position.x - GetComponent<SpriteRenderer>().bounds.size.x / 2)

            {
                _timeLeft = 1;
                _polygonHitbox.enabled = true;
                _animator.SetInteger("State", 3);
                _attack.enabled = false;
                _status = BehemothStatus.STRUCK;
                
            }
        }
        else
        {
            _attack.enabled = false;
            SetWaitStatus();
        }
    }

    private void UpdateWhenStruckWall()
    {
        if (_timeLeft > 0)
        {
            _timeLeft -= Time.fixedDeltaTime;
            _rigidbody.velocity = new Vector2(-_speed / 10 * _bossOrientation.Orientation, _rigidbody.velocity.y);
        }
        else
        {
            _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
            _timeLeft = STUN_TIME;
            _animator.SetInteger("State", 4);
            _status = BehemothStatus.STUN;
        }
    }

    private void UpdateWhenStunned()
    {
        _timeLeft -= Time.deltaTime;
        if (_timeLeft <= 0)
        {
            _polygonHitbox.enabled = false;
            SetWaitStatus();
        }
    }

    private void SetWaitStatus()
    {
        _animator.SetInteger("State", 0);
        _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
        _status = BehemothStatus.WAIT;
        _timeLeft = _rng.Next(5, 10);
    }

    public void OnBehemothDefeated()
    {
        _status = BehemothStatus.DEAD;
        _animator.SetBool("IsDead", true);
        Destroy(_rigidbody);
    }
}
