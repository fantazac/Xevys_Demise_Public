using UnityEngine;
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
    //public XevyLastAttack LastAttack { get; set; }
    private float _statusTimer = IDLE_STATUS_COOLDOWN;
    private float _sameAttackCount;

    private void Start()
    {
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
        if (_status == XevyStatus.IDLE)
        {
            UpdateWhenIdle();
        }     
        else if (_status == XevyStatus.BLOCKING)
        {
            UpdateWhenBlocking();
        }
        else if (_status == XevyStatus.HEALING)
        {
            UpdateWhenHealing();
        }    
        else if (_status == XevyStatus.VULNERABLE)
        {
            UpdateWhenVulnerable();
        }
    }

    private void UpdateWhenIdle()
    {
        if (_projectileInteraction.CheckKnivesDistance() || _projectileInteraction.CheckAxesDistance())
        {
            _action.Block();
            _status = XevyStatus.BLOCKING;
        }
        else
        {
            bool playerProximity = _playerInteraction.CheckPlayerDistance();
            bool healthStatus = (_health.HealthPoint > _health.MaxHealth / 3);
            _status = XevyStatus.VULNERABLE;
            if (playerProximity && healthStatus)
            {
                _action.EarthAttack();
                //_movement.StepBack();
                //_movement.Bounce();
            }
            else if (!playerProximity && healthStatus)
            {
                //_action.FireAttack(_playerInteraction.GetPlayerHorizontalDistance(), _playerInteraction.GetPlayerVerticalDistance());
                _playerInteraction.IsFocusedOnPlayer = false;
                //_movement.Bounce();
                //_action.RangedAttack(_playerInteraction.CheckAlignmentWithPlayer());
            }
            else if (playerProximity && !healthStatus)
            {
                _playerInteraction.IsFocusedOnPlayer = false;
                _movement.Flee();
            }
            else
            {
                _status = XevyStatus.HEALING;
            }
        }
    }

    private void UpdateWhenBlocking()
    {
        if (!_projectileInteraction.CheckKnivesDistance() && !_projectileInteraction.CheckAxesDistance())
        {
            _action.LowerGuard();
            SetIdleStatus();
        }
    }

    private void UpdateWhenHealing()
    {
        _action.Heal();
        if (!_projectileInteraction.CheckKnivesDistance() || !_projectileInteraction.CheckAxesDistance())
        {
            if (_playerInteraction.CheckPlayerDistance())
            {
                _status = XevyStatus.VULNERABLE;
            }
        }
    }

    private void UpdateWhenVulnerable()
    {
        if (_movement.MovementStatus == XevyMovement.XevyMovementStatus.NONE)
        {
            if (_statusTimer <= 0)
            {
                _statusTimer = VULNERABLE_STATUS_COOLDOWN;
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
        _playerInteraction.IsFocusedOnPlayer = true;
        _statusTimer = IDLE_STATUS_COOLDOWN;
        _status = XevyStatus.IDLE;
    }
}
