using UnityEngine;
using System.Collections;

public class ScarabMovement : MonoBehaviour
{

    protected Vector3[] _points;

    protected const float MAX_ROTATION = 90f;
    protected const float ROTATION_SPEED = 1.8f;
    protected const float MOVEMENT_SPEED = 1f;

    protected float _halfOfScarabWidth;

    protected bool _canRotate = true;
    protected float _rotateCount = 0;
    protected float _frameRotation = 0;

    protected int _selectedTargetPoint;

    protected Vector3 _rotationDirection;

    protected Vector3 _target;

    protected void StartMovementTowardsNewTarget()
    {
        FindTarget();
        StartCoroutine(MoveTowardsTarget());
    }

    protected void MovementFinished()
    {
        StartMovementTowardsNewTarget();
    }

    protected void RotationFinished()
    {
        _rotateCount = 0;
        _canRotate = true;
    }

    protected IEnumerator Rotate()
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

    protected bool IsCloseToTarget()
    {
        return Vector3.Distance(_target, transform.position) < _halfOfScarabWidth;
    }

    protected virtual void Start() { }
    protected virtual void StartRotation() { }
    protected virtual IEnumerator MoveTowardsTarget() { yield return null; }
    protected virtual void InitializeSpriteDirection() { }
    protected virtual bool CanStartRotation() { return _canRotate; }
    protected virtual void FindTarget() { }

    public bool IsNotOnTopOfPlatform()
    {
        return transform.rotation.eulerAngles.z != 0;
    }
}
