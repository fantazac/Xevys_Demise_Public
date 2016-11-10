﻿using UnityEngine;
using System.Collections;

public class XevyAction: MonoBehaviour
{
    private int _sameAttackCount;
    private bool _isPlayerStillOnSameLine;

    [SerializeField]
    GameObject _airSpike;
    [SerializeField]
    GameObject _fireBall;
    [SerializeField]
    GameObject _earthThorns;
    BoxCollider2D _clawHitbox; //Check with Alex if it has changed (GameObject or BoxCollider2D)
    PolygonCollider2D _xevyHitbox;


	private void Start()
    {
        _isPlayerStillOnSameLine = true;
	}

    public void Block()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        //_clawHitbox.enabled = false;
        //_xevyHitbox.enabled = false;
    }

    public void LowerGuard()
    {
        //_xevyHitbox.enabled = true;
        GetComponent<SpriteRenderer>().color = Color.green;
    }

    public void Heal()
    {
        GetComponent<Health>().Heal(0);
    }

    public void RangedAttack(bool isPlayerOnSameLine)
    {
        if (_isPlayerStillOnSameLine != isPlayerOnSameLine)//Double check
        {
            _sameAttackCount = 0;
        }
        if (isPlayerOnSameLine)
        {
            AirAttack();
        }
        else
        {
            FireAttack();
        }
        _sameAttackCount++;
    }

    public void MeleeAttack()
    {
        EarthAttack();
        ClawAttack();
        //_clawHitbox.enabled = true;
    }

    private void FireAttack()
    {
        //(GameObject)Instantiate(_fireBall, initialPosition, transform.rotation);
    }

    private void AirAttack()
    {
        //(GameObject)Instantiate(_airSpike, initialPosition, transform.rotation);
    }

    private void EarthAttack()
    {
        //(GameObject)Instantiate(_earthThorns, initialPosition, transform.rotation);
    }

    private void ClawAttack()
    {
        _clawHitbox.enabled = true;
        //Flee
    }
}