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

        _initialPosition = new Vector3(transform.position.x + _rightDistance, transform.position.y, transform.position.z);
        _initialPositionLeft = new Vector3(transform.position.x - _leftDistance, transform.position.y, transform.position.z);

        base.Start();
    }

    protected override void StartSkeltalMovement()
    {
        _startingPosition = _skeltalOrientation.IsFacingRight ? _initialPositionLeft : _initialPosition;
        _directionFactor = _skeltalOrientation.IsFacingRight ? 1 : -1;

        StartCoroutine("SkeltalMovement");
    }

    protected override IEnumerator SkeltalMovement()
    {
        while (_timeInAirCount < _timeInAir)
        {
            _newXPosition = _startingPosition.x + (_horizontalSpeed * _timeInAirCount * _directionFactor);

            //formule de physique pour calculer la hauteur à laquelle est rendu l'objet par rapport au temps, l'accélération verticale et la vitesse initiale
            _newYPosition = _startingPosition.y + (_initialVerticalSpeed * _timeInAirCount) +
                (HALF_VALUE * _verticalAcceleration * _timeInAirCount * _timeInAirCount);

            transform.position = new Vector3(_newXPosition, _newYPosition, transform.position.z);
            _timeInAirCount += Time.deltaTime;

            yield return null;
        }
        SkeltalMovementFinished();
    }

    protected override void SkeltalMovementFinished()
    {
        transform.position = _skeltalOrientation.IsFacingRight ? _initialPosition : _initialPositionLeft;
        _timeInAirCount = 0;
        base.SkeltalMovementFinished();
    }
}
