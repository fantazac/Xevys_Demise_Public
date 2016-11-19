using UnityEngine;

public struct XevyMovementCommand
{
    public XevyMovement.XevyMovementStatus _status;
    public Vector2 _startPosition;
    public Vector2 _arrivalPosition;

    public XevyMovementCommand(Vector2 startPosition, Vector2 arrivalPosition, XevyMovement.XevyMovementStatus status)
    {
        _startPosition = startPosition;
        _arrivalPosition = arrivalPosition;
        _status = status;
    }
}