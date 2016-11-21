using UnityEngine;
using System.Collections;

public class OnBossDefeated : MonoBehaviour
{
    private Health _health;
    private BoxCollider2D _boxCollider;
    private PolygonCollider2D _polygonCollider;

    public delegate void OnDefeatedHandler();
    public event OnDefeatedHandler OnDefeated;

    private void Start()
    {
        _health = GetComponent<Health>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _polygonCollider = GetComponent<PolygonCollider2D>();
        _health.OnDeath += OnDeath;
    }

    private void OnDeath()
    {
        _boxCollider.enabled = false;
        _polygonCollider.enabled = false;
        OnDefeated();
    }
}
