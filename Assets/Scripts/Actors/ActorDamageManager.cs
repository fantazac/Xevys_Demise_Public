using UnityEngine;
using System.Collections;

public class ActorDamageManager : MonoBehaviour
{
    [SerializeField]
    private int _baseDamage = 100;
    private int _baseDamageTimer;
    private int _damageTimer;

    private string _attackerTag;
    private string _receiverTag;

    private void Start()
    {
        _attackerTag = gameObject.tag;
        if(_attackerTag == "BasicAttackHitbox")
        {
            _baseDamageTimer = 50;
        }
        else if(_attackerTag == "Scarab" || _attackerTag == "Bat")
        {
            _baseDamageTimer = 200;
        }
        _damageTimer = _baseDamageTimer;
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        _receiverTag = collider.gameObject.tag;
        _damageTimer--;

        if (((_attackerTag == "BasicAttackHitbox" && (_receiverTag == "Scarab" || _receiverTag == "Bat"))
            || ((_attackerTag == "Scarab" || _attackerTag == "Bat") && _receiverTag == "Player")) && _damageTimer <= 0)
        {
            if (collider.GetComponent<Health>().HealthPoint >= 100)
            {
                collider.GetComponent<Health>().Hit(_baseDamage);
            }
            else if (collider.GetComponent<Health>().HealthPoint < 100)
            {
                collider.GetComponent<Health>().Hit((int)collider.GetComponent<Health>().HealthPoint);
            }

            if(_receiverTag == "Player")
            {
                collider.GetComponent<KnockbackOnDamageTaken>().KnockbackPlayer(transform.position);
            }

            _damageTimer = _baseDamageTimer;
        }
    }
}
