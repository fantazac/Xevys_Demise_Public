using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float _health = 1000f;

    private AudioSourcePlayer _soundPlayer;

    public float HealthPoint { get { return _health; } set { _health = value; } }

    public delegate void OnDamageTakenHandler(int hitPoints);
    public event OnDamageTakenHandler OnDamageTaken;

    public delegate void OnHealHandler(int healPoints);
    public event OnHealHandler OnHeal;

    public delegate void OnHealthChangedHandler(int healPoints);
    public event OnHealthChangedHandler OnHealthChanged;

    private void Start()
    {
        _soundPlayer = GetComponent<AudioSourcePlayer>();
        OnHealthChanged += ChangeHealth;
    }

    public void Heal(int healPoints)
    {
        OnHeal(healPoints);
        OnHealthChanged(healPoints);
    }

    public void Hit(int hitPoints)
    {
        OnDamageTaken(-hitPoints);
        OnHealthChanged(-hitPoints);
    }

    private void ChangeHealth(int healthPointsToAdd)
    {
        HealthPoint += healthPointsToAdd;
    }
}
