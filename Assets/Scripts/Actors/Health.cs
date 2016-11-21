using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float _health = 1000f;

    public float MaxHealth { get; private set; }

    public float HealthPoint { get { return _health; } set { _health = value; } }

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
        Database.OnHealthReloaded += ReloadHealth;
        MaxHealth = _health;
        OnHealthChanged += ChangeHealth;
    }

    public void Heal(int healPoints)
    {
        if(_health + healPoints > MaxHealth)
        {
            healPoints = (int)(MaxHealth - _health);
        }
        OnHeal(healPoints);
        OnHealthChanged(healPoints);
    }

    public void FullHeal()
    {
        OnHeal(Convert.ToInt32(MaxHealth));
        OnHealthChanged(Convert.ToInt32(MaxHealth));

    }

    public void Hit(int hitPoints, Vector2 attackerPosition)
    {
        if (!IsDead())
        {
            if(OnDamageTaken != null)
            {
                OnDamageTaken(-hitPoints);
            }
            
            OnHealthChanged(-hitPoints);    
                
            if (IsDead())
            {
                OnDeath();
            }
            if(OnDamageTakenByEnemy != null)
            {
                OnDamageTakenByEnemy(attackerPosition);
            }
        }
    }

    public void Hit(int hitPoints)
    {
        if (!IsDead())
        {
            if (OnDamageTaken != null)
            {
                OnDamageTaken(-hitPoints);
            }

            OnHealthChanged(-hitPoints);

            if (IsDead())
            {
                OnDeath();
            }
        }
    }

    private void ChangeHealth(int healthPointsToAdd)
    {
        HealthPoint += healthPointsToAdd;
    }

    private void ReloadHealth(float health)
    {
        if(tag == "Player")
        {
            OnHealthChanged(Convert.ToInt32(-(_health - health)));
        }
    }

    private bool IsDead()
    {
        return _health <= 0;
    }

    public bool CanHeal()
    {
        return _health < MaxHealth;
    }
}
