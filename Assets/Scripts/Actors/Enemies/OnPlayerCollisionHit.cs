using UnityEngine;
using System.Collections;

public class OnPlayerCollisionHit : MonoBehaviour
{
    [SerializeField]
    private int _baseDamage = 100;

    private Health _playerHealth;

    private void Start()
    {
        _playerHealth = StaticObjects.GetPlayer().GetComponent<Health>();
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (CanAttackPlayer(collider))
        {
            _playerHealth.Hit(_baseDamage, transform.position);
        }
    }

    private bool CanAttackPlayer(Collider2D collider)
    {
        return !StaticObjects.GetPlayerState().IsInvincible && collider.gameObject.tag == "Player";
    }
}
