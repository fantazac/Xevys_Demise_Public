using UnityEngine;
using System.Collections;

public class DestroyOnDeath : MonoBehaviour
{

    private Health _health;

    private void Start()
    {
        _health = GetComponent<Health>();
    }

    private void Update()
    {
        if (_health.HealthPoint <= 0)
        {
            Destroy(gameObject);
        }
    }
}
