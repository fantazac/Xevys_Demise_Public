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

    [SerializeField]
    private float _downSpeed = 5.5f;

    [SerializeField]
    private float _upSpeed = 3;

    private WaitForSeconds _onGroundDelay;

    private DetectPlayer _detectPlayer;
    private float _playerDetectionHitboxInitialYPosition;

    private Vector3 _roofTarget;
    private Vector3 _floorTarget;
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
        GetComponent<Health>().OnDeath += StopMovementOnDeath;

        _initialPosition = transform.position;
        _minX = _initialPosition.x - _leftDistance;
        _maxX = _initialPosition.x + _rightDistance;

        _onGroundDelay = new WaitForSeconds(_immobileDurationOnGround);
    }

    private void StartMovementDown()
    {
        OnBatMovement();
        StartCoroutine(MoveBat());
    }

    private IEnumerator MoveBat()
    {
        _floorTarget = new Vector3(transform.position.x, _lowestY, transform.position.z);
        while (transform.position.y > _lowestY)
        {
            transform.position = Vector3.MoveTowards(transform.position, _floorTarget, _downSpeed * Time.deltaTime);

            yield return null;
        }
        transform.position = _floorTarget;

        yield return _onGroundDelay;

        _roofTarget = new Vector3(Random.Range(_minX, _maxX), _initialPosition.y, _initialPosition.z);
        while (transform.position.y < _roofTarget.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, _roofTarget, _upSpeed * Time.deltaTime);

            yield return null;
        }
        transform.position = _roofTarget;
        StopMovement();
    }

    private void StopMovement()
    {
        OnBatReachedTarget();
        _playerDetectionHitbox.transform.position = 
            new Vector3(transform.position.x, _playerDetectionHitboxInitialYPosition, transform.position.z);
        _detectPlayer.EnableHitbox();
    }

    private void StopMovementOnDeath()
    {
        StopAllCoroutines();
        enabled = false;
    }

    public bool IsCloseToTop()
    {
        return _initialPosition.y - transform.position.y < 1;
    }
}
