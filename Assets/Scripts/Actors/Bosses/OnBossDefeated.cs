using UnityEngine;
using System.Collections;

public class OnBossDefeated : MonoBehaviour
{
    private bool _isDead;
    private Health _health;
    private BoxCollider2D _boxCollider;
    private PolygonCollider2D _polygonCollider;

    public delegate void OnDefeated();
    public event OnDefeated onDefeated;

    // Use this for initialization
    private void Start ()
    {
        _health = GetComponent<Health>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _polygonCollider = GetComponent<PolygonCollider2D>();
    }
	
	// Update is called once per frame
	private void Update ()
    {
        if (!_isDead && _health.HealthPoint <= 0)
        {
            _isDead = true;
            _boxCollider.enabled = false;
            _polygonCollider.enabled = false;
            onDefeated();
        }
    }
}
