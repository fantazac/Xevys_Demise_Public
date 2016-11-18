﻿using UnityEngine;
using System.Collections;

public class FlipBoss : MonoBehaviour {

    [SerializeField]
    private bool _isFacingLeft;
    GameObject player;

    public bool IsFacingLeft { get { return _isFacingLeft; } }
    public int Orientation { get { return (_isFacingLeft ? -1 : 1); } }

    private void Start()
    {
        player = StaticObjects.GetPlayer();
    }

    public void FlipTowardsSpecificPoint(Vector2 point)
    {
        if (point.x > transform.position.x)
        {
            if (_isFacingLeft)
            {
                Flip();
            }
        }
        else if (point.x < transform.position.x)
        {
            if (!_isFacingLeft)
            {
                Flip();
            }
        }
    }

    public void FlipTowardsPlayer()
    {
        if (player.transform.position.x > transform.position.x)
        {
            if (_isFacingLeft)
            {
                Flip();
            }
        }
        else if (player.transform.position.x < transform.position.x)
        {
            if (!_isFacingLeft)
            {
                Flip();
            }
        }
    }

    private void Flip()
    {
        _isFacingLeft = !_isFacingLeft;
        transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
    }
}