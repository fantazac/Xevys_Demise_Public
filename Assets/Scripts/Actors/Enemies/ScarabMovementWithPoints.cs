using UnityEngine;
using System.Collections;

/*
 * BEN_REVIEW
 * 
 * Pour ScarabMovement et ses dérivés, penser utiliser le patern "Template Method".
 * 
 * Voir http://www.oodesign.com/template-method-pattern.html
 */
public class ScarabMovementWithPoints : ScarabMovement
{
    [SerializeField]
    private Vector3[] _targetPoints;

    [SerializeField]
    private bool[] _rotationDirections;

    private ScarabDirection _initialDirection;

    private bool _goesBackwards = false;
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
    }

    private void SetInitialDirection()
    {
        if (_points[_selectedTargetPoint].x < _points[_selectedTargetPoint + 1].x)
        {
            _initialDirection = ScarabDirection.Right;
        }
        else if (_points[_selectedTargetPoint].x > _points[_selectedTargetPoint + 1].x)
        {
            _initialDirection = ScarabDirection.Left;
        }
        else if (_points[_selectedTargetPoint].y < _points[_selectedTargetPoint + 1].y)
        {
            _initialDirection = ScarabDirection.Up;
        }
        else
        {
            _initialDirection = ScarabDirection.Down;
        }
    }

    protected override void StartRotation()
    {
        _canRotate = false;
        SetRotationDirection();
        StartCoroutine("Rotate");
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

    protected override IEnumerator MoveTowardsTarget()
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

    private void FlipSprite()
    {
        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        _goesBackwards = !_goesBackwards;
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

    private bool IsAtAFlipCorner()
    {
        return _selectedTargetPoint == 0 || _selectedTargetPoint == _points.Length - 1;
    }
}
