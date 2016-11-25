using UnityEngine;
using System.Collections;

public class NeptuneHeadAI : MonoBehaviour
{
    private const int UPPER_FLAME_INDEX = 0;
    private const int RIGHT_FLAME_INDEX = 2;
    private const int LOWER_FLAME_INDEX = 4;
    private const int LEFT_FLAME_INDEX = 6;
    private const int SOUTH_EAST_LIMIT = 0;
    private const int SOUTH_WEST_LIMIT = 2;
    private const int NUMBER_FLAMES_SPAWNED = 8;
    private const int NUMBER_POINTS_TO_REACH = 4;
    protected const float SPEED = 0.95f;
    protected const float RADIAN_TO_DEGREE = 57.2958f;
    private const float ATTACK_DELAY = 5;
    private const float WARNING_DELAY = 2;
    private const float ROTATION_BY_FLAME = 45;
    private const float FLAME_SPAWN_SPACING = 1.5f;
    private const float BODY_PART_SPAWN_DELAY = 1.8f;

    [SerializeField]
    GameObject _flame;
    [SerializeField]
    protected float _horizontalLimit;
    [SerializeField]
    protected float _verticalLimit;

    protected Vector2 _origin;
    protected Vector2[] _pointsToReach;

    private Health _health;
    private GameObject[] _bodyParts;
    protected Rigidbody2D _rigidbody;
    private Animator _animator;
    protected BossOrientation _bossOrientation;
    protected OnBossDefeated _onBossDefeated;

    protected int _targetedPointIndex;
    private int numberBodyPartsSpawned;
    private float _spawnBodyPartTimeLeft;
    private float _attackCooldownTimeLeft;

    public float HorizontalLimit
    {
        get
        {
            return _horizontalLimit;
        }
    }

    public float VerticalLimit
    {
        get
        {
            return _verticalLimit;
        }
    }

    protected virtual void Start()
    {
        _bodyParts = GameObject.FindGameObjectsWithTag("NeptuneBody");
        
        _health = GetComponent<Health>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _onBossDefeated = GetComponent<OnBossDefeated>();
        _health.OnDeath += OnNeptuneDefeated;
        InitializeNeptune();
    }

    private void OnEnable()
    {
        InitializeNeptune();
    }

    private void InitializeNeptune()
    {
        if(_health != null)
        {
            _attackCooldownTimeLeft = ATTACK_DELAY;
            _spawnBodyPartTimeLeft = BODY_PART_SPAWN_DELAY;
            numberBodyPartsSpawned = 0;
            _health.HealthPoint = _health.MaxHealth;
            foreach (GameObject bodyPart in _bodyParts)
            {
                bodyPart.transform.position = transform.position;
                bodyPart.SetActive(false);
            }
            InitializePoints();
            RotateAndFlip();
        }
    }

    protected void InitializePoints()
    {
        _bossOrientation = GetComponent<BossOrientation>();
        _horizontalLimit = Mathf.Abs(_horizontalLimit);
        _verticalLimit = Mathf.Abs(_verticalLimit);
        _origin = transform.position;
        _pointsToReach = new Vector2[NUMBER_POINTS_TO_REACH]
        {
            new Vector2(_origin.x + _horizontalLimit, _origin.y - _verticalLimit),
            new Vector2(_origin.x + _horizontalLimit, _origin.y + _verticalLimit),
            new Vector2(_origin.x - _horizontalLimit, _origin.y - _verticalLimit),
            new Vector2(_origin.x - _horizontalLimit, _origin.y + _verticalLimit),
        };
        _targetedPointIndex = (_bossOrientation.IsFacingRight ? SOUTH_EAST_LIMIT : SOUTH_WEST_LIMIT);
        _bossOrientation.FlipTowardsSpecificPoint(_pointsToReach[_targetedPointIndex]);
    }

    private void FixedUpdate()
    {
        SpawnOtherBodyParts();
        _attackCooldownTimeLeft -= Time.fixedDeltaTime;
        if (_attackCooldownTimeLeft <= 0)
        {
            _attackCooldownTimeLeft = ATTACK_DELAY;
            SetFlamesAroundHead();
        }
        else if (_attackCooldownTimeLeft < WARNING_DELAY)
        {
            _animator.SetBool("IsAttacking", true);
        }
        else
        {
            _animator.SetBool("IsAttacking", false);
        }
        MoveInTrajectory();
    }

    private void SpawnOtherBodyParts()
    {
        if (numberBodyPartsSpawned < _bodyParts.Length)
        {
            _spawnBodyPartTimeLeft -= Time.fixedDeltaTime;
            if (_spawnBodyPartTimeLeft <= 0)
            {
                _spawnBodyPartTimeLeft = BODY_PART_SPAWN_DELAY;
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
        for (int x = 0; x < NUMBER_FLAMES_SPAWNED; x++)
        {
            float xPosition = -FLAME_SPAWN_SPACING;
            if (x < LOWER_FLAME_INDEX && x > UPPER_FLAME_INDEX)
            {
                xPosition = FLAME_SPAWN_SPACING;
            }
            else if (x == UPPER_FLAME_INDEX || x == LOWER_FLAME_INDEX)
            {
                xPosition = 0;
            }
            float yPosition = FLAME_SPAWN_SPACING;
            if (x == RIGHT_FLAME_INDEX || x == LEFT_FLAME_INDEX)
            {
                yPosition = 0;
            }
            else if (x > RIGHT_FLAME_INDEX && x < LEFT_FLAME_INDEX)
            {
                yPosition = -FLAME_SPAWN_SPACING;
            }
            var flame = Instantiate(_flame, transform.position + new Vector3(xPosition, yPosition), Quaternion.identity);
            ((GameObject)flame).transform.Rotate(0, 0, x * ROTATION_BY_FLAME);
            ((GameObject)flame).SetActive(true);
        }
    }

    protected void MoveInTrajectory()
    {
        transform.position = Vector2.MoveTowards(transform.position, _pointsToReach[_targetedPointIndex], SPEED * Time.fixedDeltaTime);
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
    }
}
