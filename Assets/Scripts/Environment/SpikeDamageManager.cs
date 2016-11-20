using UnityEngine;
using System.Collections;

//Cela ressemble à EnemiesAttackManager, à l'exception que celui-ci vérifie le dommage au niveau de la Collision, plutôt que sur le Trigger.
//Nous avons donc préféré les garder en scripts séparés.

/*
* BEN_REVIEW
* 
* N'empêche qu'il serait possible de faire un script avec un autre nom, car actuellement, c'est comme si
* cela ne fonctionnait que pour les spikes, tandis que cela pourrait marcher avec n'importe quoi.
*/
public class SpikeDamageManager : MonoBehaviour
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
        return !PlayerState.IsInvincible && collision.gameObject.tag == "Player";
    }
}
