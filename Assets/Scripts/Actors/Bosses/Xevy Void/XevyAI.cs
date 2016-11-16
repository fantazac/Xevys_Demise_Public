﻿using UnityEngine;
using System.Collections;

public class XevyAI : MonoBehaviour
{
    public enum XevyStatus
    {
        IDLE,
        VULNERABLE,
        BLOCKING,
        HEALING,
        DEAD,
    }

    //Input more cooldowns for each attack
    private const float VULNERABLE_STATUS_COOLDOWN = 1.0f;
    private const float IDLE_STATUS_COOLDOWN = 1.0f;

    private Health _health;
    private XevyAction _action;
    private XevyMovement _movement;
    private XevyPlayerInteraction _playerInteraction;
    private XevyProjectileInteraction _projectileInteraction;

    private XevyStatus _status;
    private XevyAction.XevyAttackType _lastAttack;
    private XevyAction.XevyAttackType _currentAttack;
    private float _statusTimer;
    private float _sameAttackCount;

    private void Start()
    {
        _sameAttackCount = 0;
        _currentAttack = XevyAction.XevyAttackType.NONE;
        _lastAttack = XevyAction.XevyAttackType.NONE;
        _status = XevyStatus.IDLE;
        _health = GetComponent<Health>();
        _action = GetComponent<XevyAction>();
        _movement = GetComponent<XevyMovement>();
        _playerInteraction = GetComponent<XevyPlayerInteraction>();
        _projectileInteraction = GetComponent<XevyProjectileInteraction>();
    }

    private void FixedUpdate()
    {
        _movement.MovementUpdate();
        switch (_status)
        {
            case XevyStatus.IDLE:
                UpdateWhenIdle();
                break;
            case XevyStatus.VULNERABLE:
                UpdateWhenVulnerable();
                break;
            case XevyStatus.BLOCKING:
                UpdateWhenBlocking();
                break;
            case XevyStatus.HEALING:
                UpdateWhenHealing();
                break;
        }
    }

    private void UpdateWhenIdle()
    {
        if (_statusTimer > 0)
        {
            if (_projectileInteraction.CheckKnivesDistance() || _projectileInteraction.CheckAxesDistance())
            {
                SetBlockingStatus();
            }
            else
            {
                _statusTimer -= Time.fixedDeltaTime;
            }
        }
        else
        {
            _action.RetreatClaws();
            bool playerProximity = _playerInteraction.CheckPlayerDistance();
            bool healthStatus = (_health.HealthPoint > _health.MaxHealth / 3);
            SetVulnerableStatus();
            if (playerProximity && !healthStatus)
            {
                _movement.FleeTowardsRandomPoint();
            }
            else if (!playerProximity && !healthStatus)
            {
                _status = XevyStatus.HEALING;
            }
            else if (playerProximity && healthStatus && _playerInteraction.CheckAlignmentWithPlayer())
            {
                switch (_lastAttack)
                {
                    case XevyAction.XevyAttackType.EARTH:
                        _currentAttack = _action.NeutralAttack();
                        _movement.ChargeForward();
                        break;
                    case XevyAction.XevyAttackType.NEUTRAL:
                        _playerInteraction.IsFocusedOnPlayer = true;
                        _currentAttack = _action.Block();
                        _movement.StepBack();
                        break;
                    default:
                        _currentAttack = _action.EarthAttack();
                        break;
                }
                _lastAttack = _currentAttack;
            }
            else
            {             
                if (_playerInteraction.CheckAlignmentWithPlayer())
                {
                    _currentAttack = _action.AirAttack();
                }
                else
                {
                    _currentAttack = _action.FireAttack(_playerInteraction.GetPlayerHorizontalDistance(), _playerInteraction.GetPlayerVerticalDistance());
                }
                _sameAttackCount = (_currentAttack == _lastAttack ? _sameAttackCount + 1 : 0);
                _lastAttack = _currentAttack;

                if (_sameAttackCount == 2)
                {
                    _movement.BounceTowardsRandomPoint();
                    _sameAttackCount = 0;
                }
            }
        }
    }

    private void UpdateWhenBlocking()
    {
        if (!_projectileInteraction.CheckKnivesDistance() && !_projectileInteraction.CheckAxesDistance())
        {
            SetIdleStatus();
        }
    }

    private void UpdateWhenHealing()
    {
        _action.Heal();
        if ((!_projectileInteraction.CheckKnivesDistance() && !_projectileInteraction.CheckAxesDistance()) || _playerInteraction.CheckPlayerDistance())
        {
            SetVulnerableStatus();
        }
    }

    private void UpdateWhenVulnerable()
    {
        if (_movement.MovementStatus == XevyMovement.XevyMovementStatus.NONE)
        {
            if (_statusTimer <= 0)
            {
                SetIdleStatus();
            }
            else
            {
                _statusTimer -= Time.fixedDeltaTime;
            }
        }
    }

    private void SetIdleStatus()
    {
        GetComponent<SpriteRenderer>().color = Color.yellow;
        _statusTimer = IDLE_STATUS_COOLDOWN;
        _action.LowerGuard();
        _playerInteraction.IsFocusedOnPlayer = true;
        _status = XevyStatus.IDLE;
    }

    private void SetVulnerableStatus()
    {
        GetComponent<SpriteRenderer>().color = Color.cyan;
        _statusTimer = VULNERABLE_STATUS_COOLDOWN;
        _playerInteraction.IsFocusedOnPlayer = false;
        _status = XevyStatus.VULNERABLE;
    }

    private void SetBlockingStatus()
    {
        _action.Block();
        _playerInteraction.IsFocusedOnPlayer = true;
        _status = XevyStatus.BLOCKING;
    }

    public void OnHit()
    {
        SetIdleStatus();
    }
}
