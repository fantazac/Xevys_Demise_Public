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

    private Health _health;
    private XevyAction _action;
    private XevyMovement _movement;
    private XevyPlayerInteraction _playerInteraction;
    private XevyProjectileInteraction _projectileInteraction;

    private XevyStatus _status;
    private float _blockingTimeLeft;

    private void Start()
    {
        GetComponent<SpriteRenderer>().color = Color.blue;
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
            UpdateWhenIdle();
        }
        else if (_status == XevyStatus.BLOCKING)
        {
            UpdateWhenBlocking();
        }
	}

    private void UpdateWhenIdle()
    {
        if (_projectileInteraction.CheckKnivesDistance() || _projectileInteraction.CheckAxesDistance())
        {
            _action.Block();
        }
        else
        {
            bool playerProximity = _playerInteraction.CheckPlayerProximity();
            bool healthStatus = (_health.HealthPoint > _health.MaxHealth / 3);
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
                _action.Heal();
            }
        }
    }

    private void UpdateWhenBlocking()
    {
        if (!_projectileInteraction.CheckKnivesDistance() && !_projectileInteraction.CheckAxesDistance())
        {
            _action.LowerGuard();
        }
    }

    private void UpdateWhenHealing()
    {
        _action.Heal();
        if (!_projectileInteraction.CheckKnivesDistance() || !_projectileInteraction.CheckAxesDistance())
        {
            if (_playerInteraction.CheckPlayerProximity())
            {
                //Stop Healing
            }
        }
    }
}
