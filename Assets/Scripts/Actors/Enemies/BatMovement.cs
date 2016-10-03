﻿using UnityEngine;
using System.Collections;

public class BatMovement : MonoBehaviour
{

    [SerializeField]
    private GameObject _playerDetectionHitbox;

    private bool _isInPosition = true;
    private bool _startCooldown = false;
    private bool _goingUp = false;
    private bool _goingDown = false;

    private const int COOLDOWN_BEFORE_GOING_UP = 35;
    private int _cooldownCount = 0;

    private Vector2 _target;
    private Vector2 _initialPosition;

    private float _minX = 0;
    private float _maxX = 0;

    [SerializeField]
    private float _leftDistance = 0;

    [SerializeField]
    private float _rightDistance = 0;

    private const float DOWN_SPEED = 5;
    private const float UP_SPEED = 2;

    private void Start()
    {
        _initialPosition = transform.position;
        _minX = _initialPosition.x - _leftDistance;
        _maxX = _initialPosition.x + _rightDistance;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if ((collider.gameObject.tag == "Wall" || collider.gameObject.tag == "Player" || collider.gameObject.tag == "FlyingPlatform") && !_goingUp)
        {
            _startCooldown = true;
            _goingUp = true;
        }
    }

    private void Update()
    {
        _playerDetectionHitbox.transform.position = new Vector2(transform.position.x, _playerDetectionHitbox.transform.position.y);

        if (!_goingDown && _isInPosition && _playerDetectionHitbox.GetComponent<DetectPlayer>().DetectedPlayer)
            _goingDown = true;

        if (!_isInPosition && !_goingDown && !_startCooldown)
        {
            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), _target, UP_SPEED * Time.deltaTime);
            if (transform.position.y == _target.y)
            {
                _isInPosition = true;
                _goingUp = false;
            }
        }
        else if (!_startCooldown && _goingDown)
        {
            if (_isInPosition)
                _isInPosition = false;

            transform.position = new Vector2(transform.position.x, transform.position.y - (DOWN_SPEED * Time.deltaTime));
        }

        if (_startCooldown && _cooldownCount < COOLDOWN_BEFORE_GOING_UP)
            _cooldownCount++;
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
        _target = new Vector2(Random.Range(_minX, _maxX), _initialPosition.y);
    }

}
