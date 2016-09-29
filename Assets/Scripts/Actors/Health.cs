using UnityEngine;
using System.Collections;

public class Health: MonoBehaviour
{
    [SerializeField]
    private float _health = 1000f;

    public delegate void OnHealthChangedHandler(float newHealth);
    public event OnHealthChangedHandler OnHealthChanged;

    public float HealthPoints
    {
        get { return _health; }
        private set { _health = value;
            if (OnHealthChanged != null)
            {
                OnHealthChanged(_health);
            }
        }
    }

    public void Heal(float healPoints)
    {
        HealthPoints += healPoints;
    }

    public void Hit(float hitPoints)
    {
        HealthPoints -= hitPoints;
    }
}
