﻿using UnityEngine;
using System.Collections;

public class KnockbackOnDamageTaken : MonoBehaviour
{
    private const float KNOCKBACK_SPEED = 5;

    public void KnockbackPlayer(Vector2 positionEnemy)
    {
        GetComponent<PlayerMovement>().IsKnockedBack = true;

        if (transform.position.x < positionEnemy.x)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-KNOCKBACK_SPEED, GetComponent<Rigidbody2D>().velocity.y);
        }
        else if (transform.position.x > positionEnemy.x)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(KNOCKBACK_SPEED, GetComponent<Rigidbody2D>().velocity.y);
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, KNOCKBACK_SPEED);
    }

}