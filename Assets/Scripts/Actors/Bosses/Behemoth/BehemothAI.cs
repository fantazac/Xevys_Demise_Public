using UnityEngine;
using System.Collections;

public class BehemothAI : MonoBehaviour
{
    [SerializeField]
    private GameObject _leftWall;

    [SerializeField]
    private GameObject _rightWall;

    [SerializeField]
    private float _speed = 25;

    [SerializeField]
    private float _struckTime = 1;

    [SerializeField]
    private float _stunTime = 3;

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
    private SpriteRenderer _spriteRenderer;
    private AnimationTags _animTags;

    private BehemothStatus _status;
    private System.Random _random;
    private float _waitingTimeLeft;
    private float _chargingTimeLeft;
    private bool _isCharging = false;

    private WaitForSeconds _delayCharging;
    private WaitForSeconds _delayStruck;
    private WaitForSeconds _delayStun;

    private void Start()
    {
        _random = new System.Random();

        StaticObjects.GetPlayer().GetComponent<KnockbackPlayer>().EnableBehemothKnockback(gameObject);

        _delayCharging = new WaitForSeconds(_timeBeforeWarning);
        _delayStruck = new WaitForSeconds(_struckTime);
        _delayStun = new WaitForSeconds(_stunTime);

        _health = GetComponent<Health>();
        _health.OnDeath += OnBehemothDefeated;

        _rigidbody = GetComponent<Rigidbody2D>();
        _bossOrientation = GetComponent<BossOrientation>();
        _animator = GetComponent<Animator>();
        _polygonHitbox = GetComponent<PolygonCollider2D>();
        _attack = GetComponent<OnBehemothAttackHit>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animTags = StaticObjects.GetAnimationTags();

        SetWaitStatus();
    }

    private IEnumerator UpdateWhenWaiting()
    {
        _rigidbody.velocity = Vector2.zero;

        while (_waitingTimeLeft >= _timeBeforeWarning)
        {
            yield return null;

            _bossOrientation.FlipTowardsPlayer();
            _waitingTimeLeft -= Time.deltaTime;
        }
        _attack.enabled = true;
        _animator.SetInteger(_animTags.State, 1);

        yield return _delayCharging;

        SetChargeStatus();
    }

    private IEnumerator UpdateWhenCharging()
    {
        _rigidbody.velocity = (Vector2.right * _speed * _bossOrientation.Orientation) + 
            (Vector2.up * _rigidbody.velocity.y);

        while (_chargingTimeLeft > 0)
        {
            if (CheckWallCollision())
            {
                SetStruckStatus();
            }
            _chargingTimeLeft -= Time.deltaTime;

            yield return null;
        }

        _attack.enabled = false;
        SetWaitStatus();
    }

    private IEnumerator UpdateWhenStruckWall()
    {
        _rigidbody.velocity = (Vector2.up * _rigidbody.velocity.y) + 
            (Vector2.left * _speed * _stunSpeedModifier * _bossOrientation.Orientation);

        yield return _delayStruck;

        SetStunnedStatus();
    }

    private IEnumerator UpdateWhenStunned()
    {
        _rigidbody.velocity = Vector2.zero;

        yield return _delayStun;

        SetWaitStatus();
    }

    private bool CheckWallCollision()
    {
        return HasTouchedWall() ^ _bossOrientation.IsFacingRight;
    }

    private bool HasTouchedWall()
    {
        return _aimedWall.transform.position.x - (_bossOrientation.Orientation * _aimedWall.transform.localScale.x * 0.5f) >=
            transform.position.x + (_bossOrientation.Orientation * _spriteRenderer.bounds.size.x * 0.5f);
    }

    private void SetWaitStatus()
    {
        _status = BehemothStatus.WAIT;
        _animator.SetInteger(_animTags.State, 0);

        _polygonHitbox.enabled = false;
        _waitingTimeLeft = _random.Next(_chargeTime, _chargeTime * 2);

        StartCoroutine(UpdateWhenWaiting());
    }

    public void SetChargeStatus()
    {
        if (_status == BehemothStatus.WAIT)
        {
            StopAllCoroutines();

            _status = BehemothStatus.CHARGE;
            _animator.SetInteger(_animTags.State, 2);

            _isCharging = (_random.Next(0, 10) < 5 ? true : false);
            _chargingTimeLeft = _feignTime + (_isCharging ? _chargeTime : 0);
            _aimedWall = (_bossOrientation.IsFacingRight ? _rightWall : _leftWall);

            StartCoroutine(UpdateWhenCharging());
        }
    }

    private void SetStruckStatus()
    {
        StopAllCoroutines();

        _status = BehemothStatus.STRUCK;
        _animator.SetInteger(_animTags.State, 3);

        _attack.enabled = false;
        _polygonHitbox.enabled = true;

        StartCoroutine(UpdateWhenStruckWall());
    }

    private void SetStunnedStatus()
    {
        _status = BehemothStatus.STUN;
        _animator.SetInteger(_animTags.State, 4);

        StartCoroutine(UpdateWhenStunned());
    }

    private void OnBehemothDefeated()
    {
        _animator.SetBool(_animTags.IsDead, true);
        Destroy(_rigidbody);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
