using UnityEngine;
using System.Collections;

public class DestroyEnemyOnDeath : MonoBehaviour
{
    [SerializeField]
    private bool _destroyParent = false;
    
    public delegate void OnEnemyDeathHandler(string enemyType);
    public static event OnEnemyDeathHandler OnEnemyDeath;

    private BoxCollider2D _hitbox;

    private void Start()
    {
        GetComponent<Health>().OnDeath += OnDeath;

        GetComponent<PlaySoundOnEnemyDeath>().OnDeathSoundFinished += Destroy;

        _hitbox = GetComponent<BoxCollider2D>();
    }

    private void OnDeath()
    {
        _hitbox.enabled = false;
    }

    private void Destroy()
    {
        OnEnemyDeath(tag);
        Destroy(gameObject);
        if (_destroyParent)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
