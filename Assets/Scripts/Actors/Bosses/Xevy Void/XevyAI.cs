using UnityEngine;

public class XevyAI : MonoBehaviour
{
    private enum XevyStatus
    {
        IDLE,
        VULNERABLE,
        BLOCKING,
        HEALING,
        DEAD,
    }

    [SerializeField]
    private int _numberSameAttacksBeforeMovement = 5;

    [SerializeField]
    private float _idleStatusCooldown = 0.5f;

    [SerializeField]
    private float _vulnerableStatusCooldown = 0.5f;

    [SerializeField]
    private float _criticalHealthPercentage = 0.33f;

    private Health _health;
    private Animator _animator;
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
        _animator = GetComponent<Animator>();
        _action = GetComponent<XevyAction>();
        _movement = GetComponent<XevyMovement>();
        _playerInteraction = GetComponent<XevyPlayerInteraction>();
        _projectileInteraction = GetComponent<XevyProjectileInteraction>();
        _health.OnDeath += OnXevyDefeated;
    }

    private void FixedUpdate()
    {
        _movement.MovementUpdate();
        _playerInteraction.UpdatePlayerInteraction();
        switch (_status)
        {
            //Xevy attend un peu avant de prendre une décision.
            case XevyStatus.IDLE:
                UpdateWhenIdle();
                break;
            //Xevy est exposé aux coups pendant un certain temps.
            case XevyStatus.VULNERABLE:
                UpdateWhenVulnerable();
                break;
            //Xevy bloque tous les coups.
            case XevyStatus.BLOCKING:
                UpdateWhenBlocking();
                break;
            //Xevy se repose et se soigne.
            case XevyStatus.HEALING:
                UpdateWhenHealing();
                break;
            default:
                break;
        }
    }

    private void UpdateWhenIdle()
    {
        if (_statusTimer > 0)
        {
            if (_projectileInteraction.CheckIfProjectilesInSight())
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
            bool healthStatus = (_health.HealthPoint > _health.MaxHealth * _criticalHealthPercentage);
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

                if (_sameAttackCount == _numberSameAttacksBeforeMovement)
                {
                    _movement.BounceTowardsRandomPoint();
                    _sameAttackCount = 0;
                }
            }
        }
    }

    private void UpdateWhenBlocking()
    {
        if (!_projectileInteraction.CheckIfProjectilesInSight())
        {
            SetIdleStatus();
        }
    }

    private void UpdateWhenHealing()
    {
        _action.Heal();
        if (!_projectileInteraction.CheckIfProjectilesInSight() || _playerInteraction.CheckPlayerDistance())
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
        _animator.SetInteger("State", 0);
        _statusTimer = _idleStatusCooldown;
        _action.LowerGuard();
        _playerInteraction.IsFocusedOnPlayer = true;
        _status = XevyStatus.IDLE;
    }

    private void SetVulnerableStatus()
    {
        _animator.SetInteger("State", 1);
        _statusTimer = _vulnerableStatusCooldown;
        _playerInteraction.IsFocusedOnPlayer = false;
        _status = XevyStatus.VULNERABLE;
    }

    private void SetBlockingStatus()
    {
        _animator.SetInteger("State", 2);
        _action.Block();
        _playerInteraction.IsFocusedOnPlayer = true;
        _status = XevyStatus.BLOCKING;
    }

    private void OnXevyDefeated()
    {
        _status = XevyStatus.DEAD;
    }
}
