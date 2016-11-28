using UnityEngine;
using System.Collections;

public class NeptuneHeadAI : MonoBehaviour
{
    [SerializeField]
    GameObject _flame;

    [SerializeField]
    protected float _horizontalLimit;

    [SerializeField]
    protected float _verticalLimit;

    [SerializeField]
    private int _upperFlameIndex = 0;

    [SerializeField]
    private int _rightFlameIndex = 2;

    [SerializeField]
    private int _lowerFlameIndex = 4;

    [SerializeField]
    private int _leftFlameIndex = 6;

    [SerializeField]
    private int _southEastIndex = 0;

    [SerializeField]
    private int _southWestIndex = 2;

    [SerializeField]
    private int _numberFlamesSpawned = 8;

    [SerializeField]
    protected float _speed = 0.95f;

    [SerializeField]
    private float _attackDelay = 5;

    [SerializeField]
    private float _warningDelay = 2;

    [SerializeField]
    private float _rotationByFlame = 45;

    [SerializeField]
    private float _flameSpawnDistanceFromHead = 1.5f;

    [SerializeField]
    private float _bodyPartSpawnDelay = 1.8f;

    [SerializeField]
    private GameObject[] _bodyParts;

    /* BEN COUNTER-CORRECTION
     *
     * À moins de vouloir réinventer la roue inutilement, cette variable doit
     * OBLIGATOIREMENT demeurer constante sans possibilité d'édition par un individu externe.
     */
    protected const float RADIAN_TO_DEGREE = 57.2958f;

    protected Vector2 _origin;
    protected Vector2[] _pointsToReach;

    private Health _health;
    protected Rigidbody2D _rigidbody;
    private Animator _animator;
    protected BossOrientation _bossOrientation;
    protected DisableCollidersOnBossDefeated _onBossDefeated;

    protected int _targetedPointIndex;
    private int numberBodyPartsSpawned;
    private float _spawnBodyPartTimeLeft;
    private float _attackCooldownTimeLeft;
    private bool _isDead = false;

    public float HorizontalLimit { get { return _horizontalLimit; } }

    public float VerticalLimit { get { return _verticalLimit; } }

    protected virtual void Start()
    {
        _health = GetComponent<Health>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _onBossDefeated = GetComponent<DisableCollidersOnBossDefeated>();
        _health.OnDeath += OnNeptuneDefeated;
        InitializeNeptune();
    }

    private void InitializeNeptune()
    {
        _attackCooldownTimeLeft = _attackDelay;
        _spawnBodyPartTimeLeft = _bodyPartSpawnDelay;
        numberBodyPartsSpawned = 0;
        _health.HealthPoint = _health.MaxHealth;
        for(int i = 0; i < _bodyParts.Length; i++)
        {
            _bodyParts[i] = (GameObject)Instantiate(_bodyParts[i], transform.position, new Quaternion());
            _bodyParts[i].transform.parent = gameObject.transform.parent;
            _bodyParts[i].SetActive(false);
        }
        InitializePoints();
        RotateAndFlip();
    }

    protected void InitializePoints()
    {
        _bossOrientation = GetComponent<BossOrientation>();
        _horizontalLimit = Mathf.Abs(_horizontalLimit);
        _verticalLimit = Mathf.Abs(_verticalLimit);
        _origin = transform.position;
        _pointsToReach = new Vector2[]
        {
            new Vector2(_origin.x + _horizontalLimit, _origin.y - _verticalLimit),
            new Vector2(_origin.x + _horizontalLimit, _origin.y + _verticalLimit),
            new Vector2(_origin.x - _horizontalLimit, _origin.y - _verticalLimit),
            new Vector2(_origin.x - _horizontalLimit, _origin.y + _verticalLimit),
        };
        _targetedPointIndex = (_bossOrientation.IsFacingRight ? _southEastIndex : _southWestIndex);
        _bossOrientation.FlipTowardsSpecificPoint(_pointsToReach[_targetedPointIndex]);
    }

    private void FixedUpdate()
    {
        if (!_isDead)
        {
            SpawnOtherBodyParts();
            _attackCooldownTimeLeft -= Time.deltaTime;
            if (_attackCooldownTimeLeft <= 0)
            {
                _attackCooldownTimeLeft = _attackDelay;
                SetFlamesAroundHead();
            }
            else if (_attackCooldownTimeLeft < _warningDelay)
            {
                _animator.SetBool("IsAttacking", true);
            }
            else
            {
                _animator.SetBool("IsAttacking", false);
            }
            MoveInTrajectory();
        }
    }

    private void SpawnOtherBodyParts()
    {
        if (numberBodyPartsSpawned < _bodyParts.Length)
        {
            _spawnBodyPartTimeLeft -= Time.fixedDeltaTime;
            if (_spawnBodyPartTimeLeft <= 0)
            {
                _spawnBodyPartTimeLeft = _bodyPartSpawnDelay;
                _bodyParts[numberBodyPartsSpawned].SetActive(true);
                if (numberBodyPartsSpawned % 2 == 1)
                {
                    _bodyParts[numberBodyPartsSpawned].transform.localScale = new Vector2(_bodyParts[numberBodyPartsSpawned].transform.localScale.x,
                        -_bodyParts[numberBodyPartsSpawned].transform.localScale.y);
                }
                numberBodyPartsSpawned++;
            }
        }
    }

    private void SetFlamesAroundHead()
    {
        for (int x = 0; x < _numberFlamesSpawned; x++)
        {
            float xPosition = -_flameSpawnDistanceFromHead;
            if (x < _lowerFlameIndex && x > _upperFlameIndex)
            {
                xPosition = _flameSpawnDistanceFromHead;
            }
            else if (x == _upperFlameIndex || x == _lowerFlameIndex)
            {
                xPosition = 0;
            }
            float yPosition = _flameSpawnDistanceFromHead;
            if (x == _rightFlameIndex || x == _leftFlameIndex)
            {
                yPosition = 0;
            }
            else if (x > _rightFlameIndex && x < _leftFlameIndex)
            {
                yPosition = -_flameSpawnDistanceFromHead;
            }
            GameObject flame = (GameObject)Instantiate(_flame, transform.position + new Vector3(xPosition, yPosition), Quaternion.identity);
            flame.transform.Rotate(0, 0, x * _rotationByFlame);
            flame.SetActive(true);
        }
    }

    protected void MoveInTrajectory()
    {
        transform.position = Vector2.MoveTowards(transform.position, _pointsToReach[_targetedPointIndex], _speed * Time.fixedDeltaTime);
        if (CheckIfHasReachedTargetedPoint(_pointsToReach[_targetedPointIndex]))
        {
            _targetedPointIndex = (_targetedPointIndex + 1) % _pointsToReach.Length;
            RotateAndFlip();
        }
    }

    private bool CheckIfHasReachedTargetedPoint(Vector2 point)
    {
        return 0 == Vector2.Distance(transform.position, _pointsToReach[_targetedPointIndex]);
    }

    protected virtual void RotateAndFlip()
    {
        _bossOrientation.FlipTowardsSpecificPoint(_pointsToReach[_targetedPointIndex]);
        transform.localScale = new Vector2(transform.localScale.x, -1 * transform.localScale.y);
        transform.rotation = Quaternion.identity;
        transform.Rotate(0, 0, RADIAN_TO_DEGREE * Mathf.Atan((_pointsToReach[_targetedPointIndex].y - transform.position.y) /
            (_pointsToReach[_targetedPointIndex].x - transform.position.x)) + (_bossOrientation.IsFacingRight ? 90 : 270));
    }

    private void OnNeptuneDefeated()
    {
        foreach (GameObject bodyPart in _bodyParts)
        {
            bodyPart.GetComponent<Rigidbody2D>().isKinematic = false;
        }
        _rigidbody.isKinematic = false;
        _animator.SetBool("IsDead", true);
        _isDead = true;
    }
}
