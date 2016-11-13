﻿using UnityEngine;
using System.Collections;

public class KnockbackOnDamageTaken : MonoBehaviour
{
    private const float KNOCKBACK_SPEED = 5;
    private const float TIME_DAMAGE_ANIMATION_PLAYS = 0.25f;

    private WaitForSeconds _damageAnimDelay;

    private Animator _anim;
    private PlayerGroundMovement _playerGroundMovement;

    //Sortir l'anim de ce component
    private void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        _playerGroundMovement = GetComponent<PlayerGroundMovement>();

        _damageAnimDelay = new WaitForSeconds(TIME_DAMAGE_ANIMATION_PLAYS);
    }

    public void KnockbackPlayer(Vector2 positionEnemy)
    {
        _anim.SetBool("IsDamaged", true);
        StartCoroutine("StopDamageAnimation");
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
    }

    private IEnumerator StopDamageAnimation()
    {
        yield return _damageAnimDelay;
        
        _anim.SetBool("IsDamaged", false);
    }
}
