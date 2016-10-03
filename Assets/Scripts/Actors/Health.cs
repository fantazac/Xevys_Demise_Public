using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Health: MonoBehaviour
{
    [SerializeField]
    private float _health = 1000f;

    public float HealthPoint { get { return _health; } set { _health = value; } }

    //private void Update()
    //{
    //    if (_health == 0)
    //    {
    //        Debug.Log("Player Health = " + _health);
    //    }
    //}
}
