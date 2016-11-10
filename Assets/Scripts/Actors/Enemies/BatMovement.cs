using UnityEngine;
using System.Collections;

public class BatMovement : MonoBehaviour
{

    [SerializeField]
    private GameObject _playerDetectionHitbox;

    [SerializeField]
    private float _leftDistance = 0;

    [SerializeField]
    private float _rightDistance = 0;

    [SerializeField]
    private float _lowestY = 0;

    [SerializeField]
    private float _immobileDurationOnGround = 1;

    private AudioSourcePlayer _soundPlayer;
    private Animator _animator;

    private DetectPlayer _detectPlayer;
    private float _playerDetectionHitboxInitialYPosition;

    private const float DOWN_SPEED = 5.5f;
    private const float UP_SPEED = 3;

    private Vector3 _target;
    private Vector3 _initialPosition;

    private float _minX = 0;
    private float _maxX = 0;

    public delegate void OnBatMovementHandler();
    public event OnBatMovementHandler OnBatMovement;

    public delegate void OnBatReachedTargetHandler();
    public event OnBatReachedTargetHandler OnBatReachedTarget;

    private void Start()
    {
        _detectPlayer = _playerDetectionHitbox.GetComponent<DetectPlayer>();
        _detectPlayer.OnDetectedPlayer += StartMovementDown;
        _playerDetectionHitboxInitialYPosition = _playerDetectionHitbox.GetComponent<Transform>().position.y;

        _initialPosition = transform.position;
        _minX = _initialPosition.x - _leftDistance;
        _maxX = _initialPosition.x + _rightDistance;
        _animator = GetComponent<Animator>();
    }

    private void StartMovementDown()
    {
        _animator.SetBool("IsFlying", true);
        OnBatMovement();
        StartCoroutine("MoveDown");
    }

    private IEnumerator MoveDown()
    {
        while (transform.position.y > _lowestY)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - (DOWN_SPEED * Time.deltaTime), transform.position.z);

            yield return null;
        }
        transform.position = new Vector3(transform.position.x, _lowestY, transform.position.z);
        Invoke("StartMovementUp", _immobileDurationOnGround);
    }

    private void StartMovementUp()
    {
        FindTarget();
        StartCoroutine("MoveUp");
    }

    private IEnumerator MoveUp()
    {
        while (transform.position.y < _target.y)
        {
            transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, transform.position.z), _target, UP_SPEED * Time.deltaTime);

            yield return null;
        }
        transform.position = _target;
        StopMovement();
    }

    private void StopMovement()
    {
        OnBatReachedTarget();
        _playerDetectionHitbox.transform.position = 
            new Vector3(transform.position.x, _playerDetectionHitboxInitialYPosition, transform.position.z);
        _animator.SetBool("IsFlying", false);
        _detectPlayer.EnableHitbox();
    }

    private void FindTarget()
    {
        _target = new Vector3(Random.Range(_minX, _maxX), _initialPosition.y, _initialPosition.z);
    }

    public bool CloseToTop()
    {
        return _initialPosition.y - transform.position.y < 1;
    }
}
