using System.Collections;
using UnityEngine;

public class PhoenixAI : MonoBehaviour
{
    private enum PhoenixStatus
    {
        FLY,
        FLEE,
        ATTACK,
        DEAD,
    }

    [SerializeField]
    private Vector2 _northEastLimit;

    [SerializeField]
    private Vector2 _southEastLimit;

    [SerializeField]
    private Vector2 _southWestLimit;

    [SerializeField]
    private Vector2 _northWestLimit;

    [SerializeField]
    private float _attackDelay = 5;

    [SerializeField]
    private float _playerApproachLimit = 4.7f;

    [SerializeField]
    private float _normalFlyingSpeed = 5;

    [SerializeField]
    private float _attackFlyingSpeed = 7;

    [SerializeField]
    private float _hitFlyingSpeed = 3;

    private const float FLAP_DELAY = 0.5f;
    private const float WING_FLAP = 2.75f;
    private const float RADIAN_TO_DEGREE = 57.2958f;

    private Vector2 _currentPoint;
    private Vector2 _closestPoint;
    private Vector2 _playerPosition;
    private Health _health;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private BossOrientation _bossOrientation;
    private PolygonCollider2D _polygonHitbox;
    private AnimationTags _animTags;

    private System.Random _random = new System.Random();
    private float _flightTimeLeft;
    private float _attackCooldownTimeLeft;
    private float _closestHorizontalPoint;
    private float _closestVerticalPoint;
    private float _flyingSpeed;

    private void Start()
    {
        _health = GetComponent<Health>();
        _health.OnDamageTaken += GotHitByPlayer;
        _health.OnDeath += OnPhoenixDefeated;

        _rigidbody = GetComponent<Rigidbody2D>();
        _bossOrientation = GetComponent<BossOrientation>();
        _animator = GetComponent<Animator>();
        _polygonHitbox = GetComponent<PolygonCollider2D>();
        _animTags = StaticObjects.GetAnimationTags();

        InitializePhoenix();
    }

    private void InitializePhoenix()
    {
        _flyingSpeed = _normalFlyingSpeed;
        _health.HealthPoint = _health.MaxHealth;
        _attackCooldownTimeLeft = 0;
        _currentPoint = (_southWestLimit + _northEastLimit) * 0.5f;
        SetFlyStatus();
    }

    private void Flap()
    {
        if (_flightTimeLeft > 0)
        {
            _flightTimeLeft -= Time.deltaTime;
        }
        else
        {
            _flightTimeLeft = FLAP_DELAY;
            _rigidbody.velocity = Vector2.up * WING_FLAP;
        }
    }

    private void GotHitByPlayer(int hitPoints)
    {
        _flyingSpeed = _hitFlyingSpeed;
        _polygonHitbox.enabled = false;
    }

    private bool CheckIfPlayerIsTooClose()
    {
        return Vector2.Distance(StaticObjects.GetPlayer().transform.position, transform.position) < _playerApproachLimit;
    }

    private void FindAdjacentPoint()
    {
        if (_currentPoint.Equals(_northEastLimit) || _currentPoint.Equals(_southWestLimit))
        {
            _closestPoint = (_random.Next(0, 10) < 5 ? _northWestLimit : _southEastLimit);
        }
        else
        {
            _closestPoint = (_random.Next(0, 10) < 5 ? _northEastLimit : _southWestLimit);
        }
    }

    private void FindFleeingPoint()
    {
        while (_closestPoint.Equals(_currentPoint))
        {
            int pointToFleeIndex = _random.Next() % 4;
            switch (pointToFleeIndex)
            {
                case 0:
                    _closestPoint = _northEastLimit;
                    break;
                case 1:
                    _closestPoint = _southEastLimit;
                    break;
                case 2:
                    _closestPoint = _southWestLimit;
                    break;
                case 3:
                    _closestPoint = _northWestLimit;
                    break;
            }
        }
    }

    private IEnumerator UpdateWhenFlying()
    {
        while (_attackCooldownTimeLeft < _attackDelay)
        {
            yield return null;

            _bossOrientation.FlipTowardsPlayer();
            _attackCooldownTimeLeft += Time.deltaTime;
            if (CheckIfPlayerIsTooClose())
            {
                FindAdjacentPoint();
                SetFleeStatus();
            }
            else
            {
                Flap();
            }
        }

        SetAttackStatus();
    }

    private IEnumerator UpdateWhenFleeing()
    {
        while (Vector2.Distance(_closestPoint, transform.position) > 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, _closestPoint, _flyingSpeed * Time.deltaTime);
            yield return null;
        }
        SetFlyStatus();
    }

    private IEnumerator UpdateWhenAttacking()
    {
        while (Vector2.Distance(transform.position, _playerPosition) > 1)
        {
            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), _playerPosition, _flyingSpeed * Time.deltaTime);
            yield return null;
        }
        _closestPoint = _currentPoint;
        FindFleeingPoint();
        SetFleeStatus();
    }

    private void SetFlyStatus()
    {
        StopAllCoroutines();

        _currentPoint = _closestPoint;
        _rigidbody.isKinematic = false;
        transform.rotation = Quaternion.identity;
        _flightTimeLeft = 0;

        StartCoroutine(UpdateWhenFlying());
    }

    private void SetFleeStatus()
    {
        StopAllCoroutines();

        _flyingSpeed = _normalFlyingSpeed;
        transform.rotation = Quaternion.identity;
        _rigidbody.isKinematic = true;
        _attackCooldownTimeLeft = 0;
        _bossOrientation.FlipTowardsSpecificPoint(_closestPoint);
        transform.Rotate(0, 0, RADIAN_TO_DEGREE * Mathf.Atan((_closestPoint.y - transform.position.y) / (_closestPoint.x - transform.position.x)));
        _polygonHitbox.enabled = false;
        StartCoroutine(UpdateWhenFleeing());
    }

    private void SetAttackStatus()
    {
        StopAllCoroutines();

        _playerPosition = StaticObjects.GetPlayer().transform.position;
        transform.Rotate(0, 0, RADIAN_TO_DEGREE * Mathf.Atan((_playerPosition.y - transform.position.y) / (_playerPosition.x - transform.position.x)));
        _attackCooldownTimeLeft = 0;
        _rigidbody.isKinematic = true;
        _flyingSpeed = _attackFlyingSpeed;
        _polygonHitbox.enabled = true;
        StartCoroutine(UpdateWhenAttacking());
    }

    private void OnPhoenixDefeated()
    {
        StopAllCoroutines();
        _animator.SetBool(_animTags.IsDead, true);
        _rigidbody.isKinematic = false;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
