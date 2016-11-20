﻿using UnityEngine;
using System.Collections;

/*
 * BEN_REVIEW
 * 
 * OK.
 */
public class KnockbackOnDamageTaken : MonoBehaviour
{
    private const float KNOCKBACK_SPEED = 5;
    private const float TIME_DAMAGE_ANIMATION_PLAYS = 0.25f;

    private WaitForSeconds _damageAnimDelay;

    private PlayerGroundMovement _playerGroundMovement;

    public delegate void OnKnockbackStartedHandler();
    public event OnKnockbackStartedHandler OnKnockbackStarted;

    public delegate void OnKnockbackFinishedHandler();
    public event OnKnockbackFinishedHandler OnKnockbackFinished;

    private void Start()
    {
        _playerGroundMovement = GetComponent<PlayerGroundMovement>();

        _damageAnimDelay = new WaitForSeconds(TIME_DAMAGE_ANIMATION_PLAYS);

        OnKnockbackFinished += PlayerState.SetImmobile;
    }

    public void KnockbackPlayer(Vector2 positionEnemy)
    {
        OnKnockbackStarted();
        _playerGroundMovement.IsKnockedBack = true;

        if (transform.position.x < positionEnemy.x)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-KNOCKBACK_SPEED, GetComponent<Rigidbody2D>().velocity.y);
        }
        else if (transform.position.x > positionEnemy.x)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(KNOCKBACK_SPEED, GetComponent<Rigidbody2D>().velocity.y);
        }
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, KNOCKBACK_SPEED);

        StartCoroutine("StopDamageAnimation");
    }

    private IEnumerator StopDamageAnimation()
    {
        yield return _damageAnimDelay;

        OnKnockbackFinished();
    }
}
