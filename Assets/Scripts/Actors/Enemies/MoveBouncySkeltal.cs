using UnityEngine;
using System.Collections;

public class MoveBouncySkeltal : SkeltalBehaviour
{
    [SerializeField]
    private float _leftDistance = 0;

    [SerializeField]
    private float _rightDistance = 0;

    [SerializeField]
    private float _leftEdgeHeight = 0;

    [SerializeField]
    private float _rightEdgeHeight = 0;

    [SerializeField]
    private float _maximumHeightFromGround = 0;

    [SerializeField]
    private float _timeInAir = 1.5f;

    [SerializeField]
    private float _unitsPerSecond = 2f;

    private float _newHeight;

    private float _initialVerticalSpeed = 0;
    private float _verticalAcceleration = 0;

    private const float HALF_VALUE = 0.5f;

    protected override void Start()
    {
        //formule pour trouver l'accélération verticale par rapport au temps et à la hauteur maximale
        //_verticalAcceleration = -_maximumHeightFromGround / (HALF_VALUE * _halfOfTimeInAir * _halfOfTimeInAir);

        base.Start();
    }

    private void InitializeArcVariables()
    {

    }

    protected override IEnumerator SkeltalMovement()
    {

        yield return null;
    }
}
