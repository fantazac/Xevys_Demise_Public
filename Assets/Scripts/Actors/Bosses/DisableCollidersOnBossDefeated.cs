using UnityEngine;
using System.Collections;

public class DisableCollidersOnBossDefeated : MonoBehaviour
{
    private Health _health;
    private BoxCollider2D _boxCollider;
    private PolygonCollider2D _polygonCollider;

    private void Start()
    {
        _health = GetComponent<Health>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _polygonCollider = GetComponent<PolygonCollider2D>();
        _health.OnDeath += OnBossDeath;
    }

    private void OnBossDeath()
    {
        _boxCollider.enabled = false;
        _polygonCollider.enabled = false;
        GetComponent<FadeOutAfterDeath>().enabled = true;
        if (GetComponent<Rigidbody2D>() != null)
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }
}
