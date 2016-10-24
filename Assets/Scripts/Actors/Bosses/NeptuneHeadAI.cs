using UnityEngine;
using System.Collections;

public class NeptuneHeadAI : MonoBehaviour
{
    protected const float SPEED = 0.95f;
    private const float RADIAN_TO_DEGREE = 57.2958f;
    private const float ATTACK_DELAY = 5;
    private const float WARNING_DELAY = 2;
    private const float BODY_PART_SPAWN_DELAY = 2;

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

    private float _attackCooldownTimeLeft = ATTACK_DELAY;
    private float _spawnBodyPartTimeLeft = BODY_PART_SPAWN_DELAY;
    private int numberBodyPartsSpawned = 0;
    private int numberBodyParts;

    // Use this for initialization
    protected virtual void Start()
    {
        _bodyParts = GameObject.FindGameObjectsWithTag("NeptuneBody");
        numberBodyParts = _bodyParts.Length;
        foreach (GameObject bodyPart in _bodyParts)
        {
            bodyPart.SetActive(false);
        }
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _onBossDefeated = GetComponent<OnBossDefeated>();
        _onBossDefeated.onDefeated += OnNeptuneDefeated;
        InitializePoints();
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

    // Update is called once per frame
    private void Update()
    {
        if (numberBodyPartsSpawned < numberBodyParts)
        {
            _spawnBodyPartTimeLeft -= Time.fixedDeltaTime;
            if (_spawnBodyPartTimeLeft <= 0)
            {
                _bodyParts[numberBodyPartsSpawned].SetActive(true);
                _spawnBodyPartTimeLeft = BODY_PART_SPAWN_DELAY;
                numberBodyPartsSpawned++;
            }
        }
        _attackCooldownTimeLeft -= Time.fixedDeltaTime;
        if (_attackCooldownTimeLeft <= ATTACK_DELAY)
        {
            //Attack
        }
        else if (_attackCooldownTimeLeft < WARNING_DELAY)
        {
            //Warning animation
        }
        MoveInTrajectory();
    }

    protected void MoveInTrajectory()
    {
        transform.rotation = Quaternion.identity;
        transform.position = Vector2.MoveTowards(transform.position, _targetedPoint, SPEED * Time.fixedDeltaTime);
        if (_targetedPoint == _southWestLimit && transform.position.x <= _southWestLimit.x && transform.position.y <= _southWestLimit.y)
        {
            _targetedPoint = _northWestLimit;
            _flipBoss.CheckSpecificPointForFlip(_targetedPoint);
        }
        else if (_targetedPoint == _northWestLimit && transform.position.x <= _northWestLimit.x && transform.position.y >= _northWestLimit.y)
        {
            _targetedPoint = _southEastLimit;
            _flipBoss.CheckSpecificPointForFlip(_targetedPoint);
        }
        else if (_targetedPoint == _southEastLimit && transform.position.x >= _southEastLimit.x && transform.position.y <= _southEastLimit.y)
        {
            _targetedPoint = _northEastLimit;
            _flipBoss.CheckSpecificPointForFlip(_targetedPoint);
        }
        else if (_targetedPoint == _northEastLimit && transform.position.x >= _northEastLimit.x && transform.position.y >= _northEastLimit.y)
        {
            _targetedPoint = _southWestLimit;
            _flipBoss.CheckSpecificPointForFlip(_targetedPoint);
        }
        transform.Rotate(0, 0, RADIAN_TO_DEGREE * Mathf.Atan((_targetedPoint.y - transform.position.y) / (_targetedPoint.x - transform.position.x)));
    }

    private void OnNeptuneDefeated()
    {

    }

    private void FlipAndRotate()
    {

    }
}
