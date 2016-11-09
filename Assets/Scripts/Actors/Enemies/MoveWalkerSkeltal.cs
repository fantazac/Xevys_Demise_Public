using UnityEngine;
using System.Collections;

public class MoveWalkerSkeltal : SkeltalBehaviour
{
    [SerializeField]
    protected float _leftDistance = 0;

    [SerializeField]
    protected float _rightDistance = 0;

    [SerializeField]
    private float _unitsPerSecond = 2f;

    protected override IEnumerator SkeltalMovement()
    {
        while (!IsOnAnEdge())
        {
            transform.position = new Vector3(transform.position.x +
                (_flipSkeltal.IsFacingRight ? _unitsPerSecond * Time.deltaTime : -_unitsPerSecond * Time.deltaTime),
                transform.position.y, transform.position.z);

            yield return null;
        }
        SkeltalMovementFinished();
    }

    private bool IsOnAnEdge()
    {
        return IsOnRightEdge() || IsOnLeftEdge();
    }

    private bool IsOnRightEdge()
    {
        return _flipSkeltal.IsFacingRight && transform.position.x >= _initialPosition.x + _rightDistance;
    }

    private bool IsOnLeftEdge()
    {
        return !_flipSkeltal.IsFacingRight && transform.position.x <= _initialPosition.x - _leftDistance;
    }
}
