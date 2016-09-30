using UnityEngine;
using System.Collections;

public class AttachCustomScarabToPlatform : MonoBehaviour
{

    [SerializeField]
    private Vector3[] _points;

    [SerializeField]
    private bool[] _rotationDirections;

    [SerializeField]
    private bool allowBacktracking = false;

    private bool goesBackwards = false;

    private const float MAX_ROTATION = 90;
    private const float ROTATION_SPEED = 5;

    private bool _rotate = false;
    private bool _currentRotateDirection = false;
    private float _rotateCount = 0;

    private int _currentPoint;

    private Vector3 _target;


    private void Start()
    {
        if (_points.Length > 0)
        {
            _currentPoint = Random.Range(0, _points.Length - 1);

            _target = _points[_currentPoint];
            transform.position = _points[_currentPoint];
            FindTarget();
        }

    }

    private void Update()
    {
        if (_target != transform.position)
        {
            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), _target, 1 * Time.deltaTime);
            if (Vector3.Distance(_target, transform.position) <= transform.localScale.x / ROTATION_SPEED && _currentPoint != 0 && _currentPoint != _points.Length - 1)
            {
                _rotate = true;
                _currentRotateDirection = _rotationDirections[_currentPoint];
                if (!goesBackwards)
                    _currentRotateDirection = !_currentRotateDirection;
            }
                
        }
        else
            FindTarget();

        if (_rotate)
            if (_rotateCount < MAX_ROTATION)
            {
                _rotateCount += ROTATION_SPEED;
                if(_currentRotateDirection)
                    transform.Rotate(Vector3.forward * ROTATION_SPEED);
                else
                    transform.Rotate(Vector3.back * ROTATION_SPEED);
            }

            else
            {
                _rotate = false;
                _rotateCount = 0;
            }
    }

    private void FindTarget()
    {
        if (_target == transform.position)
            if (allowBacktracking)
                if (goesBackwards)
                {
                    _target = _points[--_currentPoint];
                    if (_currentPoint == 0)
                        goesBackwards = false;
                }
                else
                {
                    _target = _points[++_currentPoint];
                    if (_currentPoint == _points.Length - 1)
                        goesBackwards = true;
                }
            else
                _target = _points[(++_currentPoint) % _points.Length];
    }
}
