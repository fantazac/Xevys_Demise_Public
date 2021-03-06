﻿using UnityEngine;
using System.Collections;

public class ScarabMovementWithPlatform : ScarabMovement
{

    [SerializeField]
    private GameObject _attachedWall;

    private Vector3 _wallPosition;
    private Vector3 _wallScale;

    private float _halfOfWallWidth;
    private float _halfOfWallHeight;
    private float _halfOfScarabHeight;

    protected override void Start()
    {
        InitializeDimensions();
        InitializePoints();

        _selectedTargetPoint = Random.Range(0, _points.Length);
        transform.position = _points[_selectedTargetPoint];
        _rotationDirection = Vector3.forward;

        StartMovementTowardsNewTarget();
        InitializeSpriteDirection();
        base.Start();
    }

    private void InitializeDimensions()
    {
        _wallPosition = _attachedWall.transform.position;
        _wallScale = _attachedWall.transform.localScale;

        _halfOfScarabWidth = transform.localScale.x * 0.5f;
        _halfOfScarabHeight = transform.localScale.y * 0.5f;

        _halfOfWallWidth = _wallScale.x * 0.5f;
        _halfOfWallHeight = _wallScale.y * 0.5f;
    }

    private void InitializePoints()
    {
        _points = new Vector3[4];

        _points[0] = new Vector3(_wallPosition.x - _halfOfWallWidth - _halfOfScarabWidth,
            _wallPosition.y - _halfOfWallHeight - _halfOfScarabHeight, transform.position.z);
        _points[1] = new Vector3(_wallPosition.x + _halfOfWallWidth + _halfOfScarabWidth,
            _wallPosition.y - _halfOfWallHeight - _halfOfScarabHeight, transform.position.z);
        _points[2] = new Vector3(_wallPosition.x + _halfOfWallWidth + _halfOfScarabWidth,
            _wallPosition.y + _halfOfWallHeight + _halfOfScarabHeight, transform.position.z);
        _points[3] = new Vector3(_wallPosition.x - _halfOfWallWidth - _halfOfScarabWidth,
            _wallPosition.y + _halfOfWallHeight + _halfOfScarabHeight, transform.position.z);
    }
}
