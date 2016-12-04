using UnityEngine;
using System.Collections;

public class MoveBouncySkeltal : SkeltalBehaviour
{
    [SerializeField]
    private float _leftDistance = 0;

    [SerializeField]
    private float _rightDistance = 0;

    [SerializeField]
    private float _maximumHeightFromGround = 2.5f;

    [SerializeField]
    private float _timeInAir = 1.5f;

    private float _timeInAirCount = 0;
    private float _newXPosition = 0;
    private float _newYPosition = 0;

    private float _horizontalSpeed = 0;
    private float _initialVerticalSpeed = 0;
    private float _verticalAcceleration = 0;

    private Vector3 _initialPositionLeft;
    private Vector3 _startingPosition;

    private int _directionFactor = 0;

    private float _halfOfTimeInAir = 0;

    private const float HALF_VALUE = 0.5f;

    protected override void Start()
    {
        _halfOfTimeInAir = _timeInAir * HALF_VALUE;

        //formule pour trouver l'accélération verticale par rapport au temps et à la hauteur maximale
        _verticalAcceleration = -_maximumHeightFromGround / (HALF_VALUE * _halfOfTimeInAir * _halfOfTimeInAir);

        //formule pour trouver la vitesse initiale verticale par rapport à l'accélération verticale et au temps
        _initialVerticalSpeed = -_verticalAcceleration * _halfOfTimeInAir;

        //formule pour trouver la vitesse horizontale par rapport au temps et à la distance
        _horizontalSpeed = (_leftDistance + _rightDistance) / _timeInAir;

        _initialPosition = transform.position + (Vector3.right * _rightDistance);
        _initialPositionLeft = transform.position + (Vector3.left * _leftDistance);

        base.Start();
    }

    protected override void StartSkeltalMovement()
    {
        _startingPosition = _skeltalOrientation.IsFacingRight ? _initialPositionLeft : _initialPosition;
        _directionFactor = _skeltalOrientation.IsFacingRight ? 1 : -1;

        StartCoroutine(SkeltalMovement());
    }

    protected override IEnumerator SkeltalMovement()
    {
        while (_timeInAirCount < _timeInAir)
        {
            _newXPosition = _startingPosition.x + (_horizontalSpeed * _timeInAirCount * _directionFactor);

            _newYPosition = CalculateSkeltalVerticalPositionWhileInTheAir();

            transform.position += (Vector3.right * (_newXPosition - transform.position.x)) 
                + (Vector3.up * (_newYPosition - transform.position.y));
            _timeInAirCount += Time.deltaTime;

            yield return null;
        }
        SkeltalMovementFinished();
    }

    private float CalculateSkeltalVerticalPositionWhileInTheAir()
    {
        return _startingPosition.y + (_initialVerticalSpeed * _timeInAirCount) +
                (HALF_VALUE * _verticalAcceleration * _timeInAirCount * _timeInAirCount);
    }

    protected override void SkeltalMovementFinished()
    {
        transform.position = _skeltalOrientation.IsFacingRight ? _initialPosition : _initialPositionLeft;
        _timeInAirCount = 0;
        base.SkeltalMovementFinished();
    }
}
