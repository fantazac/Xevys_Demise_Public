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

    private BossDirection _actorDirection;
    private BossOrientation _bossOrientation;
    private GameObject _xevySpell;
    private Animator _animator;

    private BoxCollider2D _swordHitbox;
    private PolygonCollider2D _collisionBox;

    private XevyHubStatus _status = XevyHubStatus.DEFENSIVE;
    private bool _isNotMoving = false;
    private bool _hasAttacked = false;
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
        _actorDirection = GetComponent<BossDirection>();
        _bossOrientation = GetComponent<BossOrientation>();
        _swordHitbox = GetComponentsInChildren<BoxCollider2D>()[1];
        _collisionBox = GetComponent<PolygonCollider2D>();
        _animator = GetComponent<Animator>();
        _animator.SetInteger("State", 1);
        _bossOrientation.OnBossFlipped += OnBossFlipped;
        _xevySpell = transform.FindChild("Xevy Spell").gameObject;
    }

    private void FixedUpdate()
    {
        _bossOrientation.FlipTowardsPlayer();
        if (!_isNotMoving)
        {
            if (CheckIfMovementCompleted())
            {
                _hasAttacked = false;
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
            _animator.SetInteger("State", 0);
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
                _animator.SetInteger("State", (_hasAttacked ? 1:2));
                _collisionBox.enabled = !_hasAttacked;
                _xevySpell.SetActive(_hasAttacked);
                _status = (_hasAttacked ? XevyHubStatus.DEFENSIVE : XevyHubStatus.VULNERABLE);
                _isNotMoving = !_hasAttacked;
            }
            else if (_status == XevyHubStatus.VULNERABLE)
            {
                _timer = _attackingTimerCooldown;
                _animator.SetInteger("State", 3);
                _collisionBox.enabled = false;
                _swordHitbox.enabled = true;
                _status = XevyHubStatus.ATTACKING;
            }
            else if (_status == XevyHubStatus.ATTACKING)
            {
                _timer = _defensiveTimerCooldown;
                _animator.SetInteger("State", 2);
                _xevySpell.SetActive(true);
                _swordHitbox.enabled = false;
                _status = XevyHubStatus.DEFENSIVE;
                _hasAttacked = true;
            }
        }
    }
}
