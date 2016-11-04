using UnityEngine;
using System.Collections;

public class OnBossDefeated : MonoBehaviour
{
    private bool _isDead;
    private Health _health;
    private BoxCollider2D _boxCollider;
    private PolygonCollider2D _polygonCollider;

    public delegate void OnDefeated();
    /* BEN_CORRECTION
     * 
     * Évènements, Méthodes et Propriétés toujours en PascalCasing". Toujours!
     */
    public event OnDefeated onDefeated;

    private void Start()
    {
        _isDead = false;
        _health = GetComponent<Health>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _polygonCollider = GetComponent<PolygonCollider2D>();
    }

	private void Update()
    {
        /* BEN_CORRECTION
         * 
         * Il devrait plutôt y avoir un évènement dans "Health" qui se déclanche quand les points
         * de vie atteignent 0. Cela éviterait ce "if" à toutes les frames.
         */
        if (!_isDead && _health.HealthPoint <= 0)
        {
            _isDead = true;
            _boxCollider.enabled = false;
            _polygonCollider.enabled = false;
            onDefeated();
        }
    }
}
