using UnityEngine;
using System.Collections;

/*
 * BEN_REVIEW
 * 
 * Nul besoin de vous dire de ce que je pense des classes qui s'appellent "Manager".
 * 
 * Pourquoi pas "HitPlayerOnCollision" ?
 * 
 * De toute façon, je ferais cela autrement, et même que ce serait plus "générique". 
 * 
 * Explications :
 * 
 * Le composant devrait avoir une liste de "tag" sur lequel il peut faire des dégats. Cette liste
 * est reçu de l'éditeur. Lors d'une colision, si le tag est bon, obtenez le composant "Health" sur 
 * l'élément et envoyez les dégats. Dans le cas spécifique du player, le composant "Health" serait 
 * en réalité une instance de "PlayerHealth" qui hérite de "Health" et qui effectue les vérifications de 
 * la méthode "CanAttackPlayer" avant de enregistrer les dégats.
 */
public class EnemiesAttackManager : MonoBehaviour
{
    [SerializeField]
    private int _baseDamage = 100;

    private Health _playerHealth;

    private void Start()
    {
        _playerHealth = StaticObjects.GetPlayer().GetComponent<Health>();
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (CanAttackPlayer(collider))
        {
            _playerHealth.Hit(_baseDamage, transform.position);
        }
    }

    private bool CanAttackPlayer(Collider2D collider)
    {
        return !PlayerState.IsInvincible && collider.gameObject.tag == "Player";
    }
}
