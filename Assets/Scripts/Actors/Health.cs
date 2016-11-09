using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float _health = 1000f;
    public float MaxHealth { get; private set; }

    public float HealthPoint { get { return _health; } set { _health = value; } }

    public delegate void OnDamageTakenHandler(int hitPoints);
    public event OnDamageTakenHandler OnDamageTaken;

    public delegate void OnHealHandler(int healPoints);
    public event OnHealHandler OnHeal;

    public delegate void OnHealthChangedHandler(int healPoints);
    public event OnHealthChangedHandler OnHealthChanged;

    public delegate void OnDeathHandler();
    public event OnDeathHandler OnDeath;

    private KnockbackOnDamageTaken _knockback;

    private void Start()
    {
        MaxHealth = _health;
        OnHealthChanged += ChangeHealth;
        _knockback = StaticObjects.GetPlayer().GetComponent<KnockbackOnDamageTaken>();
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

    public void Hit(int hitPoints, Vector2 positionAttacker)
    {
        if (!IsDead())
        {
            OnDamageTaken(-hitPoints);
            OnHealthChanged(-hitPoints);        
            if (IsDead())
            {
                OnDeath();
            }
            else if (gameObject.tag == "Player")
            {               
                _knockback.KnockbackPlayer(positionAttacker);              
            }
        }
    }

    private void ChangeHealth(int healthPointsToAdd)
    {
        HealthPoint += healthPointsToAdd;
    }

    private bool IsDead()
    {
        return _health <= 0;
    }
}
