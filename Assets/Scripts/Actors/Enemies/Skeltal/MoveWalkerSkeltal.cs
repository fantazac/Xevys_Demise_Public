﻿using UnityEngine;
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
            transform.position += Vector3.right * (_skeltalOrientation.IsFacingRight ? _unitsPerSecond : -_unitsPerSecond) * Time.deltaTime;

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
        return _skeltalOrientation.IsFacingRight && transform.position.x >= _initialPosition.x + _rightDistance;
    }

    private bool IsOnLeftEdge()
    {
        return !_skeltalOrientation.IsFacingRight && transform.position.x <= _initialPosition.x - _leftDistance;
    }
}
