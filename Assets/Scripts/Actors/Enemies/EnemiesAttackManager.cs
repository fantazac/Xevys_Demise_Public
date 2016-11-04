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
            /* BEN_CORRECTION
             * 
             * Le simple fait d'appeller "Hit" sur le player devrait déclancher le reste. En fait, il
             * devrait y avoir un évènement sur "Health" auquel les deux autres composants 
             * (KnockbackOnDamageTaken et InvincibilityAfterBeingHit) sont abbonnés.
             * 
             * Ce serait pas plus simple et plus logique ?
             */
            collider.GetComponent<Health>().Hit(_baseDamage);
            collider.GetComponent<KnockbackOnDamageTaken>().KnockbackPlayer(transform.position);
            collider.GetComponent<InvincibilityAfterBeingHit>().StartFlicker();

            _damageTimer = _baseDamageTimer;
        }
    }
}
