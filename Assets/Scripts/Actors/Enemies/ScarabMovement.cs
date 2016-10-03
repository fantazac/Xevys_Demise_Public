using UnityEngine;
using System.Collections;

public class ScarabMovement : MonoBehaviour
{

    [SerializeField]
    private Vector3[] _points;

    [SerializeField]
    private bool[] _rotationDirections;

    [SerializeField]
    private bool allowBacktracking = false;

    [SerializeField]
    private GameObject _attachedWall;

    private Vector3 _wallPosition;
    private Vector3 _wallScale;

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
        if (_attachedWall != null)
        {
            _points = new Vector3[4];

            _wallPosition = _attachedWall.GetComponent<Transform>().position;
            _wallScale = _attachedWall.GetComponent<Transform>().localScale;

            _points[0] = new Vector2(_wallPosition.x - _wallScale.x / 2 - transform.localScale.x / 2, _wallPosition.y - _wallScale.y / 2 - transform.localScale.y / 2);
            _points[1] = new Vector2(_wallPosition.x + _wallScale.x / 2 + transform.localScale.x / 2, _wallPosition.y - _wallScale.y / 2 - transform.localScale.y / 2);
            _points[2] = new Vector2(_wallPosition.x + _wallScale.x / 2 + transform.localScale.x / 2, _wallPosition.y + _wallScale.y / 2 + transform.localScale.y / 2);
            _points[3] = new Vector2(_wallPosition.x - _wallScale.x / 2 - transform.localScale.x / 2, _wallPosition.y + _wallScale.y / 2 + transform.localScale.y / 2);

            _currentPoint = Random.Range(0, _points.Length - 1);

            _target = _points[_currentPoint];
            transform.position = _points[_currentPoint];
            FindTarget();
            SetSpriteDirection();
        }
        else if (_points.Length > 0)
        {
            _currentPoint = Random.Range(0, _points.Length - 1);

            _target = _points[_currentPoint];
            transform.position = _points[_currentPoint];
            FindTarget();
            SetSpriteDirection();
        }

    }

    private void Update()
    {
        if(_attachedWall != null)
        {
            if (_target != transform.position)
            {
                transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), _target, 1 * Time.deltaTime);
                if (Vector3.Distance(_target, transform.position) <= transform.localScale.x / ROTATION_SPEED)
                    _rotate = true;
            }
            else
                FindTarget();

            if (_rotate)
                if (_rotateCount < MAX_ROTATION)
                {
                    _rotateCount += ROTATION_SPEED;
                    transform.Rotate(Vector3.forward * ROTATION_SPEED);
                }

                else
                {
                    _rotate = false;
                    _rotateCount = 0;
                }
        }
        else
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
                    if (_currentRotateDirection)
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
    }

    private void FindTarget()
    {
        if(_attachedWall != null)
        {
            if (_target == transform.position)
                _target = _points[(++_currentPoint) % _points.Length];
        }
        else
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

    public void SetSpriteDirection()
    {
        if (_target.x == transform.position.x)
        {
            if (_target.y > transform.position.y)
            {
                GetComponent<SpriteRenderer>().flipY = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        else if (_target.y == transform.position.y)
        {
            if (_target.x > transform.position.x)
            {
                GetComponent<SpriteRenderer>().flipY = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }
}
