using UnityEngine;
using System.Collections;

public class DestroyEnemyOnDeath : MonoBehaviour
{

    [SerializeField]
    private bool _destroyParent = false;

    private BoxCollider2D _hitbox;
    private Health _health;
    private PlaySoundOnEnemyDeath _enemyDeathSound;

    private void Start()
    {
        _health = GetComponent<Health>();
        _health.OnDeath += OnDeath;

        _enemyDeathSound = GetComponent<PlaySoundOnEnemyDeath>();
        _enemyDeathSound.OnDeathSoundFinished += Destroy;

        _hitbox = GetComponent<BoxCollider2D>();
    }

    private void OnDeath()
    {
        _hitbox.enabled = false;
    }

    private void Destroy()
    {
        Destroy(gameObject);
        if (_destroyParent)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
