using UnityEngine;
using System.Collections;

public class XevyHubAI : MonoBehaviour
{
    private enum XevyHubStatus
    {
        DEFENSIVE,
        VULNERABLE,
        ATTACKING,
    }

    [SerializeField]
    private float _leftDistance;
    [SerializeField]
    private float _rightDistance;

    [SerializeField]
    private float _attackingTimerCooldown = 1;

    [SerializeField]
    private float _defensiveTimerCooldown = 1;

    [SerializeField]
    private float _vulnerableTimerCooldown = 1;

    [SerializeField]
    private float _speed = 2.5f;

    private Health _health;
    private BossDirection _actorDirection;
    private BossOrientation _bossOrientation;
    private GameObject _xevySword;
    private PolygonCollider2D _collisionBox;

    private XevyHubStatus _status = XevyHubStatus.DEFENSIVE;
    private bool _isNotMoving = false;
    private float _timer;
    private float _leftLimit;
    private float _rightLimit;

    private void Start()
    {
        _leftDistance = Mathf.Abs(_leftDistance);
        _rightDistance = Mathf.Abs(_rightDistance);
        _leftLimit = transform.position.x - _leftDistance;
        _rightLimit = transform.position.x + _rightDistance;
        _timer = _defensiveTimerCooldown;
        _health = GetComponent<Health>();
        _actorDirection = GetComponent<BossDirection>();
        _bossOrientation = GetComponent<BossOrientation>();
        _xevySword = transform.FindChild("Xevy Sword").gameObject;
        _collisionBox = GetComponent<PolygonCollider2D>();
        _bossOrientation.OnBossFlipped += OnBossFlipped;
    }

    private void FixedUpdate()
    {
        _bossOrientation.FlipTowardsPlayer();
        if (!_isNotMoving)
        {
            if (CheckIfMovementCompleted())
            {
                _isNotMoving = true;
                _actorDirection.FlipDirection();
            }
            else
            {
                transform.position = new Vector3(transform.position.x + 
                    _bossOrientation.Orientation * _actorDirection.Direction * _speed * Time.deltaTime, transform.position.y);
            }
        }
        else
        {
            UpdateWhenNotMoving();
        }
    }

    private bool CheckIfMovementCompleted()
    {
        return (transform.position.x > (_bossOrientation.IsFacingRight ^ _actorDirection.IsGoingForward ? _leftLimit : _rightLimit) ^
             (_actorDirection.IsGoingForward ^ _bossOrientation.IsFacingRight));
    }

    private void OnBossFlipped()
    {
        _actorDirection.FlipDirection();
    }


    private void UpdateWhenNotMoving()
    {
        _timer -= Time.fixedDeltaTime;
        if (_timer <= 0)
        {
            if (_status == XevyHubStatus.DEFENSIVE)
            {
                _timer = _vulnerableTimerCooldown;
                _collisionBox.enabled = true;
                _status = XevyHubStatus.VULNERABLE;
            }
            else if (_status == XevyHubStatus.VULNERABLE)
            {
                _timer = _attackingTimerCooldown;
                _collisionBox.enabled = false;
                _xevySword.SetActive(true);
                _status = XevyHubStatus.ATTACKING;
            }
            else if (_status == XevyHubStatus.ATTACKING)
            {
                _timer = _defensiveTimerCooldown;
                _xevySword.SetActive(false);
                _collisionBox.enabled = false;
                _status = XevyHubStatus.DEFENSIVE;
                _isNotMoving = false;
            }
        }
    }
}
