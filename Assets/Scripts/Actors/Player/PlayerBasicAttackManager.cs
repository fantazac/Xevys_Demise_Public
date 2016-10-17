using UnityEngine;
using System.Collections;
using System.Linq;

public class PlayerBasicAttackManager : MonoBehaviour
{
    [SerializeField]
    private int _baseDamage = 100;
    private int _baseDamageTimer;

    private int _damageTimer = 0;

    private string[] _enemiesTags;

    private void Start()
    {
        _enemiesTags = new string[] { "Scarab", "Bat", "Skeltal", "Behemoth", "Phoenix", "SkeltalSwordHitbox" };
        _baseDamageTimer = 50;
    }


    private void Update()
    {
        _damageTimer--;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_enemiesTags.Contains(collider.gameObject.tag) && _damageTimer <= 0)
        {
            collider.GetComponent<Health>().Hit(_baseDamage);
            _damageTimer = _baseDamageTimer;
        }
    }
}