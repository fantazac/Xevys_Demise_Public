using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int _health = 1000;

    public int MaxHealth { get; private set; }

    public int HealthPoint { get { return _health; } set { _health = value; } }

    public delegate void OnDamageTakenHandler(int hitPoints);
    public event OnDamageTakenHandler OnDamageTaken;

    public delegate void OnDamageTakenByEnemyHandler(Vector2 attackerPosition);
    public event OnDamageTakenByEnemyHandler OnDamageTakenByEnemy;

    public delegate void OnHealHandler(int healPoints);
    public event OnHealHandler OnHeal;

    public delegate void OnHealthChangedHandler(int healPoints);
    public event OnHealthChangedHandler OnHealthChanged;

    public delegate void OnDeathHandler();
    public event OnDeathHandler OnDeath;

    private void Start()
    {
        AccountStats.OnHealthReloaded += ReloadHealth;
        MaxHealth = _health;
    }

    public void Heal(int healPoints)
    {
        if (HealWouldGiveTooMuchHealth(healPoints))
        {
            healPoints = (int)(MaxHealth - _health);
        }
        _health += healPoints;

        OnHeal(healPoints);
        if (OnHealthChanged != null)
        {
            OnHealthChanged(healPoints);
        }
    }

    public void FullHeal()
    {
        Heal(MaxHealth - _health);
    }

    public void Hit(int hitPoints, Vector2 attackerPosition)
    {
        Hit(hitPoints);

        if (OnDamageTakenByEnemy != null && !IsDead())
        {
            OnDamageTakenByEnemy(attackerPosition);
        }
    }

    public void Hit(int hitPoints)
    {
        if (!IsDead())
        {
            _health -= hitPoints;

            if (OnDamageTaken != null)
            {
                OnDamageTaken(-hitPoints);
            }

            if (OnHealthChanged != null)
            {
                OnHealthChanged(-hitPoints);
            }

            if (IsDead())
            {
                OnDeath();
            }
        }
    }

    private void ReloadHealth(int health)
    {
        if (tag == StaticObjects.GetObjectTags().Player)
        {
            _health -= (_health - health);
        }
    }

    public bool IsDead()
    {
        return _health <= 0;
    }

    public bool CanHeal()
    {
        return _health < MaxHealth;
    }

    private bool HealWouldGiveTooMuchHealth(float healPoints)
    {
        return _health + healPoints > MaxHealth;
    }
}
