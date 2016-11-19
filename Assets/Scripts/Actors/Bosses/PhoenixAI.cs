using UnityEngine;
using System.Collections;

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

    private const float FLIGHT_DELAY = 0.5f;
    private const float ATTACK_DELAY = 5;
    private const float PLAYER_APPROACH_LIMIT = 6;
    private const float SPEED = 7;
    private const float WING_FLAP = 2.675f;
    private const float RADIAN_TO_DEGREE = 57.2958f;

    private Vector2 _currentPoint;
    private Vector2 _closestPoint;
    private Vector2 _playerPosition;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private BossOrientation _bossOrientation;
    private OnBossDefeated _onBossDefeated;

    private System.Random _rng = new System.Random();
    private PhoenixStatus _status;
    private float _flightTimeLeft;
    private float _attackCooldownTimeLeft;
    private float _closestHorizontalPoint;
    private float _closestVerticalPoint;

    // Use this for initialization
    private void Start()
    {
        _status = PhoenixStatus.FLY;
        _flightTimeLeft = 0;
        _attackCooldownTimeLeft = 0;
        _currentPoint = _southWestLimit;
        _rigidbody = GetComponent<Rigidbody2D>();
        _bossOrientation = GetComponent<BossOrientation>();
        _animator = GetComponent<Animator>();
        _onBossDefeated = GetComponent<OnBossDefeated>();
        _onBossDefeated.OnDefeated += OnPhoenixDefeated;
    }

    private void OnDestroy()
    {
        _onBossDefeated.OnDefeated -= OnPhoenixDefeated;
    }

    private void FixedUpdate()
    {
        //This status allows Phoenix to watch the player and either charge on him after a few seconds or flee.
        if (_status == PhoenixStatus.FLY)
        {
            UpdateWhenFlying();
        }
        //Flee status makes Phoenix go to a neighbouring point in order to avoid the player.
        else if (_status == PhoenixStatus.FLEE)
        {
            UpdateWhenFleeing();
        }
        //In this status, Phoenix dives on the player in a parabolic path, allowing the latter to strike its head.
        else if (_status == PhoenixStatus.ATTACK)
        {
            UpdateWhenAttacking();
        }
    }

    private void UpdateWhenFlying()
    {
        _bossOrientation.FlipTowardsPlayer();
        _attackCooldownTimeLeft += Time.fixedDeltaTime;
        if (_attackCooldownTimeLeft > ATTACK_DELAY)
        {
            _playerPosition = StaticObjects.GetPlayer().transform.position;//GameObject.Find("Character").transform.position;
            transform.Rotate(0, 0, RADIAN_TO_DEGREE * Mathf.Atan((_playerPosition.y - transform.position.y) / (_playerPosition.x - transform.position.x)));
            _attackCooldownTimeLeft = 0;
            _rigidbody.isKinematic = true;
            _status = PhoenixStatus.ATTACK;
        }
        else
        {
            float playerDistance = Vector2.Distance(StaticObjects.GetPlayer().transform.position, transform.position);
            if (playerDistance < PLAYER_APPROACH_LIMIT)
            {
                int randomNumber = _rng.Next();
                if (_currentPoint.Equals(_northEastLimit) || _currentPoint.Equals(_southWestLimit))
                {
                    _closestPoint = (randomNumber % 2 == 0 ? _northWestLimit : _southEastLimit);
                }
                else
                {
                    _closestPoint = (randomNumber % 2 == 0 ? _northEastLimit : _southWestLimit);
                }
                EngageInFleeStatus();
            }
            else
            {
                if (_flightTimeLeft > 0)
                {
                    _flightTimeLeft -= Time.fixedDeltaTime;
                }
                else
                {
                    _flightTimeLeft = FLIGHT_DELAY;
                    _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, WING_FLAP);
                }
            }
        }
    }

    private void UpdateWhenFleeing()
    {
        transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), _closestPoint, SPEED * Time.fixedDeltaTime);
        CheckForFlyStatus();
    }

    private void UpdateWhenAttacking()
    {
        transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), _playerPosition, SPEED * Time.fixedDeltaTime);

        if (Vector2.Distance(transform.position, _playerPosition) < 1)
        {
            _closestPoint = _currentPoint;
            while (_closestPoint.Equals(_currentPoint))
            {
                int pointToFleeIndex = _rng.Next() % 4;
                if (pointToFleeIndex == 0)
                {
                    _closestPoint = _northEastLimit;
                }
                else if (pointToFleeIndex == 1)
                {
                    _closestPoint = _southEastLimit;
                }
                else if (pointToFleeIndex == 2)
                {
                    _closestPoint = _southWestLimit;
                }
                else if (pointToFleeIndex == 3)
                {
                    _closestPoint = _northWestLimit;
                }
            }
            EngageInFleeStatus();
        }
    }

    private void CheckForFlyStatus()
    {
        float closestPointDistance = Vector2.Distance(_closestPoint, transform.position);
        if (closestPointDistance < 1)
        {
            _currentPoint = _closestPoint;
            _rigidbody.isKinematic = false;
            transform.rotation = Quaternion.identity;
            _flightTimeLeft = 0;
            _status = PhoenixStatus.FLY;
        }
    }

    private void EngageInFleeStatus()
    {
        transform.rotation = Quaternion.identity;
        _rigidbody.isKinematic = true;
        _attackCooldownTimeLeft = 0;
        _bossOrientation.FlipTowardsSpecificPoint(_closestPoint);
        transform.Rotate(0, 0, RADIAN_TO_DEGREE * Mathf.Atan((_closestPoint.y - transform.position.y) / (_closestPoint.x - transform.position.x)));
        _status = PhoenixStatus.FLEE;
    }

    public void OnPhoenixDefeated()
    {
        _status = PhoenixStatus.DEAD;
        _animator.SetBool("IsDead", true);
        _rigidbody.isKinematic = false;
    }
}
