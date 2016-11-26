using UnityEngine;
using System.Collections;

public class MoveJumpySkeltal : SkeltalBehaviour
{
    [SerializeField]
    private float _maximumHeightFromGround = 2.5f;

    [SerializeField]
    private float _timeInAir = 1.5f;

    private float _timeInAirCount = 0;
    private float _newYPosition = 0;

    private float _initialVerticalSpeed = 0;
    private float _verticalAcceleration = 0;

    private float _halfOfTimeInAir = 0;

    private const float HALF_VALUE = 0.5f;

    protected override void Start()
    {
        _halfOfTimeInAir = _timeInAir * HALF_VALUE;

        //formule pour trouver l'accélération verticale par rapport au temps et à la hauteur maximale
        _verticalAcceleration = -_maximumHeightFromGround / (HALF_VALUE * _halfOfTimeInAir * _halfOfTimeInAir);

        //formule pour trouver la vitesse initiale verticale par rapport à l'accélération verticale et au temps
        _initialVerticalSpeed = -_verticalAcceleration * _halfOfTimeInAir;

        base.Start();
    }

    protected override IEnumerator SkeltalMovement()
    {
        while (_timeInAirCount < _timeInAir)
        {
            //formule de physique pour calculer la hauteur à laquelle est rendu l'objet par rapport au temps, l'accélération verticale et la vitesse initiale
            _newYPosition = _initialPosition.y + (_initialVerticalSpeed * _timeInAirCount) +
                (HALF_VALUE * _verticalAcceleration * _timeInAirCount * _timeInAirCount);

            transform.position += Vector3.up * (_newYPosition - transform.position.y);
            _timeInAirCount += Time.deltaTime;

            yield return null;
        }
        SkeltalMovementFinished();
    }

    protected override void SkeltalMovementFinished()
    {
        transform.position = _initialPosition;
        _timeInAirCount = 0;
        base.SkeltalMovementFinished();
    }
}
