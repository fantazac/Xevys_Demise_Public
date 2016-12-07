using UnityEngine;
using System.Collections;

public class XevyHubAI : MonoBehaviour
{
    private enum XevyHubStatus
    {
        DEFENSIVE,
        VULNERABLE,
        ATTACKING,
        DEAD,
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
    private Animator _animator;
    private AnimationTags _animTags;

    private GameObject _xevySword;
    private GameObject _xevySpell;
    private PolygonCollider2D _collisionBox;

    private XevyHubStatus _status = XevyHubStatus.DEFENSIVE;
    private bool _isMoving = true;
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
        _xevySword = transform.FindChild("Xevy Sword").gameObject;
        _collisionBox = GetComponent<PolygonCollider2D>();
        _animTags = StaticObjects.GetAnimationTags();
        _animator = GetComponent<Animator>();
        _animator.SetInteger(_animTags.State, 1);
        _bossOrientation.OnBossFlipped += OnBossFlipped;
        _xevySpell = transform.FindChild("Xevy Spell").gameObject;
        GetComponent<Health>().OnDeath += OnXevyHubDefeated;
        StartCoroutine(UpdateWhenMoving());
    }

    private IEnumerator UpdateWhenMoving()
    {
        while (_isMoving)
        {
            yield return null;

            _bossOrientation.FlipTowardsPlayer();
            if (CheckIfMovementCompleted())
            {
                _hasAttacked = false;
                _isMoving = false;
                _actorDirection.FlipDirection();
            }
            else
            {
                transform.position = new Vector3(transform.position.x +
                    _bossOrientation.Orientation * _actorDirection.Direction * _speed * Time.deltaTime, transform.position.y);
            }
        }
        _animator.SetInteger(_animTags.State, 0);
        StartNotMovingCoroutine();
    }

    private IEnumerator UpdateWhenStanding()
    {
        while (!_isMoving)
        {
            if (_timer <= 0)
            {
                if (_status == XevyHubStatus.DEFENSIVE)
                {
                    _timer = _vulnerableTimerCooldown;
                    _animator.SetInteger(_animTags.State, (_hasAttacked ? 1 : 2));
                    _collisionBox.enabled = !_hasAttacked;
                    _xevySpell.SetActive(_hasAttacked);
                    _status = (_hasAttacked ? XevyHubStatus.DEFENSIVE : XevyHubStatus.VULNERABLE);
                    _isMoving = _hasAttacked;
                }
                else if (_status == XevyHubStatus.VULNERABLE)
                {
                    _timer = _attackingTimerCooldown;
                    _animator.SetInteger(_animTags.State, 3);
                    _collisionBox.enabled = false;
                    _xevySword.SetActive(true);
                    _status = XevyHubStatus.ATTACKING;
                }
                else if (_status == XevyHubStatus.ATTACKING)
                {
                    _timer = _defensiveTimerCooldown;
                    _animator.SetInteger(_animTags.State, 2);
                    _xevySpell.SetActive(true);
                    _xevySword.SetActive(false);
                    _status = XevyHubStatus.DEFENSIVE;
                    _hasAttacked = true;
                }
            }
            _timer -= Time.fixedDeltaTime;
            yield return null;
        }

        StartMovingCoroutine();
    }

    private bool CheckIfMovementCompleted()
    {
        return (transform.position.x > (_bossOrientation.IsFacingRight ^ _actorDirection.IsGoingForward ? _leftLimit : _rightLimit) ^
             (_actorDirection.IsGoingForward ^ _bossOrientation.IsFacingRight));
    }

    private void StartMovingCoroutine()
    {
        StopAllCoroutines();
        StartCoroutine(UpdateWhenMoving());
    }

    private void StartNotMovingCoroutine()
    {
        StopAllCoroutines();
        StartCoroutine(UpdateWhenStanding());
    }

    private void OnBossFlipped()
    {
        _actorDirection.FlipDirection();
    }

    private void OnXevyHubDefeated()
    {
        _status = XevyHubStatus.DEAD;
        _animator.SetInteger(_animTags.State, 1);
        _xevySpell.SetActive(false);
        _xevySword.SetActive(false);
        _collisionBox.enabled = false;
    }
}
