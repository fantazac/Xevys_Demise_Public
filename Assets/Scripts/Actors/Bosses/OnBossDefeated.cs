using UnityEngine;
using System.Collections;

/*
 * BEN_REVIEW
 * 
 * Health n'est-il pas suffisant ? En fait, tu veux désactiver les colliders quand il meurt ? Pourquoi pas créer un autre
 * script juste pour cela ?
 * 
 * Genre "DisableCollidersOnDeath" ?
 */
public class OnBossDefeated : MonoBehaviour
{
    private bool _isDead;
    private Health _health;
    private BoxCollider2D _boxCollider;
    private PolygonCollider2D _polygonCollider;

    /*
     * BEN_REVIEW
     * 
     * Évènement inutile compte tenu que "Health" fourni déjà de l'information à ce sujet.
     */
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
