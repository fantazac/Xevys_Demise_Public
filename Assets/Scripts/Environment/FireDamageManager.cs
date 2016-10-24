using UnityEngine;
using System.Collections;

public class FireDamageManager : MonoBehaviour
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

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player" &&
            !GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManager>().FireProofArmorEnabled &&
            !coll.gameObject.GetComponent<InvincibilityAfterBeingHit>().IsFlickering &&
            _damageTimer <= 0)
        {
            coll.gameObject.GetComponent<Health>().Hit(_baseDamage);
            coll.gameObject.GetComponent<KnockbackOnDamageTaken>().KnockbackPlayer(transform.position);
            coll.gameObject.GetComponent<InvincibilityAfterBeingHit>().StartFlicker();

            _damageTimer = _baseDamageTimer;
        }
    }
}
