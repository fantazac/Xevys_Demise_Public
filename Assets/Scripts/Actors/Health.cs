using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Health: MonoBehaviour
{
    [SerializeField]
    private float _health = 1000f;
    private int testCounter = 0;

    public float HealthPoint { get { return _health; } set { _health = value; } }

    public delegate void HealthChangedHandler(int hitPoint);
    public event HealthChangedHandler OnHealthChanged;

    private void Update()
    {
        testCounter++;
        if (testCounter >= 30)
        {
            if (OnHealthChanged != null)
            {
                OnHealthChanged(10);
                testCounter = 0;
            }
            
        }
    }
}
