using UnityEngine;

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

    [SerializeField]
    GameObject _fireball;

    [SerializeField]
    private float _loweredTime = 2;

    [SerializeField]
    private float _standingTime = 5;

    [SerializeField]
    private float _horizontalSpeed = 20;

    [SerializeField]
    private float _raisingUpwardForce = 1650;

    private Health _health;
    private Rigidbody2D _rigidbody;
    private BossOrientation _bossOrientation;
    private DisableCollidersOnBossDefeated _onBossDefeated;
    private PolygonCollider2D _polygonHitbox;

    private System.Random _random = new System.Random();
    private VulcanStatus _status;
    private bool _criticalStatus = true;
    private float[] _positionsForRaise;
    private float _attackCooldownTimeLeft;
    private float _bodyHeight;
    private float _initialHeight;
    private float _timeLeft;
    private float _halfHealth;
    private bool _hasShotFireball = false;

    public int CurrentIndex { get; private set; }

    private void Start()
    {
        _health = GetComponent<Health>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _bossOrientation = GetComponent<BossOrientation>();
        _health.OnDeath += OnVulcanDefeated;
        _polygonHitbox = GetComponent<PolygonCollider2D>();
        InitializeVulcan();
    }

    private void InitializeVulcan()
    {
        _timeLeft = _loweredTime;
        _health.HealthPoint = _health.MaxHealth;
        _halfHealth = _health.HealthPoint / 2;
        _polygonHitbox.enabled = false;
        _status = VulcanStatus.LOWERED;
        _initialHeight = transform.position.y;
        _bodyHeight = transform.localScale.y;
        _positionsForRaise = new float[5];
        for (int x = -2; x < 3; x++)
        {
            _positionsForRaise[x + 2] = transform.position.x + x * transform.localScale.x;
        }
    }

    private void FixedUpdate()
    {
        switch (_status)
        {
            //Vulcan attend au fond du cratère.
            case VulcanStatus.LOWERED:
                UpdateWhenLowered();
                break;
            //Vulcan remonte à la surface pour affronter Bimon.
            case VulcanStatus.RAISING:
                UpdateWhenRaising();
                break;
            //Vulcan surveille Bimon pour lui lancer un projectile.
            case VulcanStatus.STANDING:
                UpdateWhenStanding();
                break;
            //Vulcan redescend au fond du cratère pour démarrer une autre manche.
            case VulcanStatus.RETREATING:
                UpdateWhenRetreating();
                break;
        }
    }

    private void SelectPointClosestToPlayer()
    {
        float horizontalDistanceToClosestPoint = float.MaxValue;
        int closestPointIndex = -1;
        for (int n = 0; n < _positionsForRaise.Length; n += 2)
        {
            float horizontalDistanceToSpecificPoint = Mathf.Abs(StaticObjects.GetPlayer().transform.position.x - _positionsForRaise[n]);
            if (horizontalDistanceToClosestPoint > horizontalDistanceToSpecificPoint)
            {
                horizontalDistanceToClosestPoint = horizontalDistanceToSpecificPoint;
                closestPointIndex = n;
            }
        }
        transform.position = new Vector3(_positionsForRaise[closestPointIndex], transform.position.y);
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
                SelectPointClosestToPlayer();
            }
            else
            {
                CurrentIndex = _random.Next() % 2 * 2 + 1;
                transform.position = new Vector3(_positionsForRaise[CurrentIndex], transform.position.y, transform.position.z);
            }
            _bossOrientation.FlipTowardsPlayer();
            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(transform.up * _raisingUpwardForce);
            _timeLeft = _standingTime;
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
                GameObject fireball = (GameObject)Instantiate(_fireball, new Vector3(transform.position.x + _bossOrientation.Orientation * 4.5f,
                    transform.position.y + 1.7f + (_criticalStatus ? 0 : 1.8f)), Quaternion.identity);
                if (!_criticalStatus)
                {
                    fireball.transform.Rotate(0, 0, 60);
                }
                fireball.GetComponent<MoveFireball>().SetVulcan(gameObject);
                fireball.SetActive(true);
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
        //Lorsque Vulcan est dans un état critique, il se met à foncer sur Bimon.
        if (transform.position.y > _initialHeight)
        {
            if (_health.HealthPoint <= _halfHealth)
            {
                _bossOrientation.FlipTowardsPlayer();
                _rigidbody.AddForce(transform.right * _bossOrientation.Orientation * _horizontalSpeed);
            }
        }
        else
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            _timeLeft = _loweredTime;
            _rigidbody.isKinematic = true;
            _polygonHitbox.enabled = false;
            _status = VulcanStatus.LOWERED;
        }
    }

    private void OnVulcanDefeated()
    {
        _status = VulcanStatus.DEAD;
        _rigidbody.isKinematic = false;
    }
}
