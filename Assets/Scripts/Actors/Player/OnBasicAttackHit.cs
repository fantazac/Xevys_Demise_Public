using UnityEngine;
using System.Collections;
using System.Linq;

public class OnBasicAttackHit : MonoBehaviour
{
    [SerializeField]
    private int _baseDamage = 100;

    private Health _playerHealth;

    private string[] _enemiesTags;
    private string[] _bossesTags;

    private void Start()
    {
        _enemiesTags = new string[] { "Scarab", "Bat", "Skeltal" };
        _bossesTags = new string[] { "Behemoth", "Phoenix", "Neptune" };
        _playerHealth = StaticObjects.GetPlayer().GetComponent<Health>();
        _playerHealth.OnDeath += OnDeath;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_bossesTags.Contains(collider.gameObject.tag) && collider is PolygonCollider2D)
        {
            collider.GetComponent<Health>().Hit(_baseDamage, Vector2.zero);
        }
        else if (_enemiesTags.Contains(collider.gameObject.tag))
        {
            collider.GetComponent<Health>().Hit(_baseDamage, Vector2.zero);
        }
    }

    private void OnDeath()
    {
        enabled = false;
    }
}
