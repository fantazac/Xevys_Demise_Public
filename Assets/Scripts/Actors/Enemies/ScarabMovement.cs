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

    [SerializeField]
    private ScarabDirection[] _initialDirections;

    private const float MAX_ROTATION = 90;
    private const float ROTATION_SPEED = 5;

    private Vector3 _wallPosition;
    private Vector3 _wallScale;

    private bool goesBackwards = false;
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

            _currentPoint = Random.Range(0, _points.Length);

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
        if (_attachedWall != null)
        {
            RectangularMovementUpdate();
        }
        else
        {
            IrregularMovementUpdate();
        }
    }

    private void RectangularMovementUpdate()
    {
        if (_target != transform.position)
        {
            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), _target, 1 * Time.deltaTime);
            if (Vector3.Distance(_target, transform.position) <= transform.localScale.x / ROTATION_SPEED)
            {
                _rotate = true;
            }
        }
        else
        {
            FindTarget();
        }

        if (_rotate)
        {
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
    }

    private void IrregularMovementUpdate()
    {
        if (_target != transform.position)
        {
            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), _target, 1 * Time.deltaTime);
            if (Vector3.Distance(_target, transform.position) <= transform.localScale.x / ROTATION_SPEED && _currentPoint != 0 && _currentPoint != _points.Length - 1)
            {
                _rotate = true;
                _currentRotateDirection = _rotationDirections[_currentPoint];
                if (!goesBackwards)
                {
                    _currentRotateDirection = !_currentRotateDirection;
                }
            }
        }
        else
        {
            FindTarget();
        }

        if (_rotate)
        {
            if (_rotateCount < MAX_ROTATION)
            {
                _rotateCount += ROTATION_SPEED;
                if (_currentRotateDirection)
                {
                    transform.Rotate(Vector3.forward * ROTATION_SPEED);
                }
                else
                {
                    transform.Rotate(Vector3.back * ROTATION_SPEED);
                }
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
        if (_target == transform.position)
        {
            if (allowBacktracking)
            {
                if (goesBackwards)
                {
                    if (_currentPoint == 0)
                    {
                        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
                        goesBackwards = false;
                        _target = _points[++_currentPoint];
                    }
                    else
                    {
                        _target = _points[--_currentPoint];
                    }

                }
                else
                {
                    if (_currentPoint == _points.Length - 1)
                    {
                        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
                        goesBackwards = true;
                        _target = _points[--_currentPoint];
                    }
                    else
                    {
                        _target = _points[++_currentPoint];
                    }
                }
            }
            else
            {
                _target = _points[(++_currentPoint) % _points.Length];
            }
        }
    }

    public void SetSpriteDirection()
    {
        if (_attachedWall != null)
        {
            transform.Rotate(Vector3.forward * 90 * (_currentPoint + 1));
        }
        else
        {
            if (_initialDirections[_currentPoint] == ScarabDirection.Down)
            {
                transform.Rotate(Vector3.forward * 90);
            }
            else if (_initialDirections[_currentPoint] == ScarabDirection.Up)
            {
                transform.Rotate(Vector3.forward * -90);
            }
            else if (_initialDirections[_currentPoint] == ScarabDirection.Right)
            {
                transform.Rotate(Vector3.forward * 180);
            }
        }
    }

    public bool OnBottomOfPlatform()
    {
        return transform.rotation.eulerAngles.z > 170 || transform.rotation.eulerAngles.z < -170;
    }
}

public enum ScarabDirection
{
    Up,
    Left,
    Down,
    Right,
    Empty,
}
