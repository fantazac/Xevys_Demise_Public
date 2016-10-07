using UnityEngine;
using System.Collections;

public class EnemiesAttackManager : MonoBehaviour
{
    [SerializeField]
    private int _baseDamage = 100;
    private int _baseDamageTimer;

    private int _damageTimer = 0;

    private void Start()
    {
        _baseDamageTimer = (int)GameObject.Find("Character").GetComponent<InvincibilityAfterBeingHit>().InvincibilityTime;
    }

    private void Update()
    {
        _damageTimer--;
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" &&
            !collider.GetComponent<InvincibilityAfterBeingHit>().IsFlickering &&
            _damageTimer <= 0)
        {
            collider.GetComponent<Health>().Hit(_baseDamage);
            collider.GetComponent<KnockbackOnDamageTaken>().KnockbackPlayer(transform.position);
            collider.GetComponent<InvincibilityAfterBeingHit>().StartFlicker();

            _damageTimer = _baseDamageTimer;
        }
    }
}
