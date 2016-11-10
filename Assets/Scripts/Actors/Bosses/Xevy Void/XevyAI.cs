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

    public enum XevyLastAttack
    {

    }

    //Input more cooldowns for each attack
    private const float VULNERABLE_STATUS_COOLDOWN = 1.0f;

    private Health _health;
    private XevyAction _action;
    private XevyMovement _movement;
    private XevyPlayerInteraction _playerInteraction;
    private XevyProjectileInteraction _projectileInteraction;

    private XevyStatus _status;
    public XevyLastAttack LastAttack { get; set; }
    private float _vulnerableStatusTimer;
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
        if (_status == XevyStatus.IDLE)
        {
            _playerInteraction.IsFocusedOnPlayer = true;
            UpdateWhenIdle();
        }
        else if (_status == XevyStatus.BLOCKING)
        {
            _playerInteraction.IsFocusedOnPlayer = true;
            UpdateWhenBlocking();
        }
        else if (_status == XevyStatus.HEALING)
        {
            _playerInteraction.IsFocusedOnPlayer = false;
            UpdateWhenHealing();
        }
        else if (_status == XevyStatus.VULNERABLE)
        {
            _playerInteraction.IsFocusedOnPlayer = false;
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
            bool playerProximity = _playerInteraction.CheckPlayerProximity();
            bool healthStatus = (_health.HealthPoint > _health.MaxHealth / 3);
            _status = XevyStatus.VULNERABLE;
            if (playerProximity && healthStatus)
            {
                _action.MeleeAttack();
            }
            else if (!playerProximity && healthStatus)
            {
                _action.RangedAttack(_playerInteraction.CheckAlignmentWithPlayer());
            }
            else if (playerProximity && !healthStatus)
            {
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
            _status = XevyStatus.IDLE;
        }
    }

    private void UpdateWhenHealing()
    {
        _action.Heal();
        if (!_projectileInteraction.CheckKnivesDistance() || !_projectileInteraction.CheckAxesDistance())
        {
            if (_playerInteraction.CheckPlayerProximity())
            {
                _status = XevyStatus.VULNERABLE;
            }
        }
    }

    private void UpdateWhenVulnerable()
    {
        if (_vulnerableStatusTimer <= 0)
        {
            _vulnerableStatusTimer = VULNERABLE_STATUS_COOLDOWN;
            _status = XevyStatus.IDLE;
        }
        else
        {
            _vulnerableStatusTimer -= Time.fixedDeltaTime;
        }
    }
}
