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

    /* BEN COUNTER-CORRECTION
     *
     * À moins de vouloir réinventer la roue inutilement ou saboter notre travail, ces variables 
     * doivent OBLIGATOIREMENT demeurer constantes sans possibilité d'édition par un individu externe.
     */
    private const float FLAP_DELAY = 0.5f;
    private const float WING_FLAP = 2.675f;
    private const float RADIAN_TO_DEGREE = 57.2958f;

    private Vector2 _currentPoint;
    private Vector2 _closestPoint;
    private Vector2 _playerPosition;
    private Health _health;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private BossOrientation _bossOrientation;
    private PolygonCollider2D _polygonHitbox;

    private System.Random _random = new System.Random();
    private PhoenixStatus _status;
    private float _flightTimeLeft;
    private float _attackCooldownTimeLeft;
    private float _closestHorizontalPoint;
    private float _closestVerticalPoint;
    private float _flyingSpeed;

    private Vector3 _initialPosition;

    private bool _canUseOnEnable = false;

    private void Start()
    {
        _health = GetComponent<Health>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _bossOrientation = GetComponent<BossOrientation>();
        _animator = GetComponent<Animator>();
        _health.OnDeath += OnPhoenixDefeated;
        _polygonHitbox = GetComponent<PolygonCollider2D>();

        _health.OnDamageTaken += GotHitByPlayer;
        InitializePhoenix();
        SetupPhoenixReset();
        _canUseOnEnable = true;
    }

    private void SetupPhoenixReset()
    {
        _initialPosition = transform.position;
    }

    private void OnEnable()
    {
        if (_canUseOnEnable)
        {
            InitializePhoenix();
            transform.position = _initialPosition;
        }
    }

    private void InitializePhoenix()
    {
        _flyingSpeed = _normalFlyingSpeed;
        _health.HealthPoint = _health.MaxHealth;
        _attackCooldownTimeLeft = 0;
        _currentPoint = (_southWestLimit + _northEastLimit) / 2;
        SetFlyStatus();
    }

    private void FixedUpdate()
    {
        switch (_status)
        {
            //Phoenix observe Bimon et fuit lorsqu'il s'approche trop près de lui. Après un certains temps, il charge sur Bimon.
            case PhoenixStatus.FLY:
                UpdateWhenFlying();
                break;
            //Phoenix fuit vers un point adjacent pour éviter Bimon.
            case PhoenixStatus.FLEE:
                UpdateWhenFleeing();
                break;
            //Phoenix fonce sur Bimon, lui donnant une chance de contre-attaquer.
            case PhoenixStatus.ATTACK:
                UpdateWhenAttacking();
                break;
        }
    }


    private void Flap()
    {
        if (_flightTimeLeft > 0)
        {
            _flightTimeLeft -= Time.fixedDeltaTime;
        }
        else
        {
            _flightTimeLeft = FLAP_DELAY;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, WING_FLAP);
        }
    }

    private void GotHitByPlayer(int hitPoints)
    {
        _flyingSpeed = _hitFlyingSpeed;
        _polygonHitbox.enabled = false;
    }

    private void FindAdjacentPoint()
    {
        int randomNumber = _random.Next();
        if (_currentPoint.Equals(_northEastLimit) || _currentPoint.Equals(_southWestLimit))
        {
            _closestPoint = (randomNumber % 2 == 0 ? _northWestLimit : _southEastLimit);
        }
        else
        {
            _closestPoint = (randomNumber % 2 == 0 ? _northEastLimit : _southWestLimit);
        }
    }

    private void FindFleeingPoint()
    {
        while (_closestPoint.Equals(_currentPoint))
        {
            int pointToFleeIndex = _random.Next() % 4;
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
    }

    private void UpdateWhenFlying()
    {
        _bossOrientation.FlipTowardsPlayer();
        _attackCooldownTimeLeft += Time.fixedDeltaTime;
        if (_attackCooldownTimeLeft > _attackDelay)
        {
            SetAttackStatus();
        }
        else
        {
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
    }

    private bool CheckIfPlayerIsTooClose()
    {
        return Vector2.Distance(StaticObjects.GetPlayer().transform.position, transform.position) < _playerApproachLimit;
    }

    private void UpdateWhenFleeing()
    {
        transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), _closestPoint, _flyingSpeed * Time.fixedDeltaTime);
        if (Vector2.Distance(_closestPoint, transform.position) < 1)
        {
            SetFlyStatus();
        }
    }

    private void UpdateWhenAttacking()
    {
        transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), _playerPosition, _flyingSpeed * Time.fixedDeltaTime);
        if (Vector2.Distance(transform.position, _playerPosition) < 1)
        {
            _closestPoint = _currentPoint;
            FindFleeingPoint();
            SetFleeStatus();
        }
    }

    private void SetFlyStatus()
    {
        _currentPoint = _closestPoint;
        _rigidbody.isKinematic = false;
        transform.rotation = Quaternion.identity;
        _flightTimeLeft = 0;
        _status = PhoenixStatus.FLY;
    }

    private void SetFleeStatus()
    {
        _flyingSpeed = _normalFlyingSpeed;
        transform.rotation = Quaternion.identity;
        _rigidbody.isKinematic = true;
        _attackCooldownTimeLeft = 0;
        _bossOrientation.FlipTowardsSpecificPoint(_closestPoint);
        transform.Rotate(0, 0, RADIAN_TO_DEGREE * Mathf.Atan((_closestPoint.y - transform.position.y) / (_closestPoint.x - transform.position.x)));
        _polygonHitbox.enabled = false;
        _status = PhoenixStatus.FLEE;
    }

    private void SetAttackStatus()
    {
        _playerPosition = StaticObjects.GetPlayer().transform.position;
        transform.Rotate(0, 0, RADIAN_TO_DEGREE * Mathf.Atan((_playerPosition.y - transform.position.y) / (_playerPosition.x - transform.position.x)));
        _attackCooldownTimeLeft = 0;
        _rigidbody.isKinematic = true;
        _status = PhoenixStatus.ATTACK;
        _flyingSpeed = _attackFlyingSpeed;
        _polygonHitbox.enabled = true;
    }

    public void OnPhoenixDefeated()
    {
        _status = PhoenixStatus.DEAD;
        _animator.SetBool("IsDead", true);
        _rigidbody.isKinematic = false;
    }
}
