using UnityEngine;
using System.Collections;

public class ScarabMovementWithPoints : ScarabMovement
{
    [SerializeField]
    private Vector3[] _targetPoints;

    [SerializeField]
    private bool[] _rotationDirections;

    private bool _currentRotateDirection = false;

    protected override void Start()
    {
        _points = _targetPoints;

        _halfOfScarabWidth = transform.localScale.x * 0.5f;

        _selectedTargetPoint = Random.Range(1, _points.Length - 1);

        transform.position = _points[_selectedTargetPoint];
        SetInitialDirection();
        StartMovementTowardsNewTarget();
        InitializeSpriteDirection();
        base.Start();
    }

    protected override void StartRotation()
    {
        SetRotationDirection();
        base.StartRotation();
    }

    private void SetRotationDirection()
    {
        _currentRotateDirection = _rotationDirections[_selectedTargetPoint];
        if (_goesBackwards)
        {
            _currentRotateDirection = !_currentRotateDirection;
        }
        _rotationDirection = _currentRotateDirection ? Vector3.back : Vector3.forward;
    }

    protected override void FindTarget()
    {
        if (IsAtAFlipCorner())
        {
            FlipSprite();
        }

        //Si le scarab recule et qu'il est sur le point 0, on lui donne comme cible le point où il était juste avant
        //Sinon si le scarab avance et qu'il est sur le dernier point, on lui donne également comme cible le point où il était juste avant
        //Sinon, on lui donne le prochain point disponible dans les 2 cas restants
        _target = _goesBackwards ? _selectedTargetPoint == 0 ? _points[++_selectedTargetPoint] : _points[--_selectedTargetPoint] :
            _selectedTargetPoint == _points.Length - 1 ? _target = _points[--_selectedTargetPoint] : _target = _points[++_selectedTargetPoint];
    }

    protected override void InitializeSpriteDirection()
    {
        switch (_initialDirection)
        {
            case ScarabDirection.Down:
                transform.Rotate(Vector3.forward * MAX_ROTATION);
                break;
            case ScarabDirection.Up:
                transform.Rotate(Vector3.forward * -MAX_ROTATION);
                break;
            case ScarabDirection.Right:
                transform.Rotate(Vector3.forward * MAX_ROTATION * 2);
                break;
        }
    }

    protected override bool CanStartRotation()
    {
        return _canRotate && IsCloseToTarget() && !IsAtAFlipCorner();
    }
}
