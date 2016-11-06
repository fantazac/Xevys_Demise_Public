using UnityEngine;
using System.Collections;

public class ScarabMovementWithPlatform : MonoBehaviour
{

    [SerializeField]
    private GameObject _attachedWall;

    private Vector3[] _points;

    private const float MAX_ROTATION = 90f;
    private const float ROTATION_SPEED = 1.8f;
    private const float MOVEMENT_SPEED = 1f;

    private Vector3 _wallPosition;
    private Vector3 _wallScale;

    private float _halfOfWallWidth;
    private float _halfOfScarabWidth;
    private float _halfOfWallHeight;
    private float _halfOfScarabHeight;

    private bool _canRotate = true;
    private float _rotateCount = 0;
    private float _frameRotation = 0;

    private int _targetPoint;

    private Vector3 _target;

    private void Start()
    {
        InitializeDimensions();
        InitializePoints();

        _targetPoint = Random.Range(0, _points.Length);
        transform.position = _points[_targetPoint];

        StartMovementTowardsNewTarget();
        InitializeSpriteDirection();
    }

    private void InitializeDimensions()
    {
        _wallPosition = _attachedWall.transform.position;
        _wallScale = _attachedWall.transform.localScale;

        _halfOfScarabWidth = transform.localScale.x * 0.5f;
        _halfOfScarabHeight = transform.localScale.y * 0.5f;

        _halfOfWallWidth = _wallScale.x * 0.5f;
        _halfOfWallHeight = _wallScale.y * 0.5f;
    }

    private void InitializePoints()
    {
        _points = new Vector3[4];

        _points[0] = new Vector3(_wallPosition.x - _halfOfWallWidth - _halfOfScarabWidth,
            _wallPosition.y - _halfOfWallHeight - _halfOfScarabHeight, transform.position.z);
        _points[1] = new Vector3(_wallPosition.x + _halfOfWallWidth + _halfOfScarabWidth,
            _wallPosition.y - _halfOfWallHeight - _halfOfScarabHeight, transform.position.z);
        _points[2] = new Vector3(_wallPosition.x + _halfOfWallWidth + _halfOfScarabWidth,
            _wallPosition.y + _halfOfWallHeight + _halfOfScarabHeight, transform.position.z);
        _points[3] = new Vector3(_wallPosition.x - _halfOfWallWidth - _halfOfScarabWidth,
            _wallPosition.y + _halfOfWallHeight + _halfOfScarabHeight, transform.position.z);
    }

    private void StartMovementTowardsNewTarget()
    {
        FindTarget();
        StartCoroutine("MoveTowardsTarget");
    }

    private void MovementFinished()
    {
        StartMovementTowardsNewTarget();
    }

    private void StartRotation()
    {
        _canRotate = false;
        StartCoroutine("Rotate");
    }

    private void RotationFinished()
    {
        _rotateCount = 0;
        _canRotate = true;
    }

    private IEnumerator MoveTowardsTarget()
    {
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target, MOVEMENT_SPEED * Time.deltaTime);
            if (CanStartRotation())
            {
                StartRotation();
            }
            if (_target == transform.position)
            {
                break;
            }
            yield return null;
        }
        MovementFinished();
    }

    private IEnumerator Rotate()
    {
        while (true)
        {
            _frameRotation = MAX_ROTATION * Time.deltaTime * ROTATION_SPEED;
            if (_rotateCount + _frameRotation > MAX_ROTATION)
            {
                transform.Rotate(Vector3.forward * (MAX_ROTATION - _rotateCount));
                break;
            }
            _rotateCount += _frameRotation;
            transform.Rotate(Vector3.forward * _frameRotation);

            yield return null;
        }
        RotationFinished();
    }

    private bool CanStartRotation()
    {
        return _canRotate && IsCloseToTarget();
    }

    private bool IsCloseToTarget()
    {
        return Vector3.Distance(_target, transform.position) < _halfOfScarabWidth;
    }

    private void FindTarget()
    {
        _target = _points[(++_targetPoint) % _points.Length];
    }

    public void InitializeSpriteDirection()
    {
        transform.Rotate(Vector3.forward * MAX_ROTATION * (_targetPoint + 1));
    }

    public bool OnBottomOfPlatform()
    {
        return transform.rotation.eulerAngles.z > 170 || transform.rotation.eulerAngles.z < -170;
    }
}
