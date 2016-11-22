using UnityEngine;
using System.Collections;

public class EnvironmentOnCollisionDamage : MonoBehaviour
{
    [SerializeField]
    private int _baseDamage = 100;

    private Health _health;

    private void Start()
    {
        _health = StaticObjects.GetPlayer().GetComponent<Health>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (CanAttackPlayer(collision))
        {
            _health.Hit(_baseDamage, transform.position);
        }
    }

    private bool CanAttackPlayer(Collision2D collision)
    {
        return !StaticObjects.GetPlayerState().IsInvincible && collision.gameObject.tag == StaticObjects.GetUnityTags().Player;
    }
}
