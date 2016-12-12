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
    private float _rotationByFlame = 45;

    [SerializeField]
    private float _flameSpawnDistanceFromHead = 1.5f;


    [SerializeField]
    private GameObject[] _bodyParts;

    protected const float RADIAN_TO_DEGREE = 57.2958f;
    protected const float BODY_PART_TRANSFORM_AJUSTMENT = 0.5f;
    protected const float BODY_PART_ODD_INDEX_SPAWN_DELAY = 1.8f;
    protected const float BODY_PART_EVEN_INDEX_SPAWN_DELAY = 1.0f;

    protected Vector2 _origin;
    protected Vector2[] _pointsToReach;

    private Health _health;
    protected Rigidbody2D _rigidbody;
    private Animator _animator;
    protected BossOrientation _bossOrientation;
    protected DisableCollidersOnBossDefeated _onBossDefeated;

    protected bool _isDead = false;
    protected int _targetedPointIndex;
    protected int _numberOfPointsReached = 0;
    private int _numberBodyPartsSpawned;
    private float _spawnBodyPartTimeLeft;
    private float _attackCooldownTimeLeft;

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
        StartCoroutine(UpdateWhenLiving());
    }

    private void InitializeNeptune()
    {
        _attackCooldownTimeLeft = _attackDelay;
        _spawnBodyPartTimeLeft = BODY_PART_ODD_INDEX_SPAWN_DELAY;
        _numberBodyPartsSpawned = 0;
        _health.HealthPoint = _health.MaxHealth;
        for (int i = 0; i < _bodyParts.Length; i++)
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

    private IEnumerator UpdateWhenLiving()
    {
        while (!_isDead)
        {
            yield return null;

            SpawnOtherBodyParts();
            _attackCooldownTimeLeft -= Time.deltaTime;
            if (_attackCooldownTimeLeft <= 0)
            {
                _attackCooldownTimeLeft = _attackDelay;
                SetFlamesAroundHead();
            }
            MoveInTrajectory();
        }

    }

    private void SpawnOtherBodyParts()
    {
        if (_numberBodyPartsSpawned < _bodyParts.Length)
        {
            _spawnBodyPartTimeLeft -= Time.deltaTime;
            if (_spawnBodyPartTimeLeft <= 0)
            {
                _bodyParts[_numberBodyPartsSpawned].SetActive(true);
                _bodyParts[_numberBodyPartsSpawned].GetComponent<NeptuneBodyAI>().SetIndex(_numberBodyPartsSpawned);
                if (_numberBodyPartsSpawned % 2 == 1)
                {
                    _bodyParts[_numberBodyPartsSpawned].transform.localScale = new Vector2(_bodyParts[_numberBodyPartsSpawned].transform.localScale.x,
                        -_bodyParts[_numberBodyPartsSpawned].transform.localScale.y);
                    _bodyParts[_numberBodyPartsSpawned].transform.position += Vector3.up * BODY_PART_TRANSFORM_AJUSTMENT;
                    _spawnBodyPartTimeLeft = BODY_PART_ODD_INDEX_SPAWN_DELAY;
                }
                else
                {
                    _bodyParts[_numberBodyPartsSpawned].transform.position += Vector3.down * BODY_PART_TRANSFORM_AJUSTMENT;
                    _spawnBodyPartTimeLeft = BODY_PART_EVEN_INDEX_SPAWN_DELAY;
                }
                _bodyParts[_numberBodyPartsSpawned].GetComponent<NeptuneBodyAI>().IsTail = (_numberBodyPartsSpawned == _bodyParts.Length - 1);
                _numberBodyPartsSpawned++;
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

    protected virtual void MoveInTrajectory()
    {
        transform.position = Vector2.MoveTowards(transform.position, _pointsToReach[_targetedPointIndex], _speed * Time.deltaTime);
        if (CheckIfHasReachedTargetedPoint(_pointsToReach[_targetedPointIndex]))
        {
            _targetedPointIndex = (_targetedPointIndex + 1) % _pointsToReach.Length;
            _numberOfPointsReached++;
            RotateAndFlip();
        }
    }

    protected virtual bool CheckIfHasReachedTargetedPoint(Vector2 point)
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
        StopAllCoroutines();
    }
}
