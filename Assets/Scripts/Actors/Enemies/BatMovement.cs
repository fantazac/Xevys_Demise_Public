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

    private const float DOWN_SPEED = 6;
    private const float UP_SPEED = 3;
    private const int COOLDOWN_BEFORE_GOING_UP = 35;

    private bool _isInPosition = true;
    private bool _startCooldown = false;
    private bool _goingUp = false;
    private bool _goingDown = false;
    private int _cooldownCount = 0;

    private Vector3 _target;
    private Vector3 _initialPosition;

    private float _minX = 0;
    private float _maxX = 0;

    private void Start()
    {
        _initialPosition = transform.position;
        _minX = _initialPosition.x - _leftDistance;
        _maxX = _initialPosition.x + _rightDistance;
    }

    /*private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && !_goingUp)
        {
            _startCooldown = true;
            _goingUp = true;
        }
    }*/

    private void Update()
    {
        if (transform.position.y < _lowestY)
        {
            transform.position = new Vector3(transform.position.x, _lowestY, transform.position.z);
            _startCooldown = true;
            _goingUp = true;
        }

        _playerDetectionHitbox.transform.position = new Vector3(transform.position.x, _playerDetectionHitbox.transform.position.y, transform.position.z);

        if (!_goingDown && _isInPosition && _playerDetectionHitbox.GetComponent<DetectPlayer>().DetectedPlayer)
        {
            _goingDown = true;
        }

        if (!_isInPosition && !_goingDown && !_startCooldown)
        {
            transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, transform.position.z), _target, UP_SPEED * Time.deltaTime);
            if (transform.position.y == _target.y)
            {
                _isInPosition = true;
                _goingUp = false;
            }
        }
        else if (!_startCooldown && _goingDown)
        {
            if (_isInPosition)
            {
                _isInPosition = false;
            }

            transform.position = new Vector3(transform.position.x, transform.position.y - (DOWN_SPEED * Time.deltaTime), transform.position.z);
        }

        if (_startCooldown && _cooldownCount < COOLDOWN_BEFORE_GOING_UP)
        {
            _cooldownCount++;
        }            
        else if (_startCooldown)
        {
            _startCooldown = false;
            _cooldownCount = 0;
            _goingDown = false;
            FindTarget();
        }
    }

    private void FindTarget()
    {
        _target = new Vector3(Random.Range(_minX, _maxX), _initialPosition.y, _initialPosition.z);
    }

}
