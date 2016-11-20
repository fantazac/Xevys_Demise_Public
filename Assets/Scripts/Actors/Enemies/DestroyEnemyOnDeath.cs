using UnityEngine;
using System.Collections;

public class DestroyEnemyOnDeath : MonoBehaviour
{
    [SerializeField]
    private bool _destroyParent = false;
    
    public delegate void OnEnemyDeathHandler(string enemyType);
    public static event OnEnemyDeathHandler OnEnemyDeath;

    private BoxCollider2D _hitbox;
    private Health _health;
    private PlaySoundOnEnemyDeath _enemyDeathSound;

    private void Start()
    {
        _health = GetComponent<Health>();
        _health.OnDeath += OnDeath;
        /*
         * BEN_REVIEW
         * 
         * Au lieu de se fier au son, mieux vaudrait avoir un composant qui gère l'animation et le son de mort 
         * et déclanche un évènement quand les deux son terminés.
         */
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
        OnEnemyDeath(tag);
        Destroy(gameObject);
        if (_destroyParent)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
