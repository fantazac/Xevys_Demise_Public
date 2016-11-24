using UnityEngine;
using System.Collections;

public class VulcanAI : MonoBehaviour
{
    private enum VulcanStatus
    {
        LOWERED,
        RAISING,
        STANDING,
        RETREATING,
        DEAD,
    }

    private const float LOWERED_TIME = 2;
    private const float STANDING_TIME = 5;
    private const float HORIZONTAL_SPEED = 20;
    private const float VERTICAL_SPEED = 1650;

    [SerializeField]
    GameObject _fireball;

    private Health _health;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private BossOrientation _bossOrientation;
    private OnBossDefeated _onBossDefeated;
    private PolygonCollider2D _polygonHitbox;

    private System.Random _random = new System.Random();
    private VulcanStatus _status;
    private bool _criticalStatus = true;
    private float[] _positionsForRaise;
    private float _attackCooldownTimeLeft;
    private float _bodyHeight;
    private float _initialHeight;
    private float _timeLeft = LOWERED_TIME;
    private float _halfHealth;
    private bool _hasShotFireball = false;

    public int CurrentIndex { get; private set; }

    private void Start()
    {
        _status = VulcanStatus.LOWERED;
        _initialHeight = transform.position.y;
        _bodyHeight = transform.localScale.y;
        _positionsForRaise = new float[5];
        for (int x = -2; x < 3; x++)
        {
            _positionsForRaise[x + 2] = transform.position.x + x * transform.localScale.x;
        }
        _health = GetComponent<Health>();
        _halfHealth = _health.HealthPoint / 2;
        _rigidbody = GetComponent<Rigidbody2D>();
        _bossOrientation = GetComponent<BossOrientation>();
        _animator = GetComponent<Animator>();
        _health.OnDeath += OnVulcanDefeated;
        _polygonHitbox = GetComponent<PolygonCollider2D>();
        _polygonHitbox.enabled = false;
    }

    private void OnDestroy()
    {
        _health.OnDeath -= OnVulcanDefeated;
    }

    private void FixedUpdate()
    {
        if (_status == VulcanStatus.LOWERED)
        {
            UpdateWhenLowered();
        }
        else if (_status == VulcanStatus.RAISING)
        {
            UpdateWhenRaising();
        }
        else if (_status == VulcanStatus.STANDING)
        {
            UpdateWhenStanding();
        }
        else if (_status == VulcanStatus.RETREATING)
        {
            UpdateWhenRetreating();
        }
    }

    private void UpdateWhenLowered()
    {
        if (_timeLeft > 0)
        {
            _timeLeft -= Time.fixedDeltaTime;
        }
        else
        {
            _criticalStatus = (_health.HealthPoint <= _halfHealth);
            if (_criticalStatus)
            {
                float horizontalDistanceToClosestPoint = float.MaxValue;
                int closestPointIndex = -1;
                for (int n = 0; n < _positionsForRaise.Length; n+= 2)
                {
                    float horizontalDistanceToSpecificPoint = Mathf.Abs(StaticObjects.GetPlayer().transform.position.x - _positionsForRaise[n]);
                    if (horizontalDistanceToClosestPoint > horizontalDistanceToSpecificPoint)
                    {
                        horizontalDistanceToClosestPoint = horizontalDistanceToSpecificPoint;
                        closestPointIndex = n;
                    }
                }
                transform.position = new Vector3(_positionsForRaise[closestPointIndex], transform.position.y);
                /*if (StaticObjects.GetPlayer().transform.position.x < _positionsForRaise[0] / 2)
                {
                    transform.position = new Vector3(_positionsForRaise[0], transform.position.y, transform.position.z);
                }
                else if (StaticObjects.GetPlayer().transform.position.x > _positionsForRaise[4] / 2)
                {
                    transform.position = new Vector3(_positionsForRaise[4], transform.position.y, transform.position.z);
                }
                else
                {
                    transform.position = new Vector3(_positionsForRaise[2], transform.position.y, transform.position.z);
                }*/
            }
            else
            {
                CurrentIndex = _random.Next() % 2 * 2 + 1;
                transform.position = new Vector3(_positionsForRaise[CurrentIndex], transform.position.y, transform.position.z);
            }
            _bossOrientation.FlipTowardsPlayer();
            //_animator.SetInteger("State", 2);
            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(transform.up * VERTICAL_SPEED);
            _timeLeft = STANDING_TIME;
            _status = VulcanStatus.RAISING;
        }
    }

    private void UpdateWhenRaising()
    {
        if (transform.position.y >= _initialHeight + _bodyHeight)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            _rigidbody.isKinematic = true;
            _status = VulcanStatus.STANDING;
        }
    }

    private void UpdateWhenStanding()
    {
        if (_timeLeft > 0)
        {
            _timeLeft -= Time.fixedDeltaTime;
            if (_timeLeft < 2 && !_hasShotFireball)
            {
                var fireball = Instantiate(_fireball, new Vector3(transform.position.x + _bossOrientation.Orientation * 4.5f, transform.position.y + 1.7f + (_criticalStatus ? 0 : 1.8f)), Quaternion.identity);
                if (!_criticalStatus)
                {
                    ((GameObject)fireball).transform.Rotate(0, 0, 60);
                }
                ((GameObject)fireball).SetActive(true);
                _hasShotFireball = true;
            }
        }
        else
        {
            _hasShotFireball = false;
            _rigidbody.isKinematic = false;
            _polygonHitbox.enabled = true;
            _status = VulcanStatus.RETREATING;
        }
    }

    private void UpdateWhenRetreating()
    {
        //When Vulcan has only half his vitality left, he lowers towards the player.
        if (transform.position.y > _initialHeight)
        {
            if (_health.HealthPoint <= _halfHealth)
            {
                _bossOrientation.FlipTowardsPlayer();
                _rigidbody.AddForce(transform.right * _bossOrientation.Orientation * HORIZONTAL_SPEED);
            }
        }
        else
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            _timeLeft = LOWERED_TIME;
            _rigidbody.isKinematic = true;
            _polygonHitbox.enabled = false;
            _status = VulcanStatus.LOWERED;
        }
    }

    private void OnVulcanDefeated()
    {
        _status = VulcanStatus.DEAD;
        _animator.SetBool("IsDead", true);
        _rigidbody.isKinematic = false;
    }


}
