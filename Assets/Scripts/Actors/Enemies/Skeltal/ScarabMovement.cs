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

    protected virtual void Start()
    {
        GetComponent<Health>().OnDeath += StopMovementOnDeath;
    }

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

    protected IEnumerator MoveTowardsTarget()
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

    protected virtual void StartRotation() { }
    protected virtual void InitializeSpriteDirection() { }
    protected virtual bool CanStartRotation() { return _canRotate; }
    protected virtual void FindTarget() { }

    private void StopMovementOnDeath()
    {
        StopAllCoroutines();
        enabled = false;
    }

    public bool IsNotOnTopOfPlatform()
    {
        return transform.rotation.eulerAngles.z != 0;
    }
}
