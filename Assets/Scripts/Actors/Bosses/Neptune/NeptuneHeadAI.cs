using UnityEngine;
using System.Collections;

public class NeptuneHeadAI : MonoBehaviour
{
    protected const float SPEED = 5f;// 0.95f;
    protected const float RADIAN_TO_DEGREE = 57.2958f;
    private const float ATTACK_DELAY = 5;
    private const float WARNING_DELAY = 2;
    private const float BODY_PART_SPAWN_DELAY = 1.8f;
    private const float FLAME_SPAWN_SPACING = 1.5f;

    [SerializeField]
    GameObject _flame;
    [SerializeField]
    protected float _horizontalLimit;
    [SerializeField]
    protected float _verticalLimit;

    protected Vector2 _origin;
    protected Vector2 _targetedPoint;
    protected Vector2 _northEastLimit;
    protected Vector2 _southEastLimit;
    protected Vector2 _southWestLimit;
    protected Vector2 _northWestLimit;

    private GameObject[] _bodyParts;
    protected Rigidbody2D _rigidbody;
    private Animator _animator;
    protected FlipBoss _flipBoss;
    protected OnBossDefeated _onBossDefeated;

    private float _attackCooldownTimeLeft;
    private float _spawnBodyPartTimeLeft;
    private int numberBodyPartsSpawned;

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
        _attackCooldownTimeLeft = ATTACK_DELAY;
        _spawnBodyPartTimeLeft = BODY_PART_SPAWN_DELAY;
        numberBodyPartsSpawned = 0;
        _bodyParts = GameObject.FindGameObjectsWithTag("NeptuneBody");
        foreach (GameObject bodyPart in _bodyParts)
        {
            bodyPart.SetActive(false);
        }
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _onBossDefeated = GetComponent<OnBossDefeated>();
        _onBossDefeated.onDefeated += OnNeptuneDefeated;
        InitializePoints();
        RotateAndFlip();
    }

    protected void InitializePoints()
    {
        _flipBoss = GetComponent<FlipBoss>();
        _horizontalLimit = Mathf.Abs(_horizontalLimit);
        _verticalLimit = Mathf.Abs(_verticalLimit);
        _origin = transform.position;
        _northEastLimit = new Vector2(_origin.x + _horizontalLimit, _origin.y + _verticalLimit);
        _southEastLimit = new Vector2(_origin.x + _horizontalLimit, _origin.y - _verticalLimit);
        _southWestLimit = new Vector2(_origin.x - _horizontalLimit, _origin.y - _verticalLimit);
        _northWestLimit = new Vector2(_origin.x - _horizontalLimit, _origin.y + _verticalLimit);
        _targetedPoint = (_flipBoss.IsFacingLeft ? _southWestLimit : _southEastLimit);
        _flipBoss.CheckSpecificPointForFlip(_targetedPoint);
    }

    private void Update()
    {
        if (numberBodyPartsSpawned < _bodyParts.Length)
        {
            _spawnBodyPartTimeLeft -= Time.fixedDeltaTime;
            if (_spawnBodyPartTimeLeft <= 0)
            {
                _bodyParts[numberBodyPartsSpawned].SetActive(true);
                _spawnBodyPartTimeLeft = BODY_PART_SPAWN_DELAY;
                if (numberBodyPartsSpawned % 2 == 1)
                {
                    _bodyParts[numberBodyPartsSpawned].transform.localScale = new Vector2(_bodyParts[numberBodyPartsSpawned].transform.localScale.x,-1 * _bodyParts[numberBodyPartsSpawned].transform.localScale.y);
                    //_bodyParts[numberBodyPartsSpawned].GetComponent<SpriteRenderer>().flipY = !_bodyParts[numberBodyPartsSpawned].GetComponent<SpriteRenderer>().flipY;
                }
                if (numberBodyPartsSpawned == _bodyParts.Length - 1)
                {
                    _bodyParts[numberBodyPartsSpawned].GetComponent<NeptuneBodyAI>().SetLastPart();
                }
                numberBodyPartsSpawned++;
            }
        }
        _attackCooldownTimeLeft -= Time.fixedDeltaTime;
        if (_attackCooldownTimeLeft <= 0)
        {
            _attackCooldownTimeLeft = ATTACK_DELAY;
            for (int x = 0; x < 8; x++)
            {
                float xPosition = -1 * FLAME_SPAWN_SPACING;
                if (x < 4 && x > 0)
                {
                    xPosition = FLAME_SPAWN_SPACING;
                }
                else if (x == 0 || x == 4)
                {
                    xPosition = 0;
                }
                float yPosition = FLAME_SPAWN_SPACING;
                if (x == 2 || x == 6)
                {
                    yPosition = 0;
                }
                else if (x > 2 && x < 6)
                {
                    yPosition = -1 * FLAME_SPAWN_SPACING;
                }
                var flame = Instantiate(_flame, transform.position + new Vector3(xPosition, yPosition), Quaternion.identity);
                ((GameObject)flame).transform.Rotate(0, 0, x * 45);
                ((GameObject)flame).SetActive(true);
            }
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

    protected void MoveInTrajectory()
    {
        
        transform.position = Vector2.MoveTowards(transform.position, _targetedPoint, SPEED * Time.fixedDeltaTime);
        if (_targetedPoint == _southWestLimit && transform.position.x <= _southWestLimit.x && transform.position.y <= _southWestLimit.y)
        {
            _targetedPoint = _northWestLimit;
            RotateAndFlip();
        }
        else if (_targetedPoint == _northWestLimit && transform.position.x <= _northWestLimit.x && transform.position.y >= _northWestLimit.y)
        {
            _targetedPoint = _southEastLimit;
            RotateAndFlip();
        }
        else if (_targetedPoint == _southEastLimit && transform.position.x >= _southEastLimit.x && transform.position.y <= _southEastLimit.y)
        {
            _targetedPoint = _northEastLimit;
            RotateAndFlip();
        }
        else if (_targetedPoint == _northEastLimit && transform.position.x >= _northEastLimit.x && transform.position.y >= _northEastLimit.y)
        {
            _targetedPoint = _southWestLimit;
            RotateAndFlip();
        }
    }

    protected virtual void RotateAndFlip()
    {
        _flipBoss.CheckSpecificPointForFlip(_targetedPoint);
        transform.localScale = new Vector2(transform.localScale.x, -1 * transform.localScale.y);
        transform.rotation = Quaternion.identity;
        transform.Rotate(0, 0, RADIAN_TO_DEGREE * Mathf.Atan((_targetedPoint.y - transform.position.y) / (_targetedPoint.x - transform.position.x)) + (_flipBoss.IsFacingLeft ? 270 : 90));
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
