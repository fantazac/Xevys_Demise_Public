using UnityEngine;
using System.Collections;

public class ScarabMovementWithPoints : MonoBehaviour
{

    [SerializeField]
    private Vector3[] _points;

    [SerializeField]
    private bool[] _rotationDirections;

    [SerializeField]
    private ScarabDirection[] _initialDirections;

    private const float MAX_ROTATION = 90f;
    private const float ROTATION_SPEED = 1.8f;
    private const float MOVEMENT_SPEED = 1f;

    private bool _goesBackwards = false;
    private bool _canRotate = true;
    private float _rotateCount = 0;
    private float _frameRotation = 0;
    private bool _currentRotateDirection = false;
    private Vector3 _rotationDirection;

    private int _targetPoint;

    private Vector3 _target;

    private void Start()
    {
        _targetPoint = Random.Range(1, _points.Length - 1);

        transform.position = _points[_targetPoint];

        StartMovementTowardsNewTarget();
        InitializeSpriteDirection();
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
        SetRotationDirection();
        StartCoroutine("Rotate");
    }

    private void SetRotationDirection()
    {
        _currentRotateDirection = _rotationDirections[_targetPoint];
        if (_goesBackwards)
        {
            _currentRotateDirection = !_currentRotateDirection;
        }
        _rotationDirection = _currentRotateDirection ? Vector3.back : Vector3.forward;
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
                transform.Rotate(_rotationDirection * (MAX_ROTATION - _rotateCount));
                break;
            }
            _rotateCount += _frameRotation;
            transform.Rotate(_rotationDirection * _frameRotation);

            yield return null;
        }
        RotationFinished();
    }

    private void FindTarget()
    {
        if (IsAtAFlipCorner())
        {
            FlipSprite();
        }

        //Si le scarab recule et qu'il est sur le point 0, on lui donne comme cible le point où il était juste avant
        //Sinon si le scarab avance et qu'il est sur le dernier point, on lui donne également comme cible le point où il était juste avant
        //Sinon, on lui donne le prochain point disponible dans les 2 cas restants
        _target = _goesBackwards ? _targetPoint == 0 ? _points[++_targetPoint] : _points[--_targetPoint] :
            _targetPoint == _points.Length - 1 ? _target = _points[--_targetPoint] : _target = _points[++_targetPoint];
    }

    private void FlipSprite()
    {
        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        _goesBackwards = !_goesBackwards;
    }

    public void InitializeSpriteDirection()
    {
        if (_initialDirections[_targetPoint] == ScarabDirection.Down)
        {
            transform.Rotate(Vector3.forward * 90);
        }
        else if (_initialDirections[_targetPoint] == ScarabDirection.Up)
        {
            transform.Rotate(Vector3.forward * -90);
        }
        else if (_initialDirections[_targetPoint] == ScarabDirection.Right)
        {
            transform.Rotate(Vector3.forward * 180);
        }
    }

    private bool CanStartRotation()
    {
        return _canRotate && IsCloseToTarget() && !IsAtAFlipCorner();
    }

    private bool IsAtAFlipCorner()
    {
        return _targetPoint == 0 || _targetPoint == _points.Length - 1;
    }

    private bool IsCloseToTarget()
    {
        return Vector3.Distance(_target, transform.position) < transform.localScale.x * 0.5f;
    }

    public bool OnBottomOfPlatform()
    {
        return transform.rotation.eulerAngles.z > 170 || transform.rotation.eulerAngles.z < -170;
    }
}
