using UnityEngine;
using System.Collections;
using System.Linq;

public class ActorDamageManager : MonoBehaviour
{
    [SerializeField]
    private int _baseDamage = 100;
    private int _baseDamageTimer;

    private int _damageTimer = 0;

    private string _attackerTag;
    private string _receiverTag;
    private string[] _enemiesTags;
    private string[] _playerTags;

    private void Start()
    {
        _attackerTag = gameObject.tag;
        _playerTags = new string[] { "BasicAttackHitbox", "Knife", "Axe" };
        _enemiesTags = new string[] { "Scarab", "Bat", "Skeltal", "Behemoth", "SkeltalSwordHitbox" };

        if (_playerTags.Contains(_attackerTag))
        {
            _baseDamageTimer = 50;
        }
        else if (_enemiesTags.Contains(_attackerTag))
        {
            _baseDamageTimer = (int)GameObject.Find("Character").GetComponent<InvincibilityAfterBeingHit>().InvincibilityTime;
        }
    }

    private void FixedUpdate()
    {
        if (_damageTimer == 0)
        {
            _damageTimer--;
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        _receiverTag = collider.gameObject.tag;

        if (((_playerTags.Contains(_attackerTag) && (_enemiesTags.Contains(_receiverTag))
            || (_enemiesTags.Contains(_attackerTag)) && _receiverTag == "Player" && !collider.GetComponent<InvincibilityAfterBeingHit>().IsFlickering))
            && _damageTimer <= 0)
        {
            if (collider.GetComponent<Health>().HealthPoint >= 100)
            {
                collider.GetComponent<Health>().Hit(_baseDamage);
            }
            else if (collider.GetComponent<Health>().HealthPoint < 100)
            {
                collider.GetComponent<Health>().Hit((int)collider.GetComponent<Health>().HealthPoint);
            }

            if (_receiverTag == "Player")
            {
                collider.GetComponent<KnockbackOnDamageTaken>().KnockbackPlayer(transform.position);
                collider.GetComponent<InvincibilityAfterBeingHit>().StartFlicker();
            }

            _damageTimer = _baseDamageTimer;
        }
    }
}
