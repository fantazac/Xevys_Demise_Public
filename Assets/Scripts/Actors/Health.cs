using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Health: MonoBehaviour
{
    [SerializeField]
    private float _health = 1000f;

    private List<PlayerDamageManager> _damageManagers;

    private void Start()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Scarab");

        for(int i = 0; i < enemies.Length; i++)
        {
            Debug.Log("There is " + enemies.Length + " enemies");
            enemies[i].GetComponent<PlayerDamageManager>().OnDamage += OnDamage;
            Debug.Log("Scarab added");
        }
    }

    private void OnDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
            Debug.Log("Dead");
    }
}
