using UnityEngine;
using System.Collections;
using System.Linq;

public class ActorDamageManager : MonoBehaviour
{
    [SerializeField]
    private int _baseDamage = 100;
    private int _baseDamageTimer;
    private int _damageTimer;

    private string _attackerTag;
    private string _receiverTag;
    private string[] _enemiesTags;

    private void Start()
    {
        _attackerTag = gameObject.tag;
        _enemiesTags = new string[] { "Scarab", "Bat", "Behemoth" };

        if (_attackerTag == "BasicAttackHitbox")
        {
            _baseDamageTimer = 50;
        }
        else if(_enemiesTags.Contains(_attackerTag))
        {
            _baseDamageTimer = 200;
        }
        _damageTimer = _baseDamageTimer;
    }

    private void FixedUpdate()
    {
        _damageTimer--;
        if (_damageTimer == 0)
        {
            Debug.Log(_attackerTag + " timer reached 0");
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        _receiverTag = collider.gameObject.tag;

        if (((_attackerTag == "BasicAttackHitbox" && (_enemiesTags.Contains(_receiverTag))
            || (_enemiesTags.Contains(_attackerTag)) && _receiverTag == "Player")) && _damageTimer <= 0)
        {
            if (collider.GetComponent<Health>().HealthPoint >= 100)
            {
                collider.GetComponent<Health>().Hit(_baseDamage);
            }
            else if (collider.GetComponent<Health>().HealthPoint < 100)
            {
                collider.GetComponent<Health>().Hit((int)collider.GetComponent<Health>().HealthPoint);
            }

            _damageTimer = _baseDamageTimer;
            Debug.Log(_attackerTag + " attacked " + _receiverTag + "!");
        }
    }
}
