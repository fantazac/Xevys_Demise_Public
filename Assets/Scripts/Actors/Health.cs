﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float _health = 1000f;

    public float HealthPoint { get { return _health; } set { _health = value; } }

    public delegate void HealthChangedHandler(int hitPoint);
    public event HealthChangedHandler OnHealthChanged;

    public void Heal(int healPoints)
    {
        HealthPoint += healPoints;
    }

    public void Hit(int hitPoints)
    {
        if (OnHealthChanged != null)
        {
            OnHealthChanged(hitPoints);
        }
        HealthPoint -= hitPoints;
    }
}
