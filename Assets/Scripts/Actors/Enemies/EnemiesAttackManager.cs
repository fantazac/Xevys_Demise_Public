using UnityEngine;
using System.Collections;

/* BEN_REVIEW
 * 
 * Du momment qu'il y a "Manager" dans le nom, ça sent mauvais. À renommer.
 * 
 * Pourquoi vous vous demandez ? En fait, "Manager" ne dit pas ce que cela fait en réalité. C'est pas clair.
 * Je sais que nommer les classes est difficile, et on a tendance à utiliser des termes comme "Handler" et "Manager"
 * par paresse car on ne trouve pas de nom. Croyez-moi, pourtant, le temps investi dans le nommage en vaut clairement la
 * chandelle, et je peux vous dire que vous n'êtes pas si pire que ça pour le nommage dans la globalité.
 */
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
        /* BEN_REVIEW
         * 
         * Décrémenter avec le "DeltaTime".
         */
        _damageTimer--;
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        /* BEN_REVIEW
         * 
         * Vous devriez avoir un component qui sert de point d'entrée aux attaques sur le Player. C'est ensuite
         * ce componsant que vous appelez à partir des ennemis (c'est à dire EnemiesAttackManager). Il pourra donc 
         * décider si comme l'attaque reçue doit être gérée en fonction de l'état du joueur (invincible ou non).
         */
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
