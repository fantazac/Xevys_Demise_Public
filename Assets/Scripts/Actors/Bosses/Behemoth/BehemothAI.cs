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

    [SerializeField]
    private int _stunTime = 3;

    [SerializeField]
    private int _chargeTime = 5;

    [SerializeField]
    private float _feignTime = 0.33f;

    [SerializeField]
    private float _stunSpeedModifier = 0.2f;

    [SerializeField]
    private float _timeBeforeWarning = 2;

    private OnAttackHit _attack;

    private Health _health;
    private GameObject _aimedWall;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private BossOrientation _bossOrientation;
    private PolygonCollider2D _polygonHitbox;
    private Vector3 _initialPosition;

    private System.Random _random = new System.Random();
    private BehemothStatus _status = BehemothStatus.WAIT;
    private float _timeLeft;
    private bool _isCharging;
    private bool _canUseOnEnable = false;

    private void Start()
    {
        _health = GetComponent<Health>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _bossOrientation = GetComponent<BossOrientation>();
        _animator = GetComponent<Animator>();
        _polygonHitbox = GetComponent<PolygonCollider2D>();
        _attack = GetComponent<OnBehemothAttackHit>();
        _health.OnDeath += OnBehemothDefeated;
        SetupBehemothReset();
        _canUseOnEnable = true;
    }

    private void SetupBehemothReset()
    {
        _initialPosition = transform.position;
        _timeLeft = _chargeTime;
    }

    private void OnEnable()
    {
        if (_canUseOnEnable)
        {
            SetWaitStatus();
            _health.HealthPoint = _health.MaxHealth;
            transform.position = _initialPosition;
        }
    }

    private void FixedUpdate()
    {
        //Permet à Behemoth de se préparer à charger tout en faisant face au joueur.
        if (_status == BehemothStatus.WAIT)
        {
            UpdateWhenWaiting();
        }
        //Behemoth fonce tout droit dans un mur pour un certains temps donné.
        //Une feinte près du mur fera une collision quand même.
        else if (_status == BehemothStatus.CHARGE)
        {
            UpdateWhenCharging();
        }
        //Recul de Behemoth pendant une seconde avant d'être assomé.
        else if (_status == BehemothStatus.STRUCK)
        {
            UpdateWhenStruckWall();
        }
        //Permet à Bimon de frapper Behemoth pendant le temps spécifié dans STUN_TIME.
        else if (_status == BehemothStatus.STUN)
        {
            UpdateWhenStunned();
        }
    }

    private bool CheckWallCollision()
    {
        return _aimedWall.transform.position.x - (_bossOrientation.Orientation * _aimedWall.transform.localScale.x / 2) >=
            transform.position.x + (_bossOrientation.Orientation * GetComponent<SpriteRenderer>().bounds.size.x / 2) ^
            _bossOrientation.IsFacingRight;
    }

    private void UpdateWhenWaiting()
    {
        _bossOrientation.FlipTowardsPlayer();
        if (_timeLeft > 0)
        {
            _timeLeft -= Time.fixedDeltaTime;
            if (_timeLeft < _timeBeforeWarning)
            {
                _attack.enabled = true;
                _animator.SetInteger("State", 1);
            }
        }
        else
        {
            SetChargeStatus();
        }
    }

    private void UpdateWhenCharging()
    {
        _timeLeft -= Time.fixedDeltaTime;
        if (_timeLeft > 0)
        {
            _rigidbody.velocity = new Vector2(_speed * _bossOrientation.Orientation, _rigidbody.velocity.y);
            if (CheckWallCollision())
            {
                SetStruckStatus();
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
            _rigidbody.velocity = new Vector2(-_speed * _stunSpeedModifier * _bossOrientation.Orientation, _rigidbody.velocity.y);
        }
        else
        {
            SetStunnedStatus();
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

    public void SetChargeStatus()
    {
        if (_status == BehemothStatus.WAIT)
        {
            _animator.SetInteger("State", 2);
            _isCharging = (_random.Next() % 2 == 0 ? true : false);
            _timeLeft = _feignTime + (_isCharging ? _chargeTime : 0);
            _status = BehemothStatus.CHARGE;
            _aimedWall = (_bossOrientation.IsFacingRight ? _rightWall : _leftWall);
        }
    }

    private void SetWaitStatus()
    {
        _animator.SetInteger("State", 0);
        _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
        _status = BehemothStatus.WAIT;
        _timeLeft = _random.Next(5, 10);
    }

    private void SetStruckStatus()
    {
        _timeLeft = 1;
        _polygonHitbox.enabled = true;
        _animator.SetInteger("State", 3);
        _attack.enabled = false;
        _status = BehemothStatus.STRUCK;
    }

    private void SetStunnedStatus()
    {
        _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
        _timeLeft = _stunTime;
        _animator.SetInteger("State", 4);
        _status = BehemothStatus.STUN;
    }

    public void OnBehemothDefeated()
    {
        _status = BehemothStatus.DEAD;
        _animator.SetBool("IsDead", true);
        Destroy(_rigidbody);
    }
}
